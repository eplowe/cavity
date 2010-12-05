namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
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

        public Lexicon Load()
        {
            return Load(null);
        }

        public Lexicon Load(IComparer<string> comparer)
        {
            var result = null == comparer ? new Lexicon() : new Lexicon(comparer);
            result.Storage = this;

            foreach (var data in new CsvFile(Location))
            {
                var canonical = data["CANONICAL"];
                var item = result.Items
                    .Where(x => 0 == (comparer ?? StringComparer.Ordinal).Compare(x.CanonicalForm, canonical))
                    .FirstOrDefault();
                if (null == item)
                {
                    item = new LexicalItem(canonical);
                    result.Items.Add(item);
                }

                foreach (var synonym in data["SYNONYMS"]
                    .Split(';', StringSplitOptions.RemoveEmptyEntries))
                {
                    var s = synonym;
                    if (0 == item
                        .Synonyms
                        .Where(x => 0 == (comparer ?? StringComparer.Ordinal).Compare(x, s))
                        .Count())
                    {
                        item.Synonyms.Add(synonym);
                    }
                }
            }

            return result;
        }

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
                if (0 == lexicon.Items.Count)
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