namespace Cavity.Net
{
    using System;
    using Cavity.Net.Mime;
    using Xunit;

    public sealed class IHttpMessageFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(typeof(IHttpMessage).IsInterface);
        }

        [Fact]
        public void IHttpMessage_Body_get()
        {
            try
            {
                IContent value = (new IHttpMessageDummy() as IHttpMessage).Body;
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IHttpHeaderCollection_Headers_get()
        {
            try
            {
                IHttpHeaderCollection value = (new IHttpMessageDummy() as IHttpMessage).Headers;
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}