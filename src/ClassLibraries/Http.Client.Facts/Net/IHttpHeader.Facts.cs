namespace Cavity.Net
{
    using System;
    using Xunit;

    public sealed class IHttpHeaderFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IHttpHeader).IsInterface);
        }

        [Fact]
        public void IHttpHeader_Name_get()
        {
            try
            {
                var value = (new IHttpHeaderDummy() as IHttpHeader).Name;
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IHttpHeader_Value_get()
        {
            try
            {
                var value = (new IHttpHeaderDummy() as IHttpHeader).Value;
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}