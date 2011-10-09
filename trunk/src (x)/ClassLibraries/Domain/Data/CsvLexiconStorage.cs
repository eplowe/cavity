namespace Cavity.Data
{
    using System;
#if NET20
    using System.Collections.Generic;
#endif
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
#if !NET20
    using System.Linq;
#endif
    using Cavity.Collections;
    using Cavity.Collections.Generic;
    using Cavity.Diagnostics;
    using Cavity.IO;
    using Cavity.Models;

    public class CsvLexiconStorage : IStoreLexicon
    {
        private FileInfo _location;

        public CsvLexiconStorage(FileInfo location)
            : this()
        {
            Location = location;
        }

        private CsvLexiconStorage()
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
        }

        public FileInfo Location
        {
            get
            {
                return _location;
            }

            set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

#if !NET20
                Trace.WriteIf(Tracing.Is.TraceVerbose, "value=\"{0}\"".FormatWith(value.FullName));
#endif
                _location = value;
            }
        }

        public virtual void Delete(Lexicon lexicon)
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
            if (null == lexicon)
            {
                throw new ArgumentNullException("lexicon");
            }

            Location.Refresh();
            if (Location.Exists)
            {
                Location.Delete();
            }
        }

        public virtual Lexicon Load(INormalizationComparer comparer)
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
            var result = new Lexicon(comparer)
            {
                Storage = this
            };

            foreach (var data in new CsvFile(Location))
            {
                var canonical = data["CANONICAL"];
                var item = result[canonical] ?? result.Add(canonical);
#if NET20
                foreach (var synonym in StringExtensionMethods.Split(data["SYNONYMS"], ';', StringSplitOptions.RemoveEmptyEntries))
#else
                foreach (var synonym in data["SYNONYMS"].Split(';', StringSplitOptions.RemoveEmptyEntries))
#endif
                {
                    item.Synonyms.Add(synonym);
                }
            }

            return result;
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "I trust the using statement.")]
        public virtual void Save(Lexicon lexicon)
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
            if (null == lexicon)
            {
                throw new ArgumentNullException("lexicon");
            }

            using (var writers = new StreamWriterDictionary("CANONICAL,SYNONYMS")
            {
                Access = FileAccess.Write,
                Mode = FileMode.Create,
                Share = FileShare.None
            })
            {
#if NET20
                if (0 == IEnumerableExtensionMethods.Count(lexicon.Items))
#else
                if (0 == lexicon.Items.Count())
#endif
                {
                    writers.Item(Location.FullName).WriteLine(string.Empty);
                    return;
                }

#if NET20
                var items = new SortedList<string, LexicalItem>();
                foreach (var item in lexicon.Items)
                {
                    items.Add(item.CanonicalForm, item);
                }

                foreach (var item in items)
                {
                    var synonyms = new SortedList<string, string>();
                    foreach (var synonym in item.Value.Synonyms)
                    {
                        synonyms.Add(synonym, synonym);
                    }

                    writers
                        .Item(Location.FullName)
                        .WriteLine(StringExtensionMethods.FormatWith(
                            "{0},{1}",
                            DataStringExtensionMethods.FormatCommaSeparatedValue(item.Value.CanonicalForm),
                            DataStringExtensionMethods.FormatCommaSeparatedValue(IEnumerableExtensionMethods.Concat(synonyms.Values, ';'))));
                }
#else
                foreach (var item in lexicon.Items.OrderBy(x => x.CanonicalForm))
                {
                    writers
                        .Item(Location.FullName)
                        .WriteLine("{0},{1}".FormatWith(
                            item.CanonicalForm.FormatCommaSeparatedValue(),
                            item.Synonyms.OrderBy(x => x).Concat(';').FormatCommaSeparatedValue()));
                }
#endif
            }
        }
    }
}