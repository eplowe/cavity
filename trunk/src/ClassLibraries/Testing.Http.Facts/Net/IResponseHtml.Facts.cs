namespace Cavity.Net
{
    using System;
    using Xunit;

    public sealed class IResponseHtmlFacts
    {
        [Fact]
        public void IResponseHtml_op_EvaluateFalse_string()
        {
            try
            {
                string[] xpaths = null;
                var value = (new IResponseHtmlDummy() as IResponseHtml).EvaluateFalse(xpaths);
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseHtml_op_EvaluateTrue_string()
        {
            try
            {
                string[] xpaths = null;
                var value = (new IResponseHtmlDummy() as IResponseHtml).EvaluateTrue(xpaths);
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseHtml_op_Evaluate_T_string()
        {
            try
            {
                const double expected = 1.23;
                string[] xpaths = null;
                var value = (new IResponseHtmlDummy() as IResponseHtml).Evaluate(expected, xpaths);
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseHtml_op_HasRobotsTag_string()
        {
            try
            {
                string value = null;
                var obj = (new IResponseHtmlDummy() as IResponseHtml).HasRobotsTag(value);
                Assert.NotNull(obj);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseHtml_op_HasStyleSheetLink_string()
        {
            try
            {
                string href = null;
                var value = (new IResponseHtmlDummy() as IResponseHtml).HasStyleSheetLink(href);
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IResponseHtml).IsInterface);
        }

        [Fact]
        public void is_ITestHttp()
        {
            Assert.True(typeof(IResponseHtml).Implements(typeof(ITestHttp)));
        }
    }
}