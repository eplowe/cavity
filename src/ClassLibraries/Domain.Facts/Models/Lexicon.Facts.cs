namespace Cavity.Models
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using Xunit;

    public sealed class LexiconFacts
    {
        [Fact]
        public void ctor()
        {
            Assert.NotNull(new Lexicon());
        }

        [Fact]
        public void op_Contains_string()
        {
            var obj = new Lexicon();
            obj.Items.Add(new LexicalItem("Example"));

            Assert.True(obj.Contains("Example"));
        }

        [Fact]
        public void op_Contains_stringEmpty()
        {
            Assert.False(new Lexicon().Contains(string.Empty));
        }

        [Fact]
        public void op_Contains_stringEmpty_StringComparison()
        {
            Assert.False(new Lexicon().Contains(string.Empty, StringComparison.InvariantCulture));
        }

        [Fact]
        public void op_Contains_stringNull()
        {
            Assert.False(new Lexicon().Contains(null));
        }

        [Fact]
        public void op_Contains_stringNull_StringComparison()
        {
            Assert.False(new Lexicon().Contains(null, StringComparison.InvariantCulture));
        }

        [Fact]
        public void op_Contains_string_StringComparison()
        {
            var obj = new Lexicon();
            obj.Items.Add(new LexicalItem("Example"));

            Assert.True(obj.Contains("EXAMPLE", StringComparison.InvariantCultureIgnoreCase));
        }

        [Fact]
        public void op_Contains_string_StringComparison_whenEmpty()
        {
            Assert.False(new Lexicon().Contains("Example", StringComparison.InvariantCulture));
        }

        [Fact]
        public void op_Contains_string_whenEmpty()
        {
            Assert.False(new Lexicon().Contains("Example"));
        }

        [Fact]
        public void op_LoadCsv_FileInfo()
        {
            var file = new FileInfo(Path.GetTempFileName());
            try
            {
                using (var stream = file.Open(FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.WriteLine("CANONICAL,SYNONYMS");
                        writer.WriteLine("1,");
                    }
                }

                var obj = Lexicon.LoadCsv(file);

                Assert.Equal("1", obj.ToCanonicalForm("1"));
            }
            finally
            {
                if (file.Exists)
                {
                    file.Delete();
                }
            }
        }

        [Fact]
        public void op_LoadCsv_FileInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => Lexicon.LoadCsv(null));
        }

        [Fact]
        public void op_LoadCsv_FileInfo_withMultipleSynonyms()
        {
            var file = new FileInfo(Path.GetTempFileName());
            try
            {
                using (var stream = file.Open(FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.WriteLine("CANONICAL,SYNONYMS");
                        writer.WriteLine("1,One;Unit");
                    }
                }

                var obj = Lexicon.LoadCsv(file);

                Assert.Equal("1", obj.ToCanonicalForm("One"));
                Assert.Equal("1", obj.ToCanonicalForm("Unit"));
            }
            finally
            {
                if (file.Exists)
                {
                    file.Delete();
                }
            }
        }

        [Fact]
        public void op_LoadCsv_FileInfo_withSingleSynonym()
        {
            var file = new FileInfo(Path.GetTempFileName());
            try
            {
                using (var stream = file.Open(FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.WriteLine("CANONICAL,SYNONYMS");
                        writer.WriteLine("1,One");
                    }
                }

                var obj = Lexicon.LoadCsv(file);

                Assert.Equal("1", obj.ToCanonicalForm("One"));
            }
            finally
            {
                if (file.Exists)
                {
                    file.Delete();
                }
            }
        }

        [Fact]
        public void op_ToCanonicalForm_string()
        {
            const string expected = "1";

            var obj = new Lexicon();
            obj.Items.Add(new LexicalItem(expected)
            {
                Synonyms =
                    {
                        "One"
                    }
            });

            var actual = obj.ToCanonicalForm(obj.Items[0].Synonyms.First());

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToCanonicalForm_stringEmpty()
        {
            Assert.Null(new Lexicon().ToCanonicalForm(string.Empty));
        }

        [Fact]
        public void op_ToCanonicalForm_stringEmpty_StringComparison()
        {
            Assert.Null(new Lexicon().ToCanonicalForm(string.Empty, StringComparison.InvariantCulture));
        }

        [Fact]
        public void op_ToCanonicalForm_stringNull()
        {
            Assert.Null(new Lexicon().ToCanonicalForm(null));
        }

        [Fact]
        public void op_ToCanonicalForm_stringNull_StringComparison()
        {
            Assert.Null(new Lexicon().ToCanonicalForm(null, StringComparison.InvariantCulture));
        }

        [Fact]
        public void op_ToCanonicalForm_string_StringComparison()
        {
            const string expected = "1";

            var obj = new Lexicon();
            obj.Items.Add(new LexicalItem(expected)
            {
                Synonyms =
                    {
                        "One"
                    }
            });

            var actual = obj.ToCanonicalForm(
                obj.Items[0].Synonyms.First().ToUpperInvariant(),
                StringComparison.InvariantCultureIgnoreCase);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Items()
        {
            Assert.NotNull(new PropertyExpectations<Lexicon>("Items")
                               .TypeIs<Collection<LexicalItem>>()
                               .DefaultValueIsNotNull()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void type_definition()
        {
            Assert.True(new TypeExpectations<Lexicon>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }
    }
}