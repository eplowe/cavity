namespace Cavity
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;
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

        public ITestObject IsInterface()
        {
            this.Items.Add(new InterfaceTest<T>());
            return this;
        }

        public ITestObject IsValueType()
        {
            this.Items.Add(new ValueTypeTest<T>());
            return this;
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

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Inference brings no benefit here.")]
        ITestObject ITestObject.IsDecoratedWith<TAttribute>()
        {
            if (typeof(SerializableAttribute).IsAssignableFrom(typeof(TAttribute)))
            {
                throw new TestException(Resources.ObjectExpectations_IsDecoratedWithSerializable);
            }
            else if (typeof(XmlRootAttribute).IsAssignableFrom(typeof(TAttribute)))
            {
                throw new TestException(Resources.ObjectExpectations_IsDecoratedWithXmlRoot);
            }

            this.Items.Add(new AttributeMemberTest(typeof(T), typeof(TAttribute)));
            return this;
        }

        ITestObject ITestObject.Serializable()
        {
            this.Items.Add(new AttributeMemberTest(typeof(T), typeof(SerializableAttribute)));
            this.Items.Add(new ImplementationTest<T>(typeof(ISerializable)));
            return this;
        }

        ITestObject ITestObject.XmlRoot(string name)
        {
            this.Items.Add(new XmlRootTest<T>(name));
            return this;
        }

        ITestObject ITestObject.XmlRoot(string name, string @namespace)
        {
            this.Items.Add(new XmlRootTest<T>(name, @namespace));
            return this;
        }

        ITestObject ITestObject.IsNotDecorated()
        {
            this.Items.Add(new AttributeMemberTest(typeof(T), null as Type));
            return this;
        }
    }
}