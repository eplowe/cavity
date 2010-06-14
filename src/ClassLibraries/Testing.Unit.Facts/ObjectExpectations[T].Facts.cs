namespace Cavity
{
    using System;
    using System.Xml.Serialization;
    using Cavity.Fluent;
    using Cavity.Types;
    using Xunit;

    public class ObjectExpectationsOfTFacts
    {
        [Fact]
        public void is_ITestObjectStyle()
        {
            Assert.IsAssignableFrom<ITestObjectStyle>(new ObjectExpectations<object>());
        }

        [Fact]
        public void is_ITestObject()
        {
            Assert.IsAssignableFrom<ITestObject>(new ObjectExpectations<object>());
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new ObjectExpectations<object>());
        }

        [Fact]
        public void prop_Result_whenAbstractBaseClass()
        {
            Assert.True(new ObjectExpectations<AbstractBaseClass1>()
                .DerivesFrom<object>()
                .IsAbstractBaseClass()
                .Result);
        }

        [Fact]
        public void prop_Result_whenUnsealedClass()
        {
            Assert.True(new ObjectExpectations<DerivedClass1>()
                .DerivesFrom<Class1>()
                .IsConcreteClass()
                .IsUnsealed()
                .HasDefaultConstructor()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void prop_Result_whenInterfaceClass()
        {
            Assert.True(new ObjectExpectations<InterfacedClass1>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsUnsealed()
                .HasDefaultConstructor()
                .Implements<Interface1>()
                .Result);
        }

        [Fact]
        public void prop_Result_whenAttributedClass()
        {
            Assert.True(new ObjectExpectations<AttributedClass1>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsUnsealed()
                .HasDefaultConstructor()
                .IsDecoratedWith<Attribute1>()
                .Result);
        }

        [Fact]
        public void prop_Result_whenIsDecoratedWithSerializableAttribute()
        {
            Assert.Throws<TestException>(() => new ObjectExpectations<TestException>()
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
            Assert.Throws<TestException>(() => new ObjectExpectations<XmlSerializableClass1>()
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
            Assert.True(new ObjectExpectations<TestException>()
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
            Assert.True(new ObjectExpectations<XmlSerializableClass1>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsUnsealed()
                .HasDefaultConstructor()
                .XmlRoot("root", "urn:example.net")
                .Result);
        }
    }
}