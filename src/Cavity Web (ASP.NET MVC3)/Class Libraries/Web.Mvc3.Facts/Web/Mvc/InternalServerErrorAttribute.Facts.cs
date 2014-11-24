namespace Cavity.Web.Mvc
{
    using System;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Moq;
    using Xunit;

    public sealed class InternalServerErrorAttributeFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<InternalServerErrorAttribute>()
                            .DerivesFrom<FilterAttribute>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .Implements<IExceptionFilter>()
                            .AttributeUsage(AttributeTargets.Class, false, false)
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new InternalServerErrorAttribute());
        }

        [Fact]
        public void ctor_Type()
        {
            Assert.NotNull(new InternalServerErrorAttribute(typeof(InternalServerErrorViewResult)));
        }

        [Fact]
        public void ctor_TypeInvalid()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new InternalServerErrorAttribute(typeof(EmptyResult)));
        }

        [Fact]
        public void ctor_TypeNull()
        {
            Assert.Throws<ArgumentNullException>(() => new InternalServerErrorAttribute(null));
        }

        [Fact]
        public void op_OnException_ExceptionContext()
        {
            var response = new Mock<HttpResponseBase>(MockBehavior.Strict);
            response
                .Setup(x => x.Clear())
                .Verifiable();
            response
                .SetupSet(x => x.StatusCode = (int)HttpStatusCode.InternalServerError)
                .Verifiable();
            response
                .Setup(x => x.Cache.SetCacheability(HttpCacheability.NoCache))
                .Verifiable();
            response
                .SetupSet(x => x.TrySkipIisCustomErrors = true)
                .Verifiable();

            var context = new Mock<HttpContextBase>(MockBehavior.Strict);
            context
                .SetupGet(x => x.IsCustomErrorEnabled)
                .Returns(true)
                .Verifiable();
            context
                .SetupGet(x => x.Response)
                .Returns(response.Object)
                .Verifiable();

            var filterContext = new ExceptionContext
                                    {
                                        HttpContext = context.Object,
                                        Exception = new HttpException(),
                                        ExceptionHandled = false
                                    };

            new InternalServerErrorAttribute().OnException(filterContext);

            Assert.True(filterContext.ExceptionHandled);
            Assert.IsAssignableFrom<InternalServerErrorViewResult>(filterContext.Result);

            response.VerifyAll();
            context.VerifyAll();
        }

        [Fact]
        public void op_OnException_ExceptionContextNull()
        {
            Assert.Throws<ArgumentNullException>(() => new InternalServerErrorAttribute().OnException(null));
        }

        [Fact]
        public void op_OnException_ExceptionContext_whenExceptionHandled()
        {
            var filterContext = new ExceptionContext
                                    {
                                        HttpContext = new Mock<HttpContextBase>(MockBehavior.Strict).Object,
                                        ExceptionHandled = true
                                    };

            new InternalServerErrorAttribute().OnException(filterContext);

            Assert.True(filterContext.ExceptionHandled);
            Assert.IsAssignableFrom<EmptyResult>(filterContext.Result);
        }

        [Fact]
        public void op_OnException_ExceptionContext_whenNotInternalServerError()
        {
            var context = new Mock<HttpContextBase>(MockBehavior.Strict);
            context
                .SetupGet(x => x.IsCustomErrorEnabled)
                .Returns(true)
                .Verifiable();

            var filterContext = new ExceptionContext
                                    {
                                        HttpContext = context.Object,
                                        Exception = new HttpException((int)HttpStatusCode.NotFound, "404 Not Found"),
                                        ExceptionHandled = false
                                    };

            new InternalServerErrorAttribute().OnException(filterContext);

            Assert.False(filterContext.ExceptionHandled);
            Assert.IsAssignableFrom<EmptyResult>(filterContext.Result);

            context.VerifyAll();
        }

        [Fact]
        public void op_OnException_ExceptionContext_whenNotIsCustomErrorEnabled()
        {
            var context = new Mock<HttpContextBase>(MockBehavior.Strict);
            context
                .SetupGet(x => x.IsCustomErrorEnabled)
                .Returns(false)
                .Verifiable();

            var filterContext = new ExceptionContext
                                    {
                                        HttpContext = context.Object,
                                        ExceptionHandled = false
                                    };

            new InternalServerErrorAttribute().OnException(filterContext);

            Assert.False(filterContext.ExceptionHandled);
            Assert.IsAssignableFrom<EmptyResult>(filterContext.Result);

            context.VerifyAll();
        }

        [Fact]
        public void prop_Interceptor()
        {
            Assert.True(new PropertyExpectations<InternalServerErrorAttribute>(x => x.Interceptor)
                            .TypeIs<Type>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Interceptor_get()
        {
            var expected = typeof(InternalServerErrorViewResult);
            var actual = new InternalServerErrorAttribute().Interceptor;

            Assert.Equal(expected, actual);
        }
    }
}