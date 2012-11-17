namespace Cavity.Collections
{
    using System;
    using System.IO;

    using Cavity.IO;

    using Xunit;

    public sealed class FileItemFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<FileItem>()
                            .DerivesFrom<PathItem>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .XmlRoot("add")
                            .Implements<IEquatable<FileItem>>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new FileItem());
        }

        [Fact]
        public void op_Equals_FileItemNull()
        {
            // ReSharper disable RedundantCast
            Assert.False(new FileItem().Equals(null as FileItem));
            // ReSharper restore RedundantCast
        }

        [Fact]
        public void op_Equals_FileItem_whenFalse()
        {
            using (var temp = new TempFile())
            {
                var obj = new FileItem
                    {
                        Name = temp.Info.Name,
                        Value = temp.Info.FullName
                    };

                var other = new FileItem();

                Assert.False(obj.Equals(other));
            }
        }

        [Fact]
        public void op_Equals_FileItem_whenTrue()
        {
            using (var temp = new TempFile())
            {
                var obj = new FileItem
                {
                    Name = temp.Info.Name,
                    Value = temp.Info.FullName
                };

                var other = new FileItem
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
            Assert.True(new PropertyExpectations<FileItem>(x => x.Info)
                            .TypeIs<FileInfo>()
                            .XmlIgnore()
                            .Result);
        }

        [Fact]
        public void prop_Info_get()
        {
            using (var temp = new TempFile())
            {
                var expected = temp.Info.FullName;
                var obj = new FileItem
                    {
                        Value = expected
                    };
                var actual = obj.Info.FullName;

                Assert.Equal(expected, actual);
            }
        }
    }
}