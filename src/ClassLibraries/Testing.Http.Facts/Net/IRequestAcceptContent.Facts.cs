namespace Cavity.Net
{
    using System;
    using Xunit;

    public sealed class IRequestAcceptContentFacts
    {
        [Fact]
        public void IRequestAcceptContent_op_AcceptAnyContent()
        {
            try
            {
                var obj = (new IRequestAcceptContentDummy() as IRequestAcceptContent).AcceptAnyContent();
                Assert.NotNull(obj);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IRequestAcceptContent_op_Accept_string()
        {
            try
            {
                string value = null;
                var obj = (new IRequestAcceptContentDummy() as IRequestAcceptContent).Accept(value);
                Assert.NotNull(obj);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IRequestAcceptContent).IsInterface);
        }
    }
}