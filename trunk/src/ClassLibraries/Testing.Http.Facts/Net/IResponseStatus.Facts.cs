namespace Cavity.Net
{
    using System;
    using System.Globalization;
    using System.Net;
    using Xunit;

    public sealed class IResponseStatusFacts
    {
        [Fact]
        public void IResponseStatus_op_Is_HttpStatusCode()
        {
            try
            {
                const HttpStatusCode status = 0;
                var value = (new IResponseStatusDummy() as IResponseStatus).Is(status);
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseStatus_op_Is_HttpStatusCode_bool()
        {
            try
            {
                const HttpStatusCode status = 0;
                const bool hasVersionHeaders = false;
                var value = (new IResponseStatusDummy() as IResponseStatus).Is(status, hasVersionHeaders);
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseStatus_op_IsContentNegotiation_string()
        {
            try
            {
                string extension = null;
                var value = (new IResponseStatusDummy() as IResponseStatus).IsContentNegotiation(extension);
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseStatus_op_IsContentNegotiationToTextHtml()
        {
            try
            {
                var value = (new IResponseStatusDummy() as IResponseStatus).IsContentNegotiationToTextHtml();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseStatus_op_IsLanguageNegotiation_CultureInfo()
        {
            try
            {
                CultureInfo language = null;
                var value = (new IResponseStatusDummy() as IResponseStatus).IsLanguageNegotiation(language);
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseStatus_op_IsLanguageNegotiation_string()
        {
            try
            {
                string language = null;
                var value = (new IResponseStatusDummy() as IResponseStatus).IsLanguageNegotiation(language);
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseStatus_op_IsLanguageNegotiationToEnglish()
        {
            try
            {
                var value = (new IResponseStatusDummy() as IResponseStatus).IsLanguageNegotiationToEnglish();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseStatus_op_IsSeeOther_AbsoluteUri()
        {
            try
            {
                AbsoluteUri location = null;
                var value = (new IResponseStatusDummy() as IResponseStatus).IsSeeOther(location);
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IResponseStatus).IsInterface);
        }
    }
}