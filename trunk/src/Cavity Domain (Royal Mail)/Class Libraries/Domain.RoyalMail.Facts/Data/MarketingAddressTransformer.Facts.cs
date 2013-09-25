namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cavity.Collections;
    using Cavity.Models;
    using Xunit;

    public sealed class MarketingAddressTransformerFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<MarketingAddressTransformer>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .Implements<ITransformData<MarketingAddress>>()
                            .Result);
        }

        [Fact]
        public void op_Transform_IEnumerableOfKeyStringDictionaryNull()
        {
            Assert.Throws<ArgumentNullException>(() => new MarketingAddressTransformer().Transform(null));
        }

        [Fact]
        public void op_Transform_IEnumerableOfKeyStringDictionary()
        {
            var data = new List<KeyStringDictionary>
                {
                    new KeyStringDictionary
                        {
                            {"SBN", "Flat 1"},
                            {"BNA", "Tall House"},
                            {"NUM", "123"},
                            {"DST", "Little Lane"},
                            {"STM", "High Street"},
                            {"DDL", "Local Wood"},
                            {"DLO", "Wide Area"},
                            {"PTN", "Postal Town"},
                            {"PCD", "AA1 2ZZ"},
                        }
                };

            const string expected = "Flat 1, Tall House, 123 Little Lane, High Street, Local Wood, Wide Area, AA1 2ZZ";
            var actual = new MarketingAddressTransformer().Transform(data).First().ToString();
            
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Transform_IEnumerableOfKeyStringDictionary_withColumns()
        {
            const string expected = "Test";

            var data = new List<KeyStringDictionary>
                {
                    new KeyStringDictionary
                        {
                            {"SBN", "Flat 1"},
                            {"BNA", "Tall House"},
                            {"NUM", "123"},
                            {"DST", "Little Lane"},
                            {"STM", "High Street"},
                            {"DDL", "Local Wood"},
                            {"DLO", "Wide Area"},
                            {"PTN", "Postal Town"},
                            {"PCD", "AA1 2ZZ"},
                            {"EXAMPLE", expected},
                        }
                };

            var first = new MarketingAddressTransformer(new List<string>{"EXAMPLE"}).Transform(data).First();

            var actual = first["EXAMPLE"];

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Transform_IEnumerableOfKeyStringDictionary_withReferences()
        {
            const string key = "3f·0";
            const string expected = "Test";
            var references = new Dictionary<PostalAddressFileKey, string>
                {
                    {key, expected},
                };

            var data = new List<KeyStringDictionary>
                {
                    new KeyStringDictionary
                        {
                            {"SBN", "Flat 1"},
                            {"BNA", "Tall House"},
                            {"NUM", "123"},
                            {"DST", "Little Lane"},
                            {"STM", "High Street"},
                            {"DDL", "Local Wood"},
                            {"DLO", "Wide Area"},
                            {"PTN", "Postal Town"},
                            {"PCD", "AA1 2ZZ"},
                            {"KEY", key},
                        }
                };

            var first = new MarketingAddressTransformer(null, references).Transform(data).First();

            var actual = first["REFERENCE"];

            Assert.Equal(expected, actual);
        }
    }
}