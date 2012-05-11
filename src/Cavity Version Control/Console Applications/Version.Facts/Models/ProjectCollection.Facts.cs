namespace Cavity.Models
{
    using System.Collections.ObjectModel;

    using Cavity.IO;
    using Cavity.Testing;

    using Xunit;

    public sealed class ProjectCollectionFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ProjectCollection>()
                            .DerivesFrom<Collection<Project>>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void op_Load_string()
        {
            using (var temp = new TempDirectory())
            {
                var location = temp.Info.ToFile("example.csproj");
                ProjectFile.Create(location);

                var obj = ProjectCollection.Load(temp.Info.FullName);

                Assert.Equal(location.FullName, obj[0].Location.FullName);
            }
        }
    }
}