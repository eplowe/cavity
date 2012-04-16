namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    using Cavity.Collections;

    using Xunit;
    using Xunit.Extensions;

    public sealed class CsvFileAttributeFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<CsvFileAttribute>()
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
            Assert.NotNull(new CsvFileAttribute("example.csv"));
        }

        [Fact]
        public void ctor_stringsEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new CsvFileAttribute(new List<string>().ToArray()));
        }

        [Fact]
        public void ctor_stringsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new CsvFileAttribute(null));
        }

        [Fact]
        public void op_GetData_MethodInfoNull_Types()
        {
            var obj = new CsvFileAttribute("example.csv");

            Assert.Throws<ArgumentNullException>(() => obj.GetData(null, new[] { typeof(CsvFile) }).ToList());
        }

        [Fact]
        public void op_GetData_MethodInfo_TypesNull()
        {
            var obj = new CsvFileAttribute("example.csv");

            Assert.Throws<ArgumentNullException>(() => obj.GetData(GetType().GetMethod("usage"), null).ToList());
        }

        [Fact]
        public void op_GetData_MethodInfo_Types_whenInvalidParameterType()
        {
            var obj = new CsvFileAttribute("example.csv");

            Assert.Throws<InvalidOperationException>(() => obj.GetData(GetType().GetMethod("usage"), new[] { typeof(string) }).ToList());
        }

        [Fact]
        public void op_GetData_MethodInfo_Types_whenParameterCountMismatch()
        {
            var obj = new CsvFileAttribute("one.csv", "two.csv");

            Assert.Throws<InvalidOperationException>(() => obj.GetData(GetType().GetMethod("usage"), new[] { typeof(CsvFile) }).ToList());
        }

        [Fact]
        public void prop_FileName()
        {
            Assert.True(new PropertyExpectations<CsvFileAttribute>(x => x.Files)
                            .TypeIs<IEnumerable<string>>()
                            .IsNotDecorated()
                            .Result);
        }

        [Theory]
        [CsvFile("example.csv")]
        public void usage(CsvFile csv)
        {
            Assert.Equal("A1", csv.First()["A"]);
            Assert.Equal("B2", csv.Last()["B"]);
        }

        [Theory]
        [CsvFile("one.csv", "two.csv")]
        public void usage_whenDataSet(DataSet data)
        {
            if (null == data)
            {
                throw new ArgumentNullException("data");
            }

            Assert.Equal("one", data.Tables["one.csv"].Rows[0].Field<string>("COLUMN"));
            Assert.Equal("two", data.Tables["two.csv"].Rows[0].Field<string>("COLUMN"));
        }

        [Theory]
        [CsvFile("example.csv")]
        [CsvFile("example.csv")]
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
        [CsvFile("example.csv")]
        public void usage_whenIEnumerableParameter(IEnumerable<KeyStringDictionary> data)
        {
            // ReSharper disable PossibleMultipleEnumeration
            Assert.Equal("A1", data.First()["A"]);
            Assert.Equal("B2", data.Last()["B"]);

            // ReSharper restore PossibleMultipleEnumeration
        }

        [Theory]
        [CsvFile("one.csv", "two.csv")]
        public void usage_whenMultipleParameters(CsvFile one, 
                                                 CsvFile two)
        {
            Assert.Equal("one", one.First()["COLUMN"]);
            Assert.Equal("two", two.First()["COLUMN"]);
        }
    }
}