namespace Cavity.Web.Mvc
{
    using System;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using Moq;
    using Xunit;

    public sealed class XmlSerializationResultFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<XmlSerializationResult>()
                            .DerivesFrom<ContentResult>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor_object()
        {
            Assert.NotNull(new XmlSerializationResult(123));
        }

        [Fact]
        public void ctor_objectNull()
        {
            Assert.Throws<ArgumentNullException>(() => new XmlSerializationResult(null));
        }

        [Fact]
        public void ctor_objectNull_string()
        {
            Assert.Throws<ArgumentNullException>(() => new XmlSerializationResult(null, "example/vnd.cavity-test+xml"));
        }

        [Fact]
        public void ctor_object_string()
        {
            Assert.NotNull(new XmlSerializationResult(123, "example/vnd.cavity-test+xml"));
        }

        [Fact]
        public void ctor_object_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new XmlSerializationResult(123, string.Empty));
        }

        [Fact]
        public void ctor_object_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new XmlSerializationResult(123, null));
        }

        [Fact]
        public void op_ExecuteResult_ControllerContext()
        {
            var response = new Mock<HttpResponseBase>(MockBehavior.Strict);
            response
                .SetupSet(x => x.ContentEncoding = Encoding.UTF8)
                .Verifiable();
            response
                .SetupSet(x => x.ContentType = "application/xml")
                .Verifiable();
            response
                .Setup(x => x.Write(It.IsAny<string>()))
                .Verifiable();

            var context = new Mock<HttpContextBase>(MockBehavior.Strict);
            context
                .SetupGet(x => x.Response)
                .Returns(response.Object)
                .Verifiable();

            new XmlSerializationResult(123).ExecuteResult(new ControllerContext
                                                              {
                                                                  HttpContext = context.Object
                                                              });

            response.VerifyAll();
        }

        [Fact]
        public void op_ExecuteResult_ControllerContextNull()
        {
            Assert.Throws<ArgumentNullException>(() => new NotAcceptableResult().ExecuteResult(null));
        }

        [Fact]
        public void prop_ContentEncoding_get()
        {
            Assert.Equal(Encoding.UTF8, new XmlSerializationResult(123).ContentEncoding);
        }

        [Fact]
        public void prop_ContentType_get()
        {
            const string expected = "application/xml";
            var actual = new XmlSerializationResult(123).ContentType;

            Assert.Equal(expected, actual);
        }
    }
}