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