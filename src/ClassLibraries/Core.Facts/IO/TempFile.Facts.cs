namespace Cavity.IO
{
    using System;
    using System.IO;
    using Xunit;

    public sealed class TempFileFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<TempFile>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IDisposable>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            using (var file = new TempFile())
            {
                Assert.NotNull(file);
            }
        }

        [Fact]
        public void ctor_DirectoryInfo()
        {
            using (var directory = new TempDirectory())
            {
                using (var file = new TempFile(directory.Info))
                {
                    Assert.NotNull(file);
                }
            }
        }

        [Fact]
        public void ctor_DirectoryInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => new TempFile(null));
        }

        [Fact]
        public void op_Dispose()
        {
            TempFile file = null;
            FileInfo info;
            try
            {
                file = new TempFile();
                info = file.Info;
            }
            finally
            {
                if (null != file)
                {
                    file.Dispose();
                }
            }

            Assert.Null(file.Info);

            info.Refresh();
            Assert.False(info.Exists);
        }

        [Fact]
        public void prop_Info()
        {
            using (var file = new TempFile())
            {
                Assert.True(file.Info.Exists);
            }
        }
    }
}