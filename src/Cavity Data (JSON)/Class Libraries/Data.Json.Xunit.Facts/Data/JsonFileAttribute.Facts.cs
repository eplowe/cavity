namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using Xunit;
    using Xunit.Extensions;

    public sealed class JsonFileAttributeFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<JsonFileAttribute>()
                            .DerivesFrom<DataAttribute>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .AttributeUsage(AttributeTargets.Method, false, true)
                            .Result);
        }

        [Fact]
        public void ctor_strings()
        {
            Assert.NotNull(new JsonFileAttribute("example.json"));
        }

        [Fact]
        public void ctor_stringsEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new JsonFileAttribute(new List<string>().ToArray()));
        }

        [Fact]
        public void ctor_stringsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new JsonFileAttribute(null));
        }

        [Fact]
        public void op_GetData_MethodInfoNull_Types()
        {
            var obj = new JsonFileAttribute("example.json");

            Assert.Throws<ArgumentNullException>(() => obj.GetData(null, new[] { typeof(JObject) }).ToList());
        }

        [Fact]
        public void op_GetData_MethodInfo_TypesNull()
        {
            var obj = new JsonFileAttribute("example.json");

            Assert.Throws<ArgumentNullException>(() => obj.GetData(GetType().GetMethod("usage"), null).ToList());
        }

        [Fact]
        public void op_GetData_MethodInfo_Types_whenInvalidParameterType()
        {
            var obj = new JsonFileAttribute("example.json");

            Assert.Throws<JsonReaderException>(() => obj.GetData(GetType().GetMethod("usage"), new[] { typeof(string) }).ToList());
        }

        [Fact]
        public void op_GetData_MethodInfo_Types_whenParameterCountMismatch()
        {
            var obj = new JsonFileAttribute("one.json", "two.json");

            Assert.Throws<InvalidOperationException>(() => obj.GetData(GetType().GetMethod("usage"), new[] { typeof(JObject) }).ToList());
        }

        [Fact]
        public void prop_FileName()
        {
            Assert.True(new PropertyExpectations<JsonFileAttribute>(x => x.Files)
                            .TypeIs<IEnumerable<string>>()
                            .IsNotDecorated()
                            .Result);
        }

        [Theory]
        [JsonFile("example.json")]
        public void usage(JObject obj)
        {
            Assert.Equal("999", obj["Value"]);
        }

        [Theory]
        [JsonFile("one.json", "two.json")]
        public void usage_whenMultipleParameters(Example one,
                                                 Example two)
        {
            Assert.Equal(1, one.Value);
            Assert.Equal(2, two.Value);
        }

        [Theory]
        [JsonFile("example.json")]
        public void usage_whenJsonDeserialize(Example example)
        {
            Assert.Equal(999, example.Value);
        }
    }
}