namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using System.Xml.XPath;

    using Cavity.IO;
    using Cavity.Xml.XPath;

    using HtmlAgilityPack;

    using Xunit;
    using Xunit.Extensions;

    public sealed class HtmlFileAttributeFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<HtmlFileAttribute>()
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
            Assert.NotNull(new HtmlFileAttribute("example.html"));
        }

        [Fact]
        public void ctor_stringsEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new HtmlFileAttribute(new List<string>().ToArray()));
        }

        [Fact]
        public void ctor_stringsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new HtmlFileAttribute(null));
        }

        [Fact]
        public void op_GetData_MethodInfoNull_Types()
        {
            var obj = new HtmlFileAttribute("example.html");

            Assert.Throws<ArgumentNullException>(() => obj.GetData(null, new[] { typeof(XmlDocument) }).ToList());
        }

        [Fact]
        public void op_GetData_MethodInfo_TypesNull()
        {
            var obj = new HtmlFileAttribute("example.html");

            Assert.Throws<ArgumentNullException>(() => obj.GetData(GetType().GetMethod("usage"), null).ToList());
        }

        [Fact]
        public void op_GetData_MethodInfo_Types_whenInvalidParameterType()
        {
            var obj = new HtmlFileAttribute("example.html");

            Assert.Throws<InvalidOperationException>(() => obj.GetData(GetType().GetMethod("usage"), new[] { typeof(string) }).ToList());
        }

        [Fact]
        public void op_GetData_MethodInfo_Types_whenParameterCountMismatch()
        {
            var obj = new HtmlFileAttribute("one.html", "two.html");

            Assert.Throws<InvalidOperationException>(() => obj.GetData(GetType().GetMethod("usage"), new[] { typeof(XmlDocument) }).ToList());
        }

        [Fact]
        public void prop_FileName()
        {
            Assert.True(new PropertyExpectations<HtmlFileAttribute>(x => x.Files)
                            .TypeIs<IEnumerable<string>>()
                            .IsNotDecorated()
                            .Result);
        }

        [Theory]
        [HtmlFile("example.html")]
        public void usage(HtmlDocument html)
        {
            var expected = new FileInfo("example.html").ReadToEnd();
            var actual = html.CreateNavigator().OuterXml;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [HtmlFile("tabular.html")]
        public void usage_whenDataSet(DataSet data)
        {
            const string expected = "1A";
            var actual = data.Tables["example"].Rows[0].Field<HtmlNode>("Column A").InnerText;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [HtmlFile("example.html")]
        [HtmlFile("example.html")]
        public void usage_whenIXPathNavigable(IXPathNavigable xml)
        {
            var expected = new FileInfo("example.html").ReadToEnd();
            var actual = xml.CreateNavigator().OuterXml;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [HtmlFile("one.html", "two.html")]
        public void usage_whenMultipleParameters(HtmlDocument one, 
                                                 HtmlDocument two)
        {
            // ReSharper disable PossibleNullReferenceException
            Assert.Equal("One", one.DocumentNode.SelectSingleNode("//title").InnerText);
            Assert.Equal("Two", two.DocumentNode.SelectSingleNode("//title").InnerText);

            // ReSharper restore PossibleNullReferenceException
        }

        [Theory]
        [HtmlFile("example.html")]
        public void usage_whenXPathNavigator(XPathNavigator navigator)
        {
            Assert.True(navigator.Evaluate<bool>("1 = count(/html/head/title[text()='Example'])"));
            Assert.True(navigator.Evaluate<bool>("1 = count(/html/body/h1[text()='Example'])"));
        }
    }
}