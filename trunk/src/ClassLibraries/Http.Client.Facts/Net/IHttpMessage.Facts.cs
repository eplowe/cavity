namespace Cavity.Net
{
    using System;
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
                var value = (new IHttpMessageDummy() as IHttpMessage).Body;
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IHttpMessage_Headers_get()
        {
            try
            {
                var value = (new IHttpMessageDummy() as IHttpMessage).Headers;
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IHttpMessage_Read_TextReader()
        {
            try
            {
                (new IHttpMessageDummy() as IHttpMessage).Read(null);
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IHttpMessage_Write_TextWriter()
        {
            try
            {
                (new IHttpMessageDummy() as IHttpMessage).Write(null);
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}