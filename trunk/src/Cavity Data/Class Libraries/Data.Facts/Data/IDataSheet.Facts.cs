namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using Cavity;
    using Cavity.Collections;

    using Moq;

    using Xunit;

    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "DataSheet", Justification = "This is the correct casing.")]
    public sealed class IDataSheetFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IDataSheet>()
                            .IsInterface()
                            .IsNotDecorated()
                            .Implements<IEnumerable<KeyStringDictionary>>()
                            .Result);
        }

        [Fact]
        public void prop_Title_get()
        {
            const string expected = "Example";

            var mock = new Mock<IDataSheet>();
            mock
                .Setup(x => x.Title)
                .Returns(expected)
                .Verifiable();

            var actual = mock.Object.Title;

            Assert.Equal(expected, actual);

            mock.VerifyAll();
        }

        [Fact]
        public void prop_Title_set()
        {
            const string expected = "Example";

            var mock = new Mock<IDataSheet>();
            mock
                .SetupSet(x => x.Title = expected)
                .Verifiable();

            mock.Object.Title = expected;

            mock.VerifyAll();
        }
    }
}