namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;

    using Cavity.IO;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using Xunit;
    using Xunit.Extensions;

    public sealed class JsonUriAttributeFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<JsonUriAttribute>()
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
            Assert.NotNull(new JsonUriAttribute("http://www.alan-dean.com/example.json"));
        }

        [Fact]
        public void ctor_stringsEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new JsonUriAttribute(new List<string>().ToArray()));
        }

        [Fact]
        public void ctor_stringsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new JsonUriAttribute(null));
        }

        [Fact]
        public void op_Download_AbsoluteUri()
        {
            var expected = new FileInfo("example.json").ReadToEnd();
            var actual = JsonUriAttribute.Download("http://www.alan-dean.com/example.json").ReadToEnd();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Download_AbsoluteUriNotFound()
        {
            Assert.Throws<WebException>(() => JsonUriAttribute.Download("http://www.alan-dean.com/missing.json"));
        }

        [Fact]
        public void op_Download_AbsoluteUriNull()
        {
            Assert.Throws<ArgumentNullException>(() => JsonUriAttribute.Download(null));
        }

        [Fact]
        public void op_GetData_MethodInfoNull_Types()
        {
            var obj = new JsonUriAttribute("http://www.alan-dean.com/example.json");

            Assert.Throws<ArgumentNullException>(() => obj.GetData(null, new[] { typeof(JObject) }).ToList());
        }

        [Fact]
        public void op_GetData_MethodInfo_TypesNull()
        {
            var obj = new JsonUriAttribute("http://www.alan-dean.com/example.json");

            Assert.Throws<ArgumentNullException>(() => obj.GetData(GetType().GetMethod("usage"), null).ToList());
        }

        [Fact]
        public void op_GetData_MethodInfo_Types_whenInvalidParameterType()
        {
            var obj = new JsonUriAttribute("http://www.alan-dean.com/example.json");

            Assert.Throws<JsonReaderException>(() => obj.GetData(GetType().GetMethod("usage"), new[] { typeof(string) }).ToList());
        }

        [Fact]
        public void op_GetData_MethodInfo_Types_whenParameterCountMismatch()
        {
            var obj = new JsonUriAttribute("http://www.alan-dean.com/one.json", "http://www.alan-dean.com/two.json");

            Assert.Throws<InvalidOperationException>(() => obj.GetData(GetType().GetMethod("usage"), new[] { typeof(JObject) }).ToList());
        }

        [Fact]
        public void prop_FileName()
        {
            Assert.True(new PropertyExpectations<JsonUriAttribute>(x => x.Locations)
                            .TypeIs<IEnumerable<string>>()
                            .IsNotDecorated()
                            .Result);
        }

        [Theory]
        [JsonUri("http://www.alan-dean.com/example.json")]
        [JsonUri("http://www.alan-dean.com/example.json")]
        public void usage(JObject obj)
        {
            Assert.Equal("999", obj["Value"]);
        }

        [Theory]
        [JsonUri("http://www.alan-dean.com/example.json")]
        public void usage_whenJsonDeserialize(Example example)
        {
            Assert.Equal(999, example.Value);
        }

        [Theory]
        [JsonUri("http://www.alan-dean.com/one.json", "http://www.alan-dean.com/two.json")]
        public void usage_whenMultipleParameters(Example one, 
                                                 Example two)
        {
            Assert.Equal(1, one.Value);
            Assert.Equal(2, two.Value);
        }
    }
}