namespace Cavity.Collections
{
    using System.Collections.Generic;
    using Cavity.Collections.Generic;
    using Cavity.IO;
    using Xunit;

    public sealed class DirectoryCollectionFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<DirectoryCollection>()
                            .DerivesFrom<XmlSerializableCollection<DirectoryItem>>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .XmlRoot("directories")
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new DirectoryCollection());
        }

        [Fact]
        public void opIndexer_string()
        {
            using (var temp = new TempDirectory())
            {
                const string name = "example";
                var expected = temp.Info.FullName;

                var obj = new DirectoryCollection
                              {
                                  new DirectoryItem
                                      {
                                          Name = name,
                                          Value = temp.Info.FullName
                                      }
                              };

                var actual = obj[name].FullName;

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void opIndexer_string_whenKeyNotFoundException()
        {
            Assert.Throws<KeyNotFoundException>(() => new DirectoryCollection()["example"]);
        }
    }
}