namespace Cavity.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Xunit;
    using Xunit.Extensions;

    public sealed class JsonDocumentFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<JsonDocument>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IEnumerable<JsonObject>>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new JsonDocument());
        }

        [Fact]
        public void opIndexer_int()
        {
            var expected = new JsonObject();

            var document = new JsonDocument
                               {
                                   expected
                               };

            var actual = document[0];

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opIndexer_intNegative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new JsonDocument()[-1]);
        }

        [Fact]
        public void opIndexer_int_whenEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new JsonDocument()[0]);
        }

        [Fact]
        public void op_Add_JsonObject()
        {
            var expected = new JsonObject();

            var document = new JsonDocument
                               {
                                   expected
                               };

            var actual = document.First();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Add_JsonObjectNull()
        {
            Assert.Throws<ArgumentNullException>(() => new JsonDocument().Add(null));
        }

        [Fact]
        public void op_GetEnumerator()
        {
            var expected = new JsonObject();

            IEnumerable document = new JsonDocument
                                       {
                                           expected
                                       };

            foreach (var actual in document)
            {
                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_Load_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => JsonDocument.Load(null));
        }

        [Theory]
        [InlineData("[{\"i\": 0},{\"i\": 1}]")]
        public void op_Load_string_whenArray(string json)
        {
            var i = 0;
            foreach (var obj in JsonDocument.Load(json))
            {
                Assert.Equal(i++, obj.Number("i").ToInt32());
            }
        }

        [Theory]
        [InlineData("{\"Name\" : \"value\", \"Number\" : 123}")]
        public void op_Load_string_whenSingleObject(string json)
        {
            var document = JsonDocument.Load(json);

            Assert.Equal("value", document[0].String("Name").Value);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(" [] ")]
        public void op_Load_string_whenZeroObjects(string json)
        {
            Assert.Empty(JsonDocument.Load(json));
        }

        [Fact]
        public void prop_Count()
        {
            Assert.True(new PropertyExpectations<JsonDocument>(x => x.Count)
                            .TypeIs<int>()
                            .DefaultValueIs(0)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Count_get()
        {
            var document = new JsonDocument();
            Assert.Equal(0, document.Count);

            document.Add(new JsonObject());

            Assert.Equal(1, document.Count);
        }
    }
}