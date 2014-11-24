namespace Cavity.Models
{
    using System.IO;
    using Cavity.Dynamic;
    using Cavity.Threading;
    using Xunit;

    public sealed class FileProcessorFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<FileProcessor>()
                            .DerivesFrom<ThreadedObject>()
                            .IsAbstractBaseClass()
                            .IsNotDecorated()
                            .Implements<IProcessFile>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            using (FileProcessor obj = new DerivedFileProcessor())
            {
                Assert.NotNull(obj);
            }
        }

        [Fact]
        public void op_Process_FileInfo_dynamic()
        {
            using (FileProcessor obj = new DerivedFileProcessor())
            {
                obj.Process(new FileInfo("example.txt"), new DynamicData());
            }
        }
    }
}