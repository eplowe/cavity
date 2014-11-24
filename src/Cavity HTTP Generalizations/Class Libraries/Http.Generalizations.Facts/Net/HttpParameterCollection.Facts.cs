namespace Cavity.Net
{
    using System;
    using System.Collections.Generic;
    using Xunit;

    public sealed class ParameterDictionaryFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HttpParameterDictionary>()
                            .DerivesFrom<Dictionary<Token, string>>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .Serializable()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new HttpParameterDictionary());
        }

        [Fact]
        public void op_Add_Parameter()
        {
            var obj = new HttpParameterDictionary
                          {
                              new HttpParameter("name", "value")
                          };

            Assert.Equal("value", obj["name"]);
        }

        [Fact]
        public void op_Add_ParameterNull()
        {
            var obj = new HttpParameterDictionary();

            Assert.Throws<ArgumentNullException>(() => obj.Add(null));
        }

        [Fact]
        public void op_ToString()
        {
            var obj = new HttpParameterDictionary
                          {
                              { "a", "1" },
                              { "b", "2" }
                          };

            const string expected = ";a=1;b=2";
            var actual = obj.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Quality()
        {
            Assert.True(new PropertyExpectations<HttpParameterDictionary>(x => x.Quality)
                            .IsAutoProperty(Quality.One)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Quality_set()
        {
            Quality expected = 0.123f;

            var obj = new HttpParameterDictionary
                          {
                              Quality = expected
                          };

            var actual = obj.Quality;

            Assert.Equal(expected, actual);
        }
    }
}