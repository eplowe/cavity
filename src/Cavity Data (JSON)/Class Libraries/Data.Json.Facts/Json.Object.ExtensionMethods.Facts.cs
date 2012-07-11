namespace Cavity
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Xml;

    using Cavity.Testing;
    using Cavity.Xml;

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
        [InlineData("[]")]
        public void op_JsonSerialize_object_whenArrayOfNull(string expected)
        {
            var obj = new List<object>
                          {
                              null, 
                              null
                          };
            var actual = obj.ToArray().JsonSerialize();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("[{}]")]
        public void op_JsonSerialize_object_whenArrayOfObjectAndNull(string expected)
        {
            var obj = new List<object>
                          {
                              new object(), 
                              null
                          };
            var actual = obj.ToArray().JsonSerialize();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("[{},{}]")]
        public void op_JsonSerialize_object_whenArrayOfObjectType(string expected)
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
        [InlineData("[{}]")]
        public void op_JsonSerialize_object_whenArrayOfValueType(string expected)
        {
            var obj = new List<object>
                          {
                              123
                          };
            var actual = obj.ToArray().JsonSerialize();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("[{\"count\":2,\"items\":[true,false]}]")]
        public void op_JsonSerialize_object_whenCollectionOfBoolean(string expected)
        {
            var obj = new Collection<bool>
                          {
                              true, 
                              false
                          };
            var actual = obj.JsonSerialize();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("[{\"count\":2,\"items\":[1,255]}]")]
        public void op_JsonSerialize_object_whenCollectionOfByte(string expected)
        {
            var obj = new Collection<byte>
                          {
                              1, 
                              255
                          };
            var actual = obj.JsonSerialize();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("[{\"count\":2,\"items\":[\"a\",\"z\"]}]")]
        public void op_JsonSerialize_object_whenCollectionOfChar(string expected)
        {
            var obj = new Collection<char>
                          {
                              'a', 
                              'z'
                          };
            var actual = obj.JsonSerialize();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("[{\"count\":2,\"items\":[\"1999-12-31T00:00:00Z\",\"2000-01-01T00:00:00Z\"]}]")]
        public void op_JsonSerialize_object_whenCollectionOfDateTime(string expected)
        {
            var obj = new Collection<DateTime>
                          {
                              new DateTime(1999, 12, 31), 
                              new DateTime(2000, 1, 1)
                          };
            var actual = obj.JsonSerialize();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("[{\"count\":2,\"items\":[\"1999-12-31T00:00:00Z\",\"2000-01-01T00:00:00Z\"]}]")]
        public void op_JsonSerialize_object_whenCollectionOfDateTimeOffset(string expected)
        {
            var obj = new Collection<DateTimeOffset>
                          {
                              new DateTime(1999, 12, 31), 
                              new DateTime(2000, 1, 1)
                          };
            var actual = obj.JsonSerialize();

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
        [InlineData("[{\"count\":2,\"items\":[1.23,4.56]}]")]
        public void op_JsonSerialize_object_whenCollectionOfDouble(string expected)
        {
            var obj = new Collection<double>
                          {
                              1.23d, 
                              4.56d
                          };
            var actual = obj.JsonSerialize();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("[{\"count\":2,\"items\":[\"Monday\",\"Friday\"]}]")]
        public void op_JsonSerialize_object_whenCollectionOfEnum(string expected)
        {
            var obj = new Collection<DayOfWeek>
                          {
                              DayOfWeek.Monday, 
                              DayOfWeek.Friday
                          };
            var actual = obj.JsonSerialize();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("[{\"count\":2,\"items\":[\"b75a9ea5-01ca-4a61-b299-8d0823a4a64a\",\"ce6a8e72-b5dc-4682-b9ce-23351d9d2f4a\"]}]")]
        public void op_JsonSerialize_object_whenCollectionOfGuid(string expected)
        {
            var obj = new Collection<Guid>
                          {
                              XmlConvert.ToGuid("b75a9ea5-01ca-4a61-b299-8d0823a4a64a"), 
                              XmlConvert.ToGuid("ce6a8e72-b5dc-4682-b9ce-23351d9d2f4a")
                          };
            var actual = obj.JsonSerialize();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("[{\"count\":2,\"items\":[123,456]}]")]
        public void op_JsonSerialize_object_whenCollectionOfInt16(string expected)
        {
            var obj = new Collection<short>
                          {
                              123, 
                              456
                          };
            var actual = obj.JsonSerialize();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("[{\"count\":2,\"items\":[1234,5678]}]")]
        public void op_JsonSerialize_object_whenCollectionOfInt32(string expected)
        {
            var obj = new Collection<int>
                          {
                              1234, 
                              5678
                          };
            var actual = obj.JsonSerialize();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("[{\"count\":2,\"items\":[12345,67890]}]")]
        public void op_JsonSerialize_object_whenCollectionOfInt64(string expected)
        {
            var obj = new Collection<long>
                          {
                              12345, 
                              67890
                          };
            var actual = obj.JsonSerialize();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("[{\"count\":2,\"items\":[null,null]}]")]
        public void op_JsonSerialize_object_whenCollectionOfNull(string expected)
        {
            var obj = new Collection<object>
                          {
                              null, 
                              null
                          };
            var actual = obj.JsonSerialize();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("[{\"count\":2,\"items\":[{\"prefix\":\"com\",\"uri\":\"http://example.com/\"},{\"prefix\":\"net\",\"uri\":\"http://example.net/\"}]}]")]
        public void op_JsonSerialize_object_whenCollectionOfObject(string expected)
        {
            var obj = new Collection<XmlNamespace>
                          {
                              new XmlNamespace("com", "http://example.com"), 
                              new XmlNamespace("net", "http://example.net")
                          };
            var actual = obj.JsonSerialize();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("[{\"count\":2,\"items\":[\"System.Object\",\"System.Object\"]}]")]
        public void op_JsonSerialize_object_whenCollectionOfObjectType(string expected)
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
        [InlineData("[{\"count\":2,\"items\":[1.2300000190734863,4.559999942779541]}]")]
        public void op_JsonSerialize_object_whenCollectionOfSingle(string expected)
        {
            var obj = new Collection<float>
                          {
                              1.23f, 
                              4.56f
                          };
            var actual = obj.JsonSerialize();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("[{\"count\":2,\"items\":[\"abc\",\"xyz\"]}]")]
        public void op_JsonSerialize_object_whenCollectionOfString(string expected)
        {
            var obj = new Collection<string>
                          {
                              "abc", 
                              "xyz"
                          };
            var actual = obj.JsonSerialize();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("[{\"count\":2,\"items\":[\"PT1H2M3S\",\"PT4H5M6S\"]}]")]
        public void op_JsonSerialize_object_whenCollectionOfTimeSpan(string expected)
        {
            var obj = new Collection<TimeSpan>
                          {
                              new TimeSpan(1, 2, 3), 
                              new TimeSpan(4, 5, 6)
                          };
            var actual = obj.JsonSerialize();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("[{\"count\":2,\"items\":[\"http://example.com/\",\"/path\"]}]")]
        public void op_JsonSerialize_object_whenCollectionOfUri(string expected)
        {
            var obj = new Collection<Uri>
                          {
                              new Uri("http://example.com"), 
                              new Uri("/path", UriKind.Relative)
                          };
            var actual = obj.JsonSerialize();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("[{\"count\":2,\"items\":[{\"day\":31,\"month\":12,\"year\":1999},{\"day\":1,\"month\":1,\"year\":2000}]}]")]
        public void op_JsonSerialize_object_whenCollectionOfValueType(string expected)
        {
            var obj = new Collection<Date>
                          {
                              new Date(1999, 12, 31), 
                              new Date(2000, 1, 1)
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