namespace Cavity.Net
{
    using System;
    using Xunit;

    public sealed class IResponseCacheControlFacts
    {
        [Fact]
        public void IResponseCacheControl_op_HasCacheControlNone()
        {
            try
            {
                var value = (new IResponseCacheControlDummy() as IResponseCacheControl).HasCacheControlNone();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseCacheControl_op_HasCacheControlPrivate()
        {
            try
            {
                var value = (new IResponseCacheControlDummy() as IResponseCacheControl).HasCacheControlPrivate();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseCacheControl_op_HasCacheControlPublic()
        {
            try
            {
                var value = (new IResponseCacheControlDummy() as IResponseCacheControl).HasCacheControlPublic();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseCacheControl_op_HasCacheControl_string()
        {
            try
            {
                string value = null;
                var obj = (new IResponseCacheControlDummy() as IResponseCacheControl).HasCacheControl(value);
                Assert.NotNull(obj);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IResponseCacheControl_op_IgnoreCacheControl()
        {
            try
            {
                var value = (new IResponseCacheControlDummy() as IResponseCacheControl).IgnoreCacheControl();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IResponseCacheControl).IsInterface);
        }
    }
}