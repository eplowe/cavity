namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Net;

    using Cavity.Collections;

    using Xunit;
    using Xunit.Extensions;

    public sealed class CsvUriAttributeFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<CsvUriAttribute>()
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
            Assert.NotNull(new CsvUriAttribute("example.csv"));
        }

        [Fact]
        public void ctor_stringsEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new CsvUriAttribute(new List<string>().ToArray()));
        }

        [Fact]
        public void ctor_stringsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new CsvUriAttribute(null));
        }

        [Fact]
        public void op_Download_AbsoluteUriNull()
        {
            Assert.Throws<ArgumentNullException>(() => CsvUriAttribute.Download(null));
        }

        [Fact]
        public void op_Download_AbsoluteUriNotFound()
        {
            Assert.Throws<WebException>(() => CsvUriAttribute.Download("http://www.alan-dean.com/missing.csv"));
        }

        [Fact]
        public void op_Download_AbsoluteUri()
        {
            const string expected = "A1";
            var actual = CsvUriAttribute.Download("http://www.alan-dean.com/example.csv").First()["A"];

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_GetData_MethodInfoNull_Types()
        {
            var obj = new CsvUriAttribute("example.csv");

            Assert.Throws<ArgumentNullException>(() => obj.GetData(null, new[] { typeof(CsvFile) }).ToList());
        }

        [Fact]
        public void op_GetData_MethodInfo_TypesNull()
        {
            var obj = new CsvUriAttribute("http://www.alan-dean.com/example.csv");

            Assert.Throws<ArgumentNullException>(() => obj.GetData(GetType().GetMethod("usage"), null).ToList());
        }

        [Fact]
        public void op_GetData_MethodInfo_Types_whenInvalidParameterType()
        {
            var obj = new CsvUriAttribute("http://www.alan-dean.com/example.csv");

            Assert.Throws<InvalidOperationException>(() => obj.GetData(GetType().GetMethod("usage"), new[] { typeof(string) }).ToList());
        }

        [Fact]
        public void op_GetData_MethodInfo_Types_whenParameterCountMismatch()
        {
            var obj = new CsvUriAttribute("http://www.alan-dean.com/one.csv", "http://www.alan-dean.com/two.csv");

            Assert.Throws<InvalidOperationException>(() => obj.GetData(GetType().GetMethod("usage"), new[] { typeof(CsvFile) }).ToList());
        }

        [Fact]
        public void prop_FileName()
        {
            Assert.True(new PropertyExpectations<CsvUriAttribute>(x => x.Locations)
                            .TypeIs<IEnumerable<string>>()
                            .IsNotDecorated()
                            .Result);
        }

        [Theory]
        [CsvUri("http://www.alan-dean.com/example.csv")]
        public void usage(CsvFile csv)
        {
            Assert.Equal("A1", csv.First()["A"]);
            Assert.Equal("B2", csv.Last()["B"]);
        }

        [Theory]
        [CsvUri("http://www.alan-dean.com/one.csv", "http://www.alan-dean.com/two.csv")]
        public void usage_whenDataSet(DataSet data)
        {
            if (null == data)
            {
                throw new ArgumentNullException("data");
            }

            Assert.Equal("one", data.Tables[0].Rows[0].Field<string>("COLUMN"));
            Assert.Equal("two", data.Tables[1].Rows[0].Field<string>("COLUMN"));
        }

        [Theory]
        [CsvUri("http://www.alan-dean.com/example.csv")]
        [CsvUri("http://www.alan-dean.com/example.csv")]
        public void usage_whenDataTable(DataTable table)
        {
            if (null == table)
            {
                throw new ArgumentNullException("table");
            }

            Assert.Equal("A1", table.Rows[0].Field<string>("A"));
            Assert.Equal("B2", table.Rows[1].Field<string>("B"));
        }

        [Theory]
        [CsvUri("http://www.alan-dean.com/example.csv")]
        public void usage_whenIEnumerableParameter(IEnumerable<KeyStringDictionary> data)
        {
            // ReSharper disable PossibleMultipleEnumeration
            Assert.Equal("A1", data.First()["A"]);
            Assert.Equal("B2", data.Last()["B"]);

            // ReSharper restore PossibleMultipleEnumeration
        }

        [Theory]
        [CsvUri("http://www.alan-dean.com/one.csv", "http://www.alan-dean.com/two.csv")]
        public void usage_whenMultipleParameters(CsvFile one, 
                                                 CsvFile two)
        {
            Assert.Equal("one", one.First()["COLUMN"]);
            Assert.Equal("two", two.First()["COLUMN"]);
        }
    }
}