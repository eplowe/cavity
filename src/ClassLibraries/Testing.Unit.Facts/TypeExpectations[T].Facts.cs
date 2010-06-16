namespace Cavity
{
    using System;
    using System.Xml.Serialization;
    using Cavity.Fluent;
    using Cavity.Tests;
    using Cavity.Types;
    using Xunit;

    public sealed class TypeExpectationsOfTFacts
    {
        [Fact]
        public void is_ITestClassStyle()
        {
            Assert.IsAssignableFrom<ITestClassStyle>(new TypeExpectations<object>());
        }

        [Fact]
        public void is_ITestClassSealed()
        {
            Assert.IsAssignableFrom<ITestClassSealed>(new TypeExpectations<object>());
        }

        [Fact]
        public void is_ITestClassConstruction()
        {
            Assert.IsAssignableFrom<ITestClassConstruction>(new TypeExpectations<object>());
        }

        [Fact]
        public void is_ITestType()
        {
            Assert.IsAssignableFrom<ITestType>(new TypeExpectations<object>());
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new TypeExpectations<object>());
        }

        [Fact]
        public void prop_Result_whenInterface()
        {
            Assert.True(new TypeExpectations<ITestExpectation>()
                .IsInterface()
                .Result);
        }

        [Fact]
        public void prop_Result_whenValueType()
        {
            Assert.True(new TypeExpectations<DateTime>()
                .IsValueType()
                .Result);
        }

        [Fact]
        public void prop_Result_whenAbstractBaseClass()
        {
            Assert.True(new TypeExpectations<AbstractBaseClass1>()
                .DerivesFrom<object>()
                .IsAbstractBaseClass()
                .Result);
        }

        [Fact]
        public void prop_Result_whenConstructorClass()
        {
            Assert.True(new TypeExpectations<ConstructorClass1>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .NoDefaultConstructor()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void prop_Result_whenSealedClass()
        {
            Assert.True(new TypeExpectations<SealedClass1>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void prop_Result_whenUnsealedClass()
        {
            Assert.True(new TypeExpectations<Class1>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsUnsealed()
                .HasDefaultConstructor()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void prop_Result_whenInterfaceClass()
        {
            Assert.True(new TypeExpectations<InterfacedClass1>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
                .Implements<Interface1>()
                .Result);
        }

        [Fact]
        public void prop_Result_whenAttributedClass()
        {
            Assert.True(new TypeExpectations<AttributedClass1>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
                .IsDecoratedWith<Attribute1>()
                .Result);
        }

        [Fact]
        public void prop_Result_whenIsDecoratedWithSerializableAttribute()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new TypeExpectations<TestException>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsUnsealed()
                .HasDefaultConstructor()
                .IsDecoratedWith<SerializableAttribute>()
                .Result);
        }

        [Fact]
        public void prop_Result_whenIsDecoratedWithXmlRootAttribute()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new TypeExpectations<XmlSerializableClass1>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsUnsealed()
                .HasDefaultConstructor()
                .IsDecoratedWith<XmlRootAttribute>()
                .Result);
        }

        [Fact]
        public void prop_Result_whenSerializable()
        {
            Assert.True(new TypeExpectations<TestException>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsUnsealed()
                .HasDefaultConstructor()
                .Serializable()
                .Result);
        }

        [Fact]
        public void prop_Result_whenXmlRoot()
        {
            Assert.True(new TypeExpectations<XmlRootClass1>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
                .XmlRoot("root")
                .Result);
        }

        [Fact]
        public void prop_Result_whenXmlRootWithNamespace()
        {
            Assert.True(new TypeExpectations<XmlSerializableClass1>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
                .XmlRoot("root", "urn:example.net")
                .Result);
        }

        [Fact]
        public void op_ImplementsOfT_whenNotInterface()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new TypeExpectations<Class1>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsUnsealed()
                .HasDefaultConstructor()
                .Implements<string>());
        }

        [Fact]
        public void op_Add_ITestExpectation()
        {
            Assert.Throws<NotImplementedException>(() => new TypeExpectations<Class1>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsUnsealed()
                .HasDefaultConstructor()
                .Add(new TestExpectation())
                .Result);
        }

        [Fact]
        public void op_Add_ITestExpectationNull()
        {
            Assert.Throws<ArgumentNullException>(() => new TypeExpectations<Class1>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsUnsealed()
                .HasDefaultConstructor()
                .Add(null as ITestExpectation));
        }
    }
}