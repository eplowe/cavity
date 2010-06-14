namespace Cavity
{
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Cavity.Fluent;
    using Cavity.Properties;
    using Cavity.Tests;

    public sealed class ObjectExpectations<T> : ITestObjectStyle, ITestObjectSealed, ITestObjectConstruction, ITestObject
    {
        public ObjectExpectations()
        {
            this.Items = new Collection<ITestExpectation>();
        }

        bool ITestObject.Result
        {
            get
            {
                return 0 == this.Items.Where(x => !x.Check()).Count();
            }
        }

        private Collection<ITestExpectation> Items
        {
            get;
            set;
        }

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Inference brings no benefit here.")]
        public ITestObjectStyle DerivesFrom<TBase>()
        {
            this.Items.Add(new BaseClassTest<T>(typeof(TBase)));
            return this;
        }

        ITestObject ITestObjectStyle.IsAbstractBaseClass()
        {
            this.Items.Add(new AbstractBaseClassTest<T>());
            return this;
        }

        ITestObjectSealed ITestObjectStyle.IsConcreteClass()
        {
            this.Items.Add(new ConcreteClassTest<T>());
            return this;
        }

        ITestObjectConstruction ITestObjectSealed.IsSealed()
        {
            this.Items.Add(new SealedClassTest<T>(true));
            return this;
        }

        ITestObjectConstruction ITestObjectSealed.IsUnsealed()
        {
            this.Items.Add(new SealedClassTest<T>(false));
            return this;
        }

        ITestObject ITestObjectConstruction.HasDefaultConstructor()
        {
            this.Items.Add(new DefaultConstructorTest<T>());
            return this;
        }

        ITestObject ITestObjectConstruction.NoDefaultConstructor()
        {
            return this;
        }

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Inference brings no benefit here.")]
        ITestObject ITestObject.Implements<TInterface>()
        {
            if (!typeof(TInterface).IsInterface)
            {
                throw new TestException(Resources.ObjectExpectationsException_InterfaceMessage);
            }

            this.Items.Add(new ImplementationTest<T>(typeof(TInterface)));
            return this;
        }
    }
}