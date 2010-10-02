namespace Cavity.Net
{
    using System;
    using Xunit;

    public sealed class IResponseContentMD5Facts
    {
        [Fact]
        public void IResponseContentMD5_op_HasContentMD5()
        {
            try
            {
                var value = (new IResponseContentMD5Dummy() as IResponseContentMD5).HasContentMD5();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseContentMD5_op_IgnoreContentMD5()
        {
            try
            {
                var value = (new IResponseContentMD5Dummy() as IResponseContentMD5).IgnoreContentMD5();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IResponseContentMD5).IsInterface);
        }
    }
}