namespace Cavity.Practices.ServiceLocation
{
    using System;
    using System.Linq;
    using Castle.Windsor;
    using Cavity.Examples;
    using Microsoft.Practices.ServiceLocation;
    using Moq;
    using Xunit;

    public sealed class WindsorServiceLocatorFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<WindsorServiceLocator>()
                            .DerivesFrom<ServiceLocatorImplBase>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor_IWindsorContainer()
        {
            var container = new Mock<IWindsorContainer>();

            Assert.NotNull(new WindsorServiceLocator(container.Object));
        }

        [Fact]
        public void ctor_IWindsorContainerNull()
        {
            Assert.Throws<ArgumentNullException>(() => new WindsorServiceLocator(null));
        }

        [Fact]
        public void op_DoGetAllInstances_Type()
        {
            var expected = new Mock<ITest>().Object;

            var container = new Mock<IWindsorContainer>();
            container
                .Setup(x => x.ResolveAll(typeof(ITest)))
                .Returns(new[] { expected })
                .Verifiable();

            var obj = new WindsorServiceLocator(container.Object);

            var actual = obj.GetAllInstances<ITest>().First();

            Assert.Same(expected, actual);

            container.VerifyAll();
        }

        [Fact]
        public void op_DoGetInstance_Type_string()
        {
            const string key = "example";
            var expected = new Mock<ITest>().Object;

            var container = new Mock<IWindsorContainer>();
            container
                .Setup(x => x.Resolve(key, typeof(ITest)))
                .Returns(expected)
                .Verifiable();

            var obj = new WindsorServiceLocator(container.Object);

            var actual = obj.GetInstance<ITest>(key);

            Assert.Same(expected, actual);

            container.VerifyAll();
        }

        [Fact]
        public void op_DoGetInstance_Type_stringNull()
        {
            var expected = new Mock<ITest>().Object;

            var container = new Mock<IWindsorContainer>();
            container
                .Setup(x => x.Resolve(typeof(ITest)))
                .Returns(expected)
                .Verifiable();

            var obj = new WindsorServiceLocator(container.Object);

            var actual = obj.GetInstance<ITest>();

            Assert.Same(expected, actual);

            container.VerifyAll();
        }
    }
}