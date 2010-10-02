namespace Cavity.Net
{
    using System;
    using Xunit;

    public sealed class IRequestMethodFacts
    {
        [Fact]
        public void IRequestMethod_op_Delete()
        {
            try
            {
                var value = (new IRequestMethodDummy() as IRequestMethod).Delete();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IRequestMethod_op_Get()
        {
            try
            {
                var value = (new IRequestMethodDummy() as IRequestMethod).Get();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IRequestMethod_op_Get_bool()
        {
            try
            {
                const bool head = false;
                var value = (new IRequestMethodDummy() as IRequestMethod).Get(head);
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IRequestMethod_op_Head()
        {
            try
            {
                var value = (new IRequestMethodDummy() as IRequestMethod).Head();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IRequestMethod_op_Options()
        {
            try
            {
                var value = (new IRequestMethodDummy() as IRequestMethod).Options();
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IRequestMethod_op_Post_IHttpContent()
        {
            try
            {
                IHttpContent content = null;
                var value = (new IRequestMethodDummy() as IRequestMethod).Post(content);
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IRequestMethod_op_Put_IHttpContent()
        {
            try
            {
                IHttpContent content = null;
                var value = (new IRequestMethodDummy() as IRequestMethod).Put(content);
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IRequestMethod_op_Use_string()
        {
            try
            {
                string method = null;
                var value = (new IRequestMethodDummy() as IRequestMethod).Use(method);
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IRequestMethod_op_Use_string_bool()
        {
            try
            {
                string method = null;
                const bool head = false;
                var value = (new IRequestMethodDummy() as IRequestMethod).Use(method, head);
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void IRequestMethod_op_Use_string_IHttpContent()
        {
            try
            {
                string method = null;
                IHttpContent content = null;
                var value = (new IRequestMethodDummy() as IRequestMethod).Use(method, content);
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(IRequestMethod).IsInterface);
        }
    }
}