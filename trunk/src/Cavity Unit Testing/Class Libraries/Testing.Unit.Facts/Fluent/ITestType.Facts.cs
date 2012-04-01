namespace Cavity.Fluent
{
    using System;

    using Cavity.Types;

    using Moq;

    using Xunit;

    public sealed class ITestTypeFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ITestType>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void op_Add_ITestExpectation()
        {
            var expected = new Mock<ITestType>().Object;

            var expectation = new Mock<ITestExpectation>().Object;

            var mock = new Mock<ITestType>();
            mock
                .Setup(x => x.Add(expectation))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Add(expectation);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_AttributeUsage_AttributeTargets()
        {
            var expected = new Mock<ITestType>().Object;

            const AttributeTargets validOn = AttributeTargets.All;

            var mock = new Mock<ITestType>();
            mock
                .Setup(x => x.AttributeUsage(validOn))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.AttributeUsage(validOn);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_AttributeUsage_AttributeTargets_bool_bool()
        {
            var expected = new Mock<ITestType>().Object;

            const AttributeTargets validOn = AttributeTargets.All;

            var mock = new Mock<ITestType>();
            mock
                .Setup(x => x.AttributeUsage(validOn, true, false))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.AttributeUsage(validOn, true, false);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_ImplementsOfT()
        {
            var expected = new Mock<ITestType>().Object;

            var mock = new Mock<ITestType>();
            mock
                .Setup(x => x.Implements<IInterface1>())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Implements<IInterface1>();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_IsDecoratedWithOfT()
        {
            var expected = new Mock<ITestType>().Object;

            var mock = new Mock<ITestType>();
            mock
                .Setup(x => x.IsDecoratedWith<Attribute1Attribute>())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.IsDecoratedWith<Attribute1Attribute>();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_IsNotDecorated()
        {
            var expected = new Mock<ITestType>().Object;

            var mock = new Mock<ITestType>();
            mock
                .Setup(x => x.IsNotDecorated())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.IsNotDecorated();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_Serializable()
        {
            var expected = new Mock<ITestType>().Object;

            var mock = new Mock<ITestType>();
            mock
                .Setup(x => x.Serializable())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Serializable();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_XmlRoot_string()
        {
            var expected = new Mock<ITestType>().Object;

            const string elementName = "example";

            var mock = new Mock<ITestType>();
            mock
                .Setup(x => x.XmlRoot(elementName))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.XmlRoot(elementName);

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_XmlRoot_string_string()
        {
            var expected = new Mock<ITestType>().Object;

            const string elementName = "example";

            var mock = new Mock<ITestType>();
            mock
                .Setup(x => x.XmlRoot(elementName, "namespace"))
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.XmlRoot(elementName, "namespace");

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void op_XmlSerializable()
        {
            var expected = new Mock<ITestType>().Object;

            var mock = new Mock<ITestType>();
            mock
                .Setup(x => x.XmlSerializable())
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.XmlSerializable();

            Assert.Same(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Result_get()
        {
            const bool expected = true;

            var mock = new Mock<ITestType>();
            mock
                .SetupGet(x => x.Result)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Result;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }
    }
}