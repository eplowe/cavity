namespace Cavity.Web
{
    using System;
    using System.Web;

    using Moq;

    using Xunit;

    public sealed class RedirectionModuleFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<RedirectionModule>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IHttpModule>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new RedirectionModule());
        }

        [Fact]
        public void op_Dispose()
        {
            new RedirectionModule().Dispose();
        }

        [Fact]
        public void op_Init_HttpApplication()
        {
            new RedirectionModule().Init(new Mock<HttpApplication>().Object);
        }

        [Fact]
        public void op_Init_HttpApplicationNull()
        {
            Assert.Throws<ArgumentNullException>(() => new RedirectionModule().Init(null));
        }

        [Fact]
        public void op_OnBeginRequest_objectNull_EventArgs()
        {
            Assert.Throws<ArgumentNullException>(() => new RedirectionModule().OnBeginRequest(null, EventArgs.Empty));
        }
    }
}