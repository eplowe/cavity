namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;

    using Xunit;

    public sealed class JsonArrayFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<JsonArray>()
                            .DerivesFrom<JsonValue>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new JsonArray());
        }

        [Fact]
        public void op_Boolean_int_whenFalse()
        {
            var document = new JsonArray
                               {
                                   Values =
                                       {
                                           new JsonFalse()
                                       }
                               };

            Assert.False(document.Boolean(0));
        }

        [Fact]
        public void op_Boolean_int_whenInvalidCastException()
        {
            var document = new JsonArray
                               {
                                   Values =
                                       {
                                           new JsonNull()
                                       }
                               };

            Assert.Throws<InvalidCastException>(() => document.Boolean(0));
        }

        [Fact]
        public void op_Boolean_int_whenTrue()
        {
            var document = new JsonArray
                               {
                                   Values =
                                       {
                                           new JsonTrue()
                                       }
                               };

            Assert.True(document.Boolean(0));
        }

        [Fact]
        public void op_IsNull_int_whenFalse()
        {
            var document = new JsonArray
                               {
                                   Values =
                                       {
                                           new JsonString("value")
                                       }
                               };

            Assert.False(document.IsNull(0));
        }

        [Fact]
        public void op_IsNull_int_whenTrue()
        {
            var document = new JsonArray
                               {
                                   Values =
                                       {
                                           new JsonNull()
                                       }
                               };

            Assert.True(document.IsNull(0));
        }

        [Fact]
        public void op_Number_int()
        {
            var expected = new JsonNumber("value");

            var document = new JsonArray
                               {
                                   Values =
                                       {
                                           expected
                                       }
                               };

            var actual = document.Number(0);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Number_int_whenNullValue()
        {
            var document = new JsonArray
                               {
                                   Values =
                                       {
                                           new JsonNull()
                                       }
                               };

            Assert.Null(document.Number(0));
        }

        [Fact]
        public void op_Object_int()
        {
            var expected = new JsonObject();

            var document = new JsonArray
                               {
                                   Values =
                                       {
                                           expected
                                       }
                               };

            var actual = document.Object(0);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Object_int_whenNullValue()
        {
            var document = new JsonArray
                               {
                                   Values =
                                       {
                                           new JsonNull()
                                       }
                               };

            Assert.Null(document.Object(0));
        }

        [Fact]
        public void op_String_int()
        {
            var expected = new JsonString("value");

            var document = new JsonArray
                               {
                                   Values =
                                       {
                                           expected
                                       }
                               };

            var actual = document.String(0);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_String_int_whenNullValue()
        {
            var document = new JsonArray
                               {
                                   Values =
                                       {
                                           new JsonNull()
                                       }
                               };

            Assert.Null(document.String(0));
        }

        [Fact]
        public void prop_Values()
        {
            Assert.True(new PropertyExpectations<JsonArray>(x => x.Values)
                            .TypeIs<IList<JsonValue>>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}