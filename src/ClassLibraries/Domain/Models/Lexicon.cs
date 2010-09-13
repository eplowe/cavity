namespace Cavity.Models
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using Cavity.IO;

    public sealed class Lexicon
    {
        public Lexicon()
        {
            Items = new Collection<LexicalItem>();
        }

        public Collection<LexicalItem> Items { get; private set; }

        public static Lexicon LoadCsv(FileInfo file)
        {
            var result = new Lexicon();

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

                foreach (var synonym in data["SYNONYMS"].Split(
                    new[]
                    {
                        ";"
                    },
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    if (!item.Synonyms.Contains(synonym))
                    {
                        item.Synonyms.Add(synonym);
                    }
                }
            }

            return result;
        }

        public bool Contains(string value)
        {
            return Contains(value, StringComparison.Ordinal);
        }

        public bool Contains(string value, StringComparison comparisonType)
        {
            return Items.Any(item => item.Contains(value, comparisonType));
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
                    writers
                        .Item(file.FullName)
                        .WriteLine(string.Empty);
                }
                else
                {
                    foreach (var item in Items.OrderBy(x => x.CanonicalForm))
                    {
                        var synonyms = new StringBuilder();
                        foreach (var synonym in item.Synonyms.OrderBy(x => x))
                        {
                            if (0 != synonyms.Length)
                            {
                                synonyms.Append(';');
                            }

                            synonyms.Append(synonym);
                        }

                        writers
                            .Item(file.FullName)
                            .WriteLine(string.Format(
                                CultureInfo.InvariantCulture,
                                "{0},{1}",
                                item.CanonicalForm,
                                synonyms));
                    }
                }
            }
        }

        public string ToCanonicalForm(string value)
        {
            return ToCanonicalForm(value, StringComparison.Ordinal);
        }

        public string ToCanonicalForm(string value, StringComparison comparisonType)
        {
            return (from item in Items
                    where item.Contains(value, comparisonType)
                    select item.CanonicalForm).FirstOrDefault();
        }
    }
}