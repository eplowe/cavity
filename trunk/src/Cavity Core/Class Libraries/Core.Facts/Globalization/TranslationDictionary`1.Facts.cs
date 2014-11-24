namespace Cavity.Globalization
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Threading;
    using Xunit;

    public sealed class TranslationDictionaryOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<TranslationDictionary<int>>().DerivesFrom<Dictionary<Language, int>>()
                                                                          .IsConcreteClass()
                                                                          .IsUnsealed()
                                                                          .HasDefaultConstructor()
                                                                          .Serializable()
                                                                          .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new TranslationDictionary<int>());
        }

        [Fact]
        public void ctor_SerializationInfo_StreamingContext()
        {
            var expected = new TranslationDictionary<int>
                               {
                                   new Translation<int>(123, "en")
                               };
            TranslationDictionary<int> actual;

            using (Stream stream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                var obj = new TranslationDictionary<int>
                              {
                                  new Translation<int>(123, "en")
                              };
                formatter.Serialize(stream, obj);
                stream.Position = 0;
                actual = (TranslationDictionary<int>)formatter.Deserialize(stream);
            }

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Add_Translation()
        {
            var obj = new TranslationDictionary<int>
                          {
                              new Translation<int>(123, "en")
                          };

            Assert.Equal(1, obj.Count);
        }

        [Fact]
        public void op_Contains_Translation()
        {
            var item = new Translation<int>(123, "en");
            var obj = new TranslationDictionary<int>
                          {
                              item
                          };

            Assert.True(obj.Contains(item));
        }

        [Fact]
        public void op_GetEnumerator()
        {
            var obj = new TranslationDictionary<int>
                          {
                              new Translation<int>(123, "en")
                          };

            foreach (var item in obj)
            {
                Assert.IsType<Translation<int>>(item);
                Assert.Equal(new Language("en"), item.Language);
                Assert.Equal(123, item.Value);
            }
        }

        [Fact]
        public void op_Remove_KeyStringPair()
        {
            var item = new Translation<int>(123, "en");
            var obj = new TranslationDictionary<int>
                          {
                              item
                          };

            Assert.True(obj.Remove(item));
            Assert.Equal(0, obj.Count);
        }

        [Fact]
        public void prop_Current()
        {
            Assert.True(new PropertyExpectations<TranslationDictionary<decimal>>(x => x.Current)
                            .TypeIs<decimal>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Current_whenExactTranslationMissing()
        {
            Assert.Throws<TranslationException>(() => new TranslationDictionary<int>().Current);
        }

        [Fact]
        public void prop_Current_whenFallbackTranslation()
        {
            var culture = Thread.CurrentThread.CurrentUICulture;
            try
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");

                const int expected = 123;
                var obj = new TranslationDictionary<int>
                              {
                                  new Translation<int>(expected, "fr"),
                                  new Translation<int>(456, "fr-CA")
                              };

                var actual = obj.Current;

                Assert.Equal(expected, actual);
            }
            finally
            {
                Thread.CurrentThread.CurrentUICulture = culture;
            }
        }

        [Fact]
        public void prop_Current_whenFullTranslation()
        {
            var culture = Thread.CurrentThread.CurrentUICulture;
            try
            {
                const string language = "fr-FR";

                Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);

                const int expected = 123;
                var obj = new TranslationDictionary<int>
                              {
                                  new Translation<int>(456, "fr"),
                                  new Translation<int>(expected, language)
                              };

                var actual = obj.Current;

                Assert.Equal(expected, actual);
            }
            finally
            {
                Thread.CurrentThread.CurrentUICulture = culture;
            }
        }

        [Fact]
        public void prop_Current_whenInvariantTranslation()
        {
            var culture = Thread.CurrentThread.CurrentUICulture;
            try
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");

                const int expected = 123;
                var obj = new TranslationDictionary<int>
                              {
                                  new Translation<int>(expected, string.Empty),
                                  new Translation<int>(456, "fr-CA")
                              };

                var actual = obj.Current;

                Assert.Equal(expected, actual);
            }
            finally
            {
                Thread.CurrentThread.CurrentUICulture = culture;
            }
        }
    }
}