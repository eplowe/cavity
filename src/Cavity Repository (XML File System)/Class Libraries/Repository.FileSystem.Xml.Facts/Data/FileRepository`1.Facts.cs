namespace Cavity.Data
{
    using Cavity.Configuration;
    using Cavity.IO;

    using Xunit;

    public sealed class FileRepositoryFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<FileRepository<int>>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IRepository<int>>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new FileRepository<int>());
        }

        [Fact]
        public void expectations_class()
        {
            try
            {
                using (var directory = new TempDirectory())
                {
                    FileRepositoryConfiguration.Mock = directory.Info;
                    new RepositoryExpectations<RandomObject>().VerifyAll<FileRepository<RandomObject>>();
                }
            }
            finally
            {
                FileRepositoryConfiguration.Mock = null;
            }
        }

        [Fact]
        public void expectations_struct()
        {
            try
            {
                using (var directory = new TempDirectory())
                {
                    FileRepositoryConfiguration.Mock = directory.Info;
                    new RepositoryExpectations<int>().VerifyAll<FileRepository<int>>();
                }
            }
            finally
            {
                FileRepositoryConfiguration.Mock = null;
            }
        }
    }
}