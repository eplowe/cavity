namespace Cavity.Data
{
    using System;
    using System.IO;
    using System.Linq;
    using Cavity.Collections;
    using Cavity.IO;
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
                            .IsUnsealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IStoreLexicon>()
                            .Result);
        }

        [Fact]
        public void ctor_FileInfo()
        {
            using (var file = new TempFile())
            {
                Assert.NotNull(new CsvLexiconStorage(file.Info));
            }
        }

        [Fact]
        public void ctor_FileInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => new CsvLexiconStorage(null));
        }

        [Fact]
        public void op_Delete_Lexicon()
        {
            using (var file = new TempFile())
            {
                IStoreLexicon store = new CsvLexiconStorage(file.Info);
                store.Delete(new Lexicon(NormalizationComparer.Ordinal));

                file.Info.Refresh();
                Assert.False(file.Info.Exists);
            }
        }

        [Fact]
        public void op_Delete_LexiconNull()
        {
            using (var file = new TempFile())
            {
                IStoreLexicon store = new CsvLexiconStorage(file.Info);

                Assert.Throws<ArgumentNullException>(() => store.Delete(null));
            }
        }

        [Fact]
        public void op_Load_IComparer()
        {
            using (var file = new TempFile())
            {
                file.Info.AppendLine("CANONICAL,SYNONYMS");
                file.Info.AppendLine("1,");

                IStoreLexicon store = new CsvLexiconStorage(file.Info);
                var obj = store.Load(NormalizationComparer.Ordinal);

                Assert.Equal("1", obj.ToCanonicalForm("1"));

                Assert.Same(store, obj.Storage);
            }
        }

        [Fact]
        public void op_Load_IComparerNull()
        {
            using (var file = new TempFile())
            {
                file.Info.Delete();

                var lexicon = new Lexicon(NormalizationComparer.Ordinal);
                lexicon.Add("Example");

                IStoreLexicon store = new CsvLexiconStorage(file.Info);
                store.Save(lexicon);

                Assert.Throws<ArgumentNullException>(() => store.Load(null));
            }
        }

        [Fact]
        public void op_Load_withMultipleSynonyms()
        {
            using (var file = new TempFile())
            {
                file.Info.AppendLine("CANONICAL,SYNONYMS");
                file.Info.AppendLine("1,One;Unit");

                IStoreLexicon store = new CsvLexiconStorage(file.Info);
                var obj = store.Load(NormalizationComparer.Ordinal);

                Assert.Equal("1", obj.ToCanonicalForm("One"));
                Assert.Equal("1", obj.ToCanonicalForm("Unit"));
            }
        }

        [Fact]
        public void op_Load_withRepeats()
        {
            using (var file = new TempFile())
            {
                file.Info.AppendLine("CANONICAL,SYNONYMS");
                file.Info.AppendLine("1,One");
                file.Info.AppendLine("1,Unit");

                IStoreLexicon store = new CsvLexiconStorage(file.Info);
                var obj = store.Load(NormalizationComparer.Ordinal);

                Assert.Equal("1", obj.ToCanonicalForm("One"));
                Assert.Equal("1", obj.ToCanonicalForm("Unit"));
                Assert.Equal(1, obj.Items.Count());
                Assert.Equal(2, obj.Items.First().Synonyms.Count);
            }
        }

        [Fact]
        public void op_Load_withSingleSynonym()
        {
            using (var file = new TempFile())
            {
                file.Info.AppendLine("CANONICAL,SYNONYMS");
                file.Info.AppendLine("1,One");

                IStoreLexicon store = new CsvLexiconStorage(file.Info);
                var obj = store.Load(NormalizationComparer.Ordinal);

                Assert.Equal("1", obj.ToCanonicalForm("One"));
            }
        }

        [Fact]
        public void op_Save_Lexicon()
        {
            using (var file = new TempFile())
            {
                file.Info.Delete();

                var lexicon = new Lexicon(NormalizationComparer.Ordinal);
                lexicon.Add("Example");

                IStoreLexicon store = new CsvLexiconStorage(file.Info);
                store.Save(lexicon);

                file.Info.Refresh();
                Assert.True(file.Info.Exists);

                Assert.True(store.Load(NormalizationComparer.Ordinal).Contains("Example"));
            }
        }

        [Fact]
        public void op_Save_LexiconNull()
        {
            using (var file = new TempFile())
            {
                IStoreLexicon store = new CsvLexiconStorage(file.Info);

                Assert.Throws<ArgumentNullException>(() => store.Save(null));
            }
        }

        [Fact]
        public void op_Save_LexiconWhenEmpty()
        {
            using (var file = new TempFile())
            {
                file.Info.Delete();

                IStoreLexicon store = new CsvLexiconStorage(file.Info);
                store.Save(new Lexicon(NormalizationComparer.Ordinal));

                file.Info.Refresh();
                Assert.True(file.Info.Exists);

                Assert.True(File.ReadAllText(file.Info.FullName).StartsWith("CANONICAL,SYNONYMS", StringComparison.Ordinal));
            }
        }

        [Fact]
        public void op_Save_LexiconWithComma()
        {
            using (var file = new TempFile())
            {
                file.Info.Delete();

                var lexicon = new Lexicon(NormalizationComparer.Ordinal);
                lexicon.Add("foo, bar");

                IStoreLexicon store = new CsvLexiconStorage(file.Info);
                store.Save(lexicon);

                file.Info.Refresh();
                Assert.True(file.Info.Exists);

                Assert.True(store.Load(NormalizationComparer.Ordinal).Contains("foo, bar"));
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