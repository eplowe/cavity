namespace Cavity.Data.Transformation
{
    using System.Collections.Generic;

    using Cavity.IO;

    using Moq;

    using Xunit;

    public sealed class ITransformCsvOfTFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ITransformCsv>()
                            .IsInterface()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void op_TransformEntriesOfT_CsvFile()
        {
            using (var temp = new TempFile())
            {
                var csv = new CsvFile(temp.Info);
                var expected = new List<int>();

                var mock = new Mock<ITransformCsv<int>>();
                mock
                    .Setup(x => x.TransformEntries(csv))
                    .Returns(expected)
                    .Verifiable();

                var actual = mock.Object.TransformEntries(csv);

                Assert.Same(expected, actual);

                mock.VerifyAll();
            }
        }
    }
}