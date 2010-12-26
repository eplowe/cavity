namespace Cavity.Data
{
    using System;
    using System.IO;
    using System.Linq;
    using Cavity.Models;
    using Xunit;

    public sealed class CsvLexiconStorageFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<CsvLexiconStorage>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IStoreLexicon>()
                            .Result);
        }

        [Fact]
        public void ctor_FileInfo()
        {
            var file = new FileInfo(Path.GetTempFileName());

            Assert.NotNull(new CsvLexiconStorage(file));
        }

        [Fact]
        public void ctor_FileInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => new CsvLexiconStorage(null));
        }

        [Fact]
        public void op_Delete_Lexicon()
        {
            var file = new FileInfo(Path.GetTempFileName());
            try
            {
                IStoreLexicon store = new CsvLexiconStorage(file);
                store.Delete(new Lexicon(StandardLexiconComparer.Ordinal));

                file.Refresh();
                Assert.False(file.Exists);
            }
            finally
            {
                file.Refresh();
                if (file.Exists)
                {
                    file.Delete();
                }
            }
        }

        [Fact]
        public void op_Delete_LexiconNull()
        {
            var file = new FileInfo(Path.GetTempFileName());
            IStoreLexicon store = new CsvLexiconStorage(file);

            Assert.Throws<ArgumentNullException>(() => store.Delete(null));
        }

        [Fact]
        public void op_Load_IComparer()
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

                IStoreLexicon store = new CsvLexiconStorage(file);
                var obj = store.Load(StandardLexiconComparer.Ordinal);

                Assert.Equal("1", obj.ToCanonicalForm("1"));

                Assert.Same(store, obj.Storage);
            }
            finally
            {
                file.Refresh();
                if (file.Exists)
                {
                    file.Delete();
                }
            }
        }

        [Fact]
        public void op_Load_IComparerNull()
        {
            var file = new FileInfo(Path.GetTempFileName());
            try
            {
                file.Delete();

                var lexicon = new Lexicon(StandardLexiconComparer.Ordinal);
                lexicon.Add("Example");

                IStoreLexicon store = new CsvLexiconStorage(file);
                store.Save(lexicon);

                Assert.Throws<ArgumentNullException>(() => store.Load(null));
            }
            finally
            {
                file.Refresh();
                if (file.Exists)
                {
                    file.Delete();
                }
            }
        }

        [Fact]
        public void op_Load_withMultipleSynonyms()
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

                IStoreLexicon store = new CsvLexiconStorage(file);
                var obj = store.Load(StandardLexiconComparer.Ordinal);

                Assert.Equal("1", obj.ToCanonicalForm("One"));
                Assert.Equal("1", obj.ToCanonicalForm("Unit"));
            }
            finally
            {
                file.Refresh();
                if (file.Exists)
                {
                    file.Delete();
                }
            }
        }

        [Fact]
        public void op_Load_withRepeats()
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

                IStoreLexicon store = new CsvLexiconStorage(file);
                var obj = store.Load(StandardLexiconComparer.Ordinal);

                Assert.Equal("1", obj.ToCanonicalForm("One"));
                Assert.Equal("1", obj.ToCanonicalForm("Unit"));
                Assert.Equal(1, obj.Items.Count());
                Assert.Equal(2, obj.Items.First().Synonyms.Count);
            }
            finally
            {
                file.Refresh();
                if (file.Exists)
                {
                    file.Delete();
                }
            }
        }

        [Fact]
        public void op_Load_withSingleSynonym()
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

                IStoreLexicon store = new CsvLexiconStorage(file);
                var obj = store.Load(StandardLexiconComparer.Ordinal);

                Assert.Equal("1", obj.ToCanonicalForm("One"));
            }
            finally
            {
                file.Refresh();
                if (file.Exists)
                {
                    file.Delete();
                }
            }
        }

        [Fact]
        public void op_Save_Lexicon()
        {
            var file = new FileInfo(Path.GetTempFileName());
            try
            {
                file.Delete();

                var lexicon = new Lexicon(StandardLexiconComparer.Ordinal);
                lexicon.Add("Example");

                IStoreLexicon store = new CsvLexiconStorage(file);
                store.Save(lexicon);

                file.Refresh();
                Assert.True(file.Exists);

                Assert.True(store.Load(StandardLexiconComparer.Ordinal).Contains("Example"));
            }
            finally
            {
                file.Refresh();
                if (file.Exists)
                {
                    file.Delete();
                }
            }
        }

        [Fact]
        public void op_Save_LexiconNull()
        {
            var file = new FileInfo(Path.GetTempFileName());
            IStoreLexicon store = new CsvLexiconStorage(file);

            Assert.Throws<ArgumentNullException>(() => store.Save(null));
        }

        [Fact]
        public void op_Save_LexiconWhenEmpty()
        {
            var file = new FileInfo(Path.GetTempFileName());
            try
            {
                file.Delete();

                IStoreLexicon store = new CsvLexiconStorage(file);
                store.Save(new Lexicon(StandardLexiconComparer.Ordinal));

                file.Refresh();
                Assert.True(file.Exists);

                Assert.True(File.ReadAllText(file.FullName).StartsWith("CANONICAL,SYNONYMS"));
            }
            finally
            {
                file.Refresh();
                if (file.Exists)
                {
                    file.Delete();
                }
            }
        }

        [Fact]
        public void op_Save_LexiconWithComma()
        {
            var file = new FileInfo(Path.GetTempFileName());
            try
            {
                file.Delete();

                var lexicon = new Lexicon(StandardLexiconComparer.Ordinal);
                lexicon.Add("foo, bar");

                IStoreLexicon store = new CsvLexiconStorage(file);
                store.Save(lexicon);

                file.Refresh();
                Assert.True(file.Exists);

                Assert.True(store.Load(StandardLexiconComparer.Ordinal).Contains("foo, bar"));
            }
            finally
            {
                file.Refresh();
                if (file.Exists)
                {
                    file.Delete();
                }
            }
        }

        [Fact]
        public void prop_Location()
        {
            Assert.True(new PropertyExpectations<CsvLexiconStorage>(p => p.Location)
                            .IsNotDecorated()
                            .TypeIs<FileInfo>()
                            .ArgumentNullException()
                            .Result);
        }
    }
}