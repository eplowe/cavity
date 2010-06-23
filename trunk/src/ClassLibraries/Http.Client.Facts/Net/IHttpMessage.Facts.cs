namespace Cavity.Net
{
    using System;
    using System.IO;
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
        public void IHttpMessage_Headers_get()
        {
            try
            {
                HttpHeaderCollection value = (new IHttpMessageDummy() as IHttpMessage).Headers;
                Assert.True(false);
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
                (new IHttpMessageDummy() as IHttpMessage).Read(null as TextReader);
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
                (new IHttpMessageDummy() as IHttpMessage).Write(null as TextWriter);
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}