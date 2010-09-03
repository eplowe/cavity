namespace Cavity.Models
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
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
                var item = new LexicalItem(data["CANONICAL"]);
                foreach (var synonym in data["SYNONYMS"].Split(
                    new[]
                    {
                        ";"
                    },
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    item.Synonyms.Add(synonym);
                }

                result.Items.Add(item);
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