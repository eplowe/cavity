namespace Cavity.Practices.ServiceLocation
{
    using System;
    using System.Linq;
    using Autofac;
    using Cavity.Examples;
    using Microsoft.Practices.ServiceLocation;
    using Moq;
    using Xunit;

    public sealed class AutofacServiceLocatorFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<AutofacServiceLocator>()
                            .DerivesFrom<ServiceLocatorImplBase>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor_IAutofacContainer()
        {
            var container = new Mock<IComponentContext>();

            Assert.NotNull(new AutofacServiceLocator(container.Object));
        }

        [Fact]
        public void ctor_IAutofacContainerNull()
        {
            Assert.Throws<ArgumentNullException>(() => new AutofacServiceLocator(null));
        }

        [Fact]
        public void op_DoGetAllInstances_Type()
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterType<Tester>()
                .Keyed<ITest>("abc")
                .Named<ITest>(typeof(Tester).FullName)
                .SingleInstance()
                .As<ITest>();

            builder
                .RegisterType<Tester>()
                .Keyed<ITest>("xyz")
                .Named<ITest>(typeof(Tester).FullName)
                .SingleInstance()
                .As<ITest>();

            var obj = new AutofacServiceLocator(builder.Build());

            Assert.Equal(2, obj.GetAllInstances<ITest>().Count());
        }

        [Fact]
        public void op_DoGetInstance_Type_string()
        {
            const string key = "example";
            var builder = new ContainerBuilder();

            builder
                .RegisterType<Tester>()
                .Keyed<ITest>(key)
                .Named<ITest>(typeof(Tester).FullName)
                .SingleInstance()
                .As<ITest>();

            var obj = new AutofacServiceLocator(builder.Build());

            Assert.IsType<Tester>(obj.GetInstance<ITest>(key));
        }

        [Fact]
        public void op_DoGetInstance_Type_stringNull()
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterType<Tester>()
                .Named<ITest>(typeof(Tester).FullName)
                .SingleInstance()
                .As<ITest>();

            var obj = new AutofacServiceLocator(builder.Build());

            Assert.IsType<Tester>(obj.GetInstance<ITest>());
        }
    }
}