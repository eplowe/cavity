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

        public IStoreLexicon Storage { get; set; }

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

        public void Save()
        {
            Save(Storage);
        }

        public void Save(IStoreLexicon storage)
        {
            if (null == storage)
            {
                throw new ArgumentNullException("storage");
            }

            Storage = storage;
            Storage.Save(this);
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