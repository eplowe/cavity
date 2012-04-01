namespace Cavity.Tests
{
    using Cavity.Fluent;
    using Cavity.Types;

    using Xunit;

    public sealed class XmlSerializableTestOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<XmlSerializableTest>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<ITestExpectation>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new XmlSerializableTest(typeof(Class1)));
        }

        [Fact]
        public void op_Check()
        {
            Assert.True(new XmlSerializableTest(typeof(XmlSerializationClass4)).Check());
        }

        [Fact]
        public void op_Check_whenGetSchemaDoesNotThrowException()
        {
            Assert.Throws<UnitTestException>(() => new XmlSerializableTest(typeof(XmlSerializationClass1)).Check());
        }

        [Fact]
        public void op_Check_whenInterfaceNotImplemented()
        {
            Assert.Throws<UnitTestException>(() => new XmlSerializableTest(typeof(Class1)).Check());
        }

        [Fact]
        public void op_Check_whenReadXmlDoesNotThrowException()
        {
            Assert.Throws<UnitTestException>(() => new XmlSerializableTest(typeof(XmlSerializationClass2)).Check());
        }

        [Fact]
        public void op_Check_whenWriteXmlDoesNotThrowException()
        {
            Assert.Throws<UnitTestException>(() => new XmlSerializableTest(typeof(XmlSerializationClass3)).Check());
        }

        [Fact]
        public void op_Check_whenXmlRoot()
        {
            Assert.True(new XmlSerializableTest(typeof(XmlSerializationClass5)).Check());
        }
    }
}