namespace Cavity.Web.Mvc
{
    using System;
    using System.Web.Mvc;

    using Xunit;

    public sealed class InternalServerErrorViewResultFacts
    {
        [Fact]
        public void IInterceptInternalServerError_View_ExceptionContext()
        {
            IInterceptInternalServerError expected = new InternalServerErrorViewResult();

            var actual = expected.View(new ExceptionContext());

            Assert.Same(expected, actual);
        }

        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<InternalServerErrorViewResult>()
                            .DerivesFrom<ViewResult>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IInterceptInternalServerError>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            const string expected = "Error";
            var actual = new InternalServerErrorViewResult().ViewName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_string()
        {
            const string expected = "Example";
            var actual = new InternalServerErrorViewResult(expected).ViewName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new InternalServerErrorViewResult(string.Empty));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new InternalServerErrorViewResult(null));
        }
    }
}