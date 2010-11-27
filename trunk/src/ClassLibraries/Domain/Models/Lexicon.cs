namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using Cavity.Data;
    using Cavity.IO;
    using Cavity.Linq;

    public sealed class Lexicon
    {
        public Lexicon()
            : this(StringComparer.OrdinalIgnoreCase)
        {
        }

        public Lexicon(IComparer<string> comparer)
        {
            Items = new Collection<LexicalItem>();
            Comparer = comparer;
        }

        public IComparer<string> Comparer { get; set; }

        public ICollection<LexicalItem> Items { get; private set; }

        public static Lexicon LoadCsv(FileInfo file)
        {
            return LoadCsv(file, null);
        }

        public static Lexicon LoadCsv(FileInfo file, IComparer<string> comparer)
        {
            var result = null == comparer ? new Lexicon() : new Lexicon(comparer);

            foreach (var data in new CsvFile(file))
            {
                var canonical = data["CANONICAL"];
                var item = result.Items
                    .Where(x => string.Equals(x.CanonicalForm, canonical, StringComparison.Ordinal))
                    .FirstOrDefault();
                if (null == item)
                {
                    item = new LexicalItem(canonical);
                    result.Items.Add(item);
                }

                foreach (var synonym in data["SYNONYMS"]
                    .Split(';', StringSplitOptions.RemoveEmptyEntries)
                    .Where(synonym => !item.Synonyms.Contains(synonym)))
                {
                    item.Synonyms.Add(synonym);
                }
            }

            return result;
        }

        public void Add(string value)
        {
            if (Contains(value))
            {
                return;
            }

            Items.Add(new LexicalItem(value));
        }

        public bool Contains(string value)
        {
            return Items.Any(item => item.Contains(value, Comparer));
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Will trust in using statement.")]
        public void SaveCsv(FileSystemInfo file)
        {
            if (null == file)
            {
                throw new ArgumentNullException("file");
            }

            using (var writers = new StreamWriterDictionary("CANONICAL,SYNONYMS")
            {
                Access = FileAccess.Write,
                Mode = FileMode.CreateNew,
                Share = FileShare.None
            })
            {
                if (0 == Items.Count)
                {
                    writers.Item(file.FullName).WriteLine(string.Empty);
                    return;
                }

                foreach (var item in Items.OrderBy(x => x.CanonicalForm))
                {
                    writers
                        .Item(file.FullName)
                        .WriteLine("{0},{1}".FormatWith(
                            item.CanonicalForm.FormatCommaSeparatedValue(),
                            item.Synonyms.OrderBy(x => x).Concat(';').FormatCommaSeparatedValue()));
                }
            }
        }

        public string ToCanonicalForm(string value)
        {
            return (from item in Items
                    where item.Contains(value, Comparer)
                    select item.CanonicalForm).FirstOrDefault();
        }
    }
}