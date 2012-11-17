namespace Cavity.Collections
{
    using System;
    using System.IO;

    using Cavity.IO;

    using Xunit;

    public sealed class DirectoryItemFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<DirectoryItem>()
                            .DerivesFrom<PathItem>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .XmlRoot("add")
                            .Implements<IEquatable<DirectoryItem>>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new DirectoryItem());
        }

        [Fact]
        public void op_Equals_DirectoryItemNull()
        {
            // ReSharper disable RedundantCast
            Assert.False(new DirectoryItem().Equals(null as DirectoryItem));
            // ReSharper restore RedundantCast
        }

        [Fact]
        public void op_Equals_DirectoryItem_whenFalse()
        {
            using (var temp = new TempDirectory())
            {
                var obj = new DirectoryItem
                    {
                        Name = temp.Info.Name,
                        Value = temp.Info.FullName
                    };

                var other = new DirectoryItem();

                Assert.False(obj.Equals(other));
            }
        }

        [Fact]
        public void op_Equals_DirectoryItem_whenTrue()
        {
            using (var temp = new TempDirectory())
            {
                var obj = new DirectoryItem
                {
                    Name = temp.Info.Name,
                    Value = temp.Info.FullName
                };

                var other = new DirectoryItem
                {
                    Name = temp.Info.Name,
                    Value = temp.Info.FullName
                };

                Assert.True(obj.Equals(other));
            }
        }

        [Fact]
        public void prop_Info()
        {
            Assert.True(new PropertyExpectations<DirectoryItem>(x => x.Info)
                            .TypeIs<DirectoryInfo>()
                            .XmlIgnore()
                            .Result);
        }

        [Fact]
        public void prop_Info_get()
        {
            using (var temp = new TempDirectory())
            {
                var expected = temp.Info.FullName;
                var obj = new DirectoryItem
                    {
                        Value = expected
                    };
                var actual = obj.Info.FullName;

                Assert.Equal(expected, actual);
            }
        }
    }
}