namespace Cavity.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Xunit;

    public sealed class LexiconFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Lexicon>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new Lexicon());
        }

        [Fact]
        public void ctor_IComparer()
        {
            Assert.NotNull(new Lexicon(StringComparer.InvariantCulture));
        }

        [Fact]
        public void ctor_IComparerNull()
        {
            Assert.NotNull(new Lexicon(null));
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
        public void op_Contains_stringEmpty_whenInvariantComparer()
        {
            Assert.False(new Lexicon(StringComparer.InvariantCulture).Contains(string.Empty));
        }

        [Fact]
        public void op_Contains_stringNull()
        {
            Assert.False(new Lexicon().Contains(null));
        }

        [Fact]
        public void op_Contains_stringNull_whenInvariantComparer()
        {
            Assert.False(new Lexicon(StringComparer.InvariantCulture).Contains(null));
        }

        [Fact]
        public void op_Contains_string_whenEmpty()
        {
            Assert.False(new Lexicon().Contains("Example"));
        }

        [Fact]
        public void op_Contains_string_whenEmptyWithInvariantComparer()
        {
            Assert.False(new Lexicon(StringComparer.InvariantCulture).Contains("Example"));
        }

        [Fact]
        public void op_Contains_string_whenInvariantComparer()
        {
            var obj = new Lexicon(StringComparer.InvariantCultureIgnoreCase);
            obj.Items.Add(new LexicalItem("Example"));

            Assert.True(obj.Contains("EXAMPLE"));
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
        public void op_LoadCsv_FileInfo_withRepeats()
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
                        writer.WriteLine("1,Unit");
                    }
                }

                var obj = Lexicon.LoadCsv(file);

                Assert.Equal("1", obj.ToCanonicalForm("One"));
                Assert.Equal("1", obj.ToCanonicalForm("Unit"));
                Assert.Equal(1, obj.Items.Count);
                Assert.Equal(2, obj.Items.First().Synonyms.Count);
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
        public void op_SaveCsv_FileInfo()
        {
            var file = new FileInfo(Path.GetTempFileName());
            try
            {
                file.Delete();

                var obj = new Lexicon(StringComparer.InvariantCultureIgnoreCase);
                obj.Items.Add(new LexicalItem("Example"));

                obj.SaveCsv(file);

                Assert.True(file.Exists);

                Assert.True(Lexicon.LoadCsv(file).Contains("Example"));
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
        public void op_SaveCsv_FileInfoNull()
        {
            var obj = new Lexicon();
            obj.Items.Add(new LexicalItem("Example"));

            Assert.Throws<ArgumentNullException>(() => obj.SaveCsv(null));
        }

        [Fact]
        public void op_SaveCsv_FileInfo_IComparer()
        {
            var file = new FileInfo(Path.GetTempFileName());
            try
            {
                file.Delete();

                var obj = new Lexicon();
                obj.Items.Add(new LexicalItem("Example"));

                obj.SaveCsv(file);

                Assert.True(file.Exists);

                Assert.True(Lexicon.LoadCsv(file, StringComparer.InvariantCultureIgnoreCase).Contains("EXAMPLE"));
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
        public void op_SaveCsv_FileInfo_IComparerNull()
        {
            var file = new FileInfo(Path.GetTempFileName());
            try
            {
                file.Delete();

                var obj = new Lexicon();
                obj.Items.Add(new LexicalItem("Example"));

                obj.SaveCsv(file);

                Assert.True(file.Exists);

                Assert.True(Lexicon.LoadCsv(file, null).Contains("Example"));
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
        public void op_SaveCsv_FileInfo_whenEmpty()
        {
            var file = new FileInfo(Path.GetTempFileName());
            try
            {
                file.Delete();

                new Lexicon().SaveCsv(file);

                Assert.True(file.Exists);

                Assert.True(File.ReadAllText(file.FullName).StartsWith("CANONICAL,SYNONYMS"));
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

            var actual = obj.ToCanonicalForm(obj.Items.First().Synonyms.First());

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToCanonicalForm_stringEmpty()
        {
            Assert.Null(new Lexicon().ToCanonicalForm(string.Empty));
        }

        [Fact]
        public void op_ToCanonicalForm_stringEmpty_whenInvariantComparer()
        {
            Assert.Null(new Lexicon(StringComparer.InvariantCulture).ToCanonicalForm(string.Empty));
        }

        [Fact]
        public void op_ToCanonicalForm_stringNull()
        {
            Assert.Null(new Lexicon().ToCanonicalForm(null));
        }

        [Fact]
        public void op_ToCanonicalForm_stringNull_whenInvariantComparer()
        {
            Assert.Null(new Lexicon(StringComparer.InvariantCulture).ToCanonicalForm(null));
        }

        [Fact]
        public void op_ToCanonicalForm_string_whenInvariantComparer()
        {
            const string expected = "1";

            var obj = new Lexicon(StringComparer.InvariantCultureIgnoreCase);
            obj.Items.Add(new LexicalItem(expected)
            {
                Synonyms =
                    {
                        "One"
                    }
            });

            var actual = obj.ToCanonicalForm("ONE");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Comparer()
        {
            Assert.NotNull(new PropertyExpectations<Lexicon>("Comparer")
                               .TypeIs<IComparer<string>>()
                               .DefaultValueIsNull()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_Items()
        {
            Assert.NotNull(new PropertyExpectations<Lexicon>("Items")
                               .TypeIs<ICollection<LexicalItem>>()
                               .DefaultValueIsNotNull()
                               .IsNotDecorated()
                               .Result);
        }
    }
}