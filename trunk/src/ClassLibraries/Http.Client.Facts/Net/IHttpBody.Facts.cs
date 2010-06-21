namespace Cavity.Net
{
    using System;
    using System.IO;
    using Xunit;

    public sealed class IHttpBodyFacts
    {
        [Fact]
        public void type_definition()
        {
            Assert.True(typeof(IHttpBody).IsInterface);
        }

        [Fact]
        public void IHttpBody_Read_StreamReader()
        {
            try
            {
                (new IHttpBodyDummy() as IHttpBody).Read(null as StreamReader);
                Assert.True(false);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}