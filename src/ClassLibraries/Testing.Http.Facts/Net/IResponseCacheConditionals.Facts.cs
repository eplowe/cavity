namespace Cavity.Net
{
    using System;
    using Xunit;

    public sealed class IResponseCacheConditionalsFacts
    {
        [Fact]
        public void IResponseCacheConditionals_op_IgnoreCacheConditionals()
        {
            try
            {
                var value = (new IResponseCacheConditionalsDummy() as IResponseCacheConditionals).IgnoreCacheConditionals();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseCacheConditionals_op_WithEtag()
        {
            try
            {
                var value = (new IResponseCacheConditionalsDummy() as IResponseCacheConditionals).WithEtag();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseCacheConditionals_op_WithExpires()
        {
            try
            {
                var value = (new IResponseCacheConditionalsDummy() as IResponseCacheConditionals).WithExpires();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseCacheConditionals_op_WithLastModified()
        {
            try
            {
                var value = (new IResponseCacheConditionalsDummy() as IResponseCacheConditionals).WithLastModified();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IResponseCacheConditionals).IsInterface);
        }
    }
}