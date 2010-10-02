namespace Cavity.Net
{
    using System;
    using System.Net.Mime;
    using Xunit;

    public sealed class IResponseContentFacts
    {
        [Fact]
        public void IResponseContent_op_ResponseHasNoContent()
        {
            try
            {
                var value = (new IResponseContentDummy() as IResponseContent).ResponseHasNoContent();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseContent_op_ResponseIsApplicationJson()
        {
            try
            {
                var value = (new IResponseContentDummy() as IResponseContent).ResponseIsApplicationJson();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseContent_op_ResponseIsApplicationJson_ContentType()
        {
            try
            {
                ContentType type = null;
                var value = (new IResponseContentDummy() as IResponseContent).ResponseIsApplicationJson(type);
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseContent_op_ResponseIsApplicationXhtml()
        {
            try
            {
                var value = (new IResponseContentDummy() as IResponseContent).ResponseIsApplicationXhtml();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseContent_op_ResponseIsApplicationXml()
        {
            try
            {
                var value = (new IResponseContentDummy() as IResponseContent).ResponseIsApplicationXml();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseContent_op_ResponseIsApplicationXml_ContentType()
        {
            try
            {
                ContentType type = null;
                var value = (new IResponseContentDummy() as IResponseContent).ResponseIsApplicationXml(type);
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseContent_op_ResponseIsImageIcon()
        {
            try
            {
                var value = (new IResponseContentDummy() as IResponseContent).ResponseIsImageIcon();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseContent_op_ResponseIsTextCss()
        {
            try
            {
                var value = (new IResponseContentDummy() as IResponseContent).ResponseIsTextCss();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseContent_op_ResponseIsTextHtml()
        {
            try
            {
                var value = (new IResponseContentDummy() as IResponseContent).ResponseIsTextHtml();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseContent_op_ResponseIsTextJavaScript()
        {
            try
            {
                var value = (new IResponseContentDummy() as IResponseContent).ResponseIsTextJavaScript();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseContent_op_ResponseIsTextPlain()
        {
            try
            {
                var value = (new IResponseContentDummy() as IResponseContent).ResponseIsTextPlain();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IResponseContent).IsInterface);
        }
    }
}