namespace Cavity.Configuration
{
    using Cavity.Collections;

    using Xunit;

    public sealed class PathsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Paths>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .XmlRoot("paths")
                            .XmlSerializable(false)
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new Paths());
        }

        [Fact]
        public void prop_Directories()
        {
            Assert.True(new PropertyExpectations<Paths>(x => x.Directories)
                            .TypeIs<DirectoryCollection>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Files()
        {
            Assert.True(new PropertyExpectations<Paths>(x => x.Files)
                            .TypeIs<FileCollection>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}