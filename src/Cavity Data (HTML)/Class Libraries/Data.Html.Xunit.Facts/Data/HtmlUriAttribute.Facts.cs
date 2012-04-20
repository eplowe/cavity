namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Net;
    using System.Xml;
    using System.Xml.XPath;

    using Cavity.Xml.XPath;

    using HtmlAgilityPack;

    using Xunit;
    using Xunit.Extensions;

    public sealed class HtmlUriAttributeFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HtmlUriAttribute>()
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
            Assert.NotNull(new HtmlUriAttribute("http://www.alan-dean.com/example.html"));
        }

        [Fact]
        public void ctor_stringsEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new HtmlUriAttribute(new List<string>().ToArray()));
        }

        [Fact]
        public void ctor_stringsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HtmlUriAttribute(null));
        }

        [Fact]
        public void op_Download_AbsoluteUri()
        {
            var navigator = HtmlUriAttribute.Download("http://www.alan-dean.com/example.html").CreateNavigator();

            Assert.True(navigator.Evaluate<bool>("1 = count(//title[text()='Example'])"));
        }

        [Fact]
        public void op_Download_AbsoluteUriNotFound()
        {
            Assert.Throws<WebException>(() => HtmlUriAttribute.Download("http://www.alan-dean.com/missing.html"));
        }

        [Fact]
        public void op_Download_AbsoluteUriNull()
        {
            Assert.Throws<ArgumentNullException>(() => HtmlUriAttribute.Download(null));
        }

        [Fact]
        public void op_GetData_MethodInfoNull_Types()
        {
            var obj = new HtmlUriAttribute("http://www.alan-dean.com/example.html");

            Assert.Throws<ArgumentNullException>(() => obj.GetData(null, new[] { typeof(XmlDocument) }).ToList());
        }

        [Fact]
        public void op_GetData_MethodInfo_TypesNull()
        {
            var obj = new HtmlUriAttribute("http://www.alan-dean.com/example.html");

            Assert.Throws<ArgumentNullException>(() => obj.GetData(GetType().GetMethod("usage"), null).ToList());
        }

        [Fact]
        public void op_GetData_MethodInfo_Types_whenInvalidParameterType()
        {
            var obj = new HtmlUriAttribute("http://www.alan-dean.com/example.html");

            Assert.Throws<InvalidOperationException>(() => obj.GetData(GetType().GetMethod("usage"), new[] { typeof(string) }).ToList());
        }

        [Fact]
        public void op_GetData_MethodInfo_Types_whenParameterCountMismatch()
        {
            var obj = new HtmlUriAttribute("http://www.alan-dean.com/one.html", "http://www.alan-dean.com/two.html");

            Assert.Throws<InvalidOperationException>(() => obj.GetData(GetType().GetMethod("usage"), new[] { typeof(XmlDocument) }).ToList());
        }

        [Fact]
        public void prop_FileName()
        {
            Assert.True(new PropertyExpectations<HtmlUriAttribute>(x => x.Locations)
                            .TypeIs<IEnumerable<string>>()
                            .IsNotDecorated()
                            .Result);
        }

        [Theory]
        [HtmlUri("http://www.alan-dean.com/example.html")]
        public void usage(HtmlDocument html)
        {
            Assert.True(html.CreateNavigator().Evaluate<bool>("1 = count(//title[text()='Example'])"));
        }

        [Theory]
        [HtmlUri("http://www.alan-dean.com/tabular.html")]
        public void usage_whenDataSet(DataSet data)
        {
            const string expected = "1A";
            var actual = data.Tables["example"].Rows[0].Field<HtmlNode>("Column A").InnerText;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [HtmlUri("http://www.alan-dean.com/example.html")]
        [HtmlUri("http://www.alan-dean.com/example.html")]
        public void usage_whenIXPathNavigable(IXPathNavigable xml)
        {
            Assert.True(xml.CreateNavigator().Evaluate<bool>("1 = count(//title[text()='Example'])"));
        }

        [Theory]
        [HtmlUri("http://www.alan-dean.com/one.html", "http://www.alan-dean.com/two.html")]
        public void usage_whenMultipleParameters(HtmlDocument one, 
                                                 HtmlDocument two)
        {
            // ReSharper disable PossibleNullReferenceException
            Assert.Equal("One", one.DocumentNode.SelectSingleNode("//title").InnerText);
            Assert.Equal("Two", two.DocumentNode.SelectSingleNode("//title").InnerText);

            // ReSharper restore PossibleNullReferenceException
        }

        [Theory]
        [HtmlUri("http://www.alan-dean.com/example.html")]
        public void usage_whenXPathNavigator(XPathNavigator navigator)
        {
            Assert.True(navigator.Evaluate<bool>("1 = count(/html/head/title[text()='Example'])"));
            Assert.True(navigator.Evaluate<bool>("1 = count(/html/body/h1[text()='Example'])"));
        }

        [Theory]
        [HtmlUri("http://developer.yahoo.com/yui/examples/datasource/datasource_table_to_array.html")]
        public void usage_whenYahooExample(DataSet data)
        {
            var table = data.Tables["accounts"];
            Assert.Equal(7, table.Rows.Count);

            var row = table.Rows[0];

            Assert.Equal("1/23/1999", row.Field<HtmlNode>("Due Date").InnerText);
            Assert.Equal("29e8548592d8c82", row.Field<HtmlNode>("Account Number").InnerText);
            Assert.Equal("12", row.Field<HtmlNode>("Quantity").InnerText);
            Assert.Equal("$150.00", row.Field<HtmlNode>("Amount Due").InnerText);
        }
    }
}