namespace Cavity.Net
{
    using System;
    using Cavity.Xml;
    using Xunit;

    public sealed class IResponseXmlFacts
    {
        [Fact]
        public void IResponseXml_op_EvaluateFalse()
        {
            try
            {
                string[] xpaths = null;
                var value = (new IResponseXmlDummy() as IResponseXml).EvaluateFalse(xpaths);
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseXml_op_EvaluateTrue()
        {
            try
            {
                string[] xpaths = null;
                var value = (new IResponseXmlDummy() as IResponseXml).EvaluateTrue(xpaths);
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseXml_op_Evaluate_T_string_XmlNamespaces()
        {
            try
            {
                const double expected = 1.23;
                string xpath = null;
                XmlNamespace[] namespaces = null;
                var value = (new IResponseXmlDummy() as IResponseXml).Evaluate(expected, xpath, namespaces);
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseXml_op_Evaluate_T_strings()
        {
            try
            {
                const double expected = 1.23;
                string[] xpaths = null;
                var value = (new IResponseXmlDummy() as IResponseXml).Evaluate(expected, xpaths);
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IResponseXml).IsInterface);
        }

        [Fact]
        public void is_ITestHttp()
        {
            Assert.True(typeof(IResponseXml).Implements(typeof(ITestHttp)));
        }
    }
}