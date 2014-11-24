namespace Cavity.Web.Mvc
{
    using System;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using Moq;
    using Xunit;

    public sealed class TextResultFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<TextResult>()
                            .DerivesFrom<ContentResult>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new TextResult());
        }

        [Fact]
        public void ctor_object()
        {
            Assert.NotNull(new TextResult("test"));
        }

        [Fact]
        public void ctor_objectNull()
        {
            Assert.NotNull(new TextResult(null));
        }

        [Fact]
        public void op_ExecuteResult_ControllerContext()
        {
            var response = new Mock<HttpResponseBase>(MockBehavior.Strict);
            response
                .SetupSet(x => x.ContentEncoding = Encoding.UTF8)
                .Verifiable();
            response
                .SetupSet(x => x.ContentType = "text/plain")
                .Verifiable();
            response
                .Setup(x => x.Write(It.IsAny<string>()))
                .Verifiable();

            var context = new Mock<HttpContextBase>(MockBehavior.Strict);
            context
                .Setup(x => x.Response)
                .Returns(response.Object)
                .Verifiable();

            new TextResult("test").ExecuteResult(new ControllerContext
                                                     {
                                                         HttpContext = context.Object
                                                     });

            context.VerifyAll();
        }

        [Fact]
        public void op_ExecuteResult_ControllerContextNull()
        {
            Assert.Throws<ArgumentNullException>(() => new NotAcceptableResult().ExecuteResult(null));
        }

        [Fact]
        public void prop_ContentEncoding_get()
        {
            Assert.Equal(Encoding.UTF8, new TextResult().ContentEncoding);
        }

        [Fact]
        public void prop_ContentType_get()
        {
            Assert.Equal("text/plain", new TextResult().ContentType);
        }
    }
}