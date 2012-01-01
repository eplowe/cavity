namespace Cavity.Practices.ServiceLocation
{
    using System;
    using System.Linq;
    using Cavity;
    using Cavity.Examples;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using Moq;
    using Xunit;

    public sealed class UnityServiceLocatorFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<UnityServiceLocator>()
                .DerivesFrom<ServiceLocatorImplBase>()
                .IsConcreteClass()
                .IsSealed()
                .NoDefaultConstructor()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void ctor_IUnityContainer()
        {
            var container = new Mock<IUnityContainer>();

            Assert.NotNull(new UnityServiceLocator(container.Object));
        }

        [Fact]
        public void ctor_IUnityContainerNull()
        {
            Assert.Throws<ArgumentNullException>(() => new UnityServiceLocator(null));
        }

        [Fact]
        public void op_DoGetInstance_Type_string()
        {
            const string key = "example";
            var expected = new Mock<ITest>().Object;

            var container = new Mock<IUnityContainer>();
            container
                .Setup(x => x.Resolve(typeof(ITest), key))
                .Returns(expected)
                .Verifiable();

            var obj = new UnityServiceLocator(container.Object);

            var actual = obj.GetInstance<ITest>(key);

            Assert.Same(expected, actual);

            container.VerifyAll();
        }

        [Fact]
        public void op_DoGetInstance_Type_stringNull()
        {
            var expected = new Mock<ITest>().Object;

            var container = new Mock<IUnityContainer>();
            container
                .Setup(x => x.Resolve(typeof(ITest), null))
                .Returns(expected)
                .Verifiable();

            var obj = new UnityServiceLocator(container.Object);
            
            var actual = obj.GetInstance<ITest>();

            Assert.Same(expected, actual);

            container.VerifyAll();
        }

        [Fact]
        public void op_DoGetAllInstances_Type()
        {
            var expected = new Mock<ITest>().Object;

            var container = new Mock<IUnityContainer>();
            container
                .Setup(x => x.ResolveAll(typeof(ITest)))
                .Returns(new[] { expected })
                .Verifiable();

            var obj = new UnityServiceLocator(container.Object);

            var actual = obj.GetAllInstances<ITest>().First();

            Assert.Same(expected, actual);

            container.VerifyAll();
        }
    }
}