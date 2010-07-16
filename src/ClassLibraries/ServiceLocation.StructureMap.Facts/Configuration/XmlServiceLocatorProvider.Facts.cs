namespace Cavity.Configuration
{
    using Example;
    using Microsoft.Practices.ServiceLocation;
    using Xunit;

    public sealed class XmlServiceLocatorProviderFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<XmlServiceLocatorProvider>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new XmlServiceLocatorProvider());
        }

        [Fact]
        public void op_Configure()
        {
            new XmlServiceLocatorProvider().Configure();

            Assert.NotNull(ServiceLocator.Current.GetInstance<ITest>());
        }
    }
}