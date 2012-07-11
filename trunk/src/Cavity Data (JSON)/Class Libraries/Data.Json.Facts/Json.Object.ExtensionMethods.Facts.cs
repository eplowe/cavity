namespace Cavity
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Xml;

    using Cavity.Testing;

    using Xunit;
    using Xunit.Extensions;

    public sealed class JsonObjectExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(JsonObjectExtensionMethods).IsStatic());
        }

        [Theory]
        [InlineData("{}")]
        public void op_JsonSerialize_object(string expected)
        {
            var actual = new object().JsonSerialize();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_JsonSerialize_objectNull()
        {
            Assert.Null((null as object).JsonSerialize());
        }

        [Theory]
        [InlineData("[{},{}]")]
        public void op_JsonSerialize_object_whenArrayObject(string expected)
        {
            var obj = new List<object>
                          {
                              new object(), 
                              new object()
                          };
            var actual = obj.ToArray().JsonSerialize();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("[{\"count\":2,\"items\":[1.23,4.56]}]")]
        public void op_JsonSerialize_object_whenCollectionOfDecimal(string expected)
        {
            var obj = new Collection<decimal>
                          {
                              1.23m, 
                              4.56m
                          };
            var actual = obj.JsonSerialize();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("[{\"count\":2,\"items\":[{},{}]}]")]
        public void op_JsonSerialize_object_whenCollectionOfObject(string expected)
        {
            var obj = new Collection<object>
                          {
                              new object(), 
                              new object()
                          };
            var actual = obj.JsonSerialize();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("{\"boolean\":true,\"byte\":123,\"character\":\"x\",\"date\":{\"day\":1,\"month\":1,\"year\":2000},\"day\":\"Monday\",\"decimal\":1.23,\"double\":1.23,\"duration\":\"PT1H2M3S\",\"int16\":123,\"int32\":123,\"int64\":123,\"offset\":\"1999-12-31T00:00:00Z\",\"single\":1,\"unique\":\"9ff20dc5-1a0e-47b2-a71c-43cbfa36201d\",\"value\":\"abc\",\"when\":\"2011-07-14T19:43:37Z\"}", true, 'x', 1.23d, "1.23", 123, "abc")]
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "boolean", Justification = "This is a test.")]
        [SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "integer", Justification = "This is a test.")]
        public void op_JsonSerialize_object_whenFundamentalTypes(string expected, 
                                                                 bool boolean, 
                                                                 char character, 
                                                                 double number, 
                                                                 string money, 
                                                                 int integer, 
                                                                 string value)
        {
            var example = new FundamentalTypes
                              {
                                  Boolean = boolean, 
                                  Byte = (byte)integer, 
                                  Character = character, 
                                  Day = DayOfWeek.Monday, 
                                  Date = new Date(2000, 1, 1), 
                                  Decimal = XmlConvert.ToDecimal(money), 
                                  Double = number, 
                                  Duration = new TimeSpan(1, 2, 3), 
                                  Single = (float)Math.Round(number, 0), 
                                  Int16 = (short)integer, 
                                  Int32 = integer, 
                                  Int64 = integer, 
                                  Unique = XmlConvert.ToGuid("9ff20dc5-1a0e-47b2-a71c-43cbfa36201d"), 
                                  Value = value, 
                                  When = XmlConvert.ToDateTime("2011-07-14T19:43:37Z", XmlDateTimeSerializationMode.Utc), 
                                  Offset = new DateTimeOffset(new DateTime(1999, 12, 31))
                              };

            var actual = example.JsonSerialize();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("null", null)]
        [InlineData("\"\"", "")]
        [InlineData("\" \"", " ")]
        [InlineData("\"abc\"", "abc")]
        public void op_JsonSerialize_object_whenString(string expected, 
                                                       string value)
        {
            var example = new FundamentalTypes
                              {
                                  Value = value
                              };

            var actual = example.JsonSerialize();

            Assert.Equal("{{\"boolean\":false,\"byte\":0,\"character\":null,\"date\":{{\"day\":1,\"month\":1,\"year\":1}},\"day\":\"Sunday\",\"decimal\":0,\"double\":0,\"duration\":\"PT0S\",\"int16\":0,\"int32\":0,\"int64\":0,\"offset\":\"0001-01-01T00:00:00Z\",\"single\":0,\"unique\":\"00000000-0000-0000-0000-000000000000\",\"value\":{0},\"when\":\"0001-01-01T00:00:00Z\"}}".FormatWith(expected), actual);
        }
    }
}