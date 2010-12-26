namespace Cavity.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using Cavity.IO;
    using Cavity.Linq;
    using Cavity.Models;

    public sealed class CsvLexiconStorage : IStoreLexicon
    {
        private FileInfo _location;

        public CsvLexiconStorage(FileInfo location)
            : this()
        {
            Location = location;
        }

        private CsvLexiconStorage()
        {
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

                _location = value;
            }
        }

        public void Delete(Lexicon lexicon)
        {
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

        public Lexicon Load(ILexiconComparer comparer)
        {
            var result = new Lexicon(comparer)
            {
                Storage = this
            };

            foreach (var data in new CsvFile(Location))
            {
                var canonical = data["CANONICAL"];

                var item = result[canonical];
                if (null == item)
                {
                    item = result.Add(canonical);
                }

                foreach (var synonym in data["SYNONYMS"]
                    .Split(';', StringSplitOptions.RemoveEmptyEntries))
                {
                    item.Synonyms.Add(synonym);
                }
            }

            return result;
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "I trust the using statement.")]
        public void Save(Lexicon lexicon)
        {
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
                if (0 == lexicon.Items.Count())
                {
                    writers.Item(Location.FullName).WriteLine(string.Empty);
                    return;
                }

                foreach (var item in lexicon.Items.OrderBy(x => x.CanonicalForm))
                {
                    writers
                        .Item(Location.FullName)
                        .WriteLine("{0},{1}".FormatWith(
                            item.CanonicalForm.FormatCommaSeparatedValue(),
                            item.Synonyms.OrderBy(x => x).Concat(';').FormatCommaSeparatedValue()));
                }
            }
        }
    }
}