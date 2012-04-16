namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using Xunit;
    using Xunit.Extensions;

    public sealed class JsonDataAttributeFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<JsonDataAttribute>()
                            .DerivesFrom<DataAttribute>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .AttributeUsage(AttributeTargets.Method, true, true)
                            .Result);
        }

        [Fact]
        public void ctor_strings()
        {
            Assert.NotNull(new JsonDataAttribute("{ \"Value\": 999 }"));
        }

        [Fact]
        public void ctor_stringsEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new JsonDataAttribute(new List<string>().ToArray()));
        }

        [Fact]
        public void ctor_stringsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new JsonDataAttribute(null));
        }

        [Fact]
        public void op_GetData_MethodInfoNull_Types()
        {
            var obj = new JsonDataAttribute("{ \"Value\": 999 }");

            Assert.Throws<ArgumentNullException>(() => obj.GetData(null, new[] { typeof(JObject) }).ToList());
        }

        [Fact]
        public void op_GetData_MethodInfo_TypesNull()
        {
            var obj = new JsonDataAttribute("{ \"Value\": 999 }");

            Assert.Throws<ArgumentNullException>(() => obj.GetData(GetType().GetMethod("usage"), null).ToList());
        }

        [Fact]
        public void op_GetData_MethodInfo_Types_whenInvalidParameterType()
        {
            var obj = new JsonDataAttribute("{ \"Value\": 999 }");

            Assert.Throws<JsonReaderException>(() => obj.GetData(GetType().GetMethod("usage"), new[] { typeof(string) }).ToList());
        }

        [Fact]
        public void op_GetData_MethodInfo_Types_whenParameterCountMismatch()
        {
            var obj = new JsonDataAttribute("{ \"Value\": 1 }", "{ \"Value\": 2 }");

            Assert.Throws<InvalidOperationException>(() => obj.GetData(GetType().GetMethod("usage"), new[] { typeof(JObject) }).ToList());
        }

        [Fact]
        public void prop_Values()
        {
            Assert.True(new PropertyExpectations<JsonDataAttribute>(x => x.Values)
                            .TypeIs<IEnumerable<string>>()
                            .IsNotDecorated()
                            .Result);
        }

        [Theory]
        [JsonData("{ \"Value\": 999 }")]
        [JsonData("{ \"Value\": 999 }")]
        public void usage(JObject obj)
        {
            Assert.Equal("999", obj["Value"]);
        }

        [Theory]
        [JsonData("{ \"Value\": 1 }", "{ \"Value\": 2 }")]
        public void usage_whenMultipleParameters(Example one,
                                                 Example two)
        {
            Assert.Equal(1, one.Value);
            Assert.Equal(2, two.Value);
        }

        [Theory]
        [JsonData("{ \"Value\": 999 }")]
        public void usage_whenJsonDeserialize(Example example)
        {
            Assert.Equal(999, example.Value);
        }
    }
}