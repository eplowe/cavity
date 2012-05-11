namespace Cavity.Models
{
    using Xunit;
    using Xunit.Extensions;

    public sealed class PackageFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Package>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new Package());
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new Package("example"));
        }

        [Fact]
        public void ctor_string_string()
        {
            Assert.NotNull(new Package("example", "1.2.3.4"));
        }

        [Theory]
        [InlineData("example.1.2.3.4", "example", "1.2.3.4")]
        [InlineData("example", "example", "")]
        public void op_ToString(string expected, string id, string version)
        {
            var actual = new Package(id, version).ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Id()
        {
            Assert.True(new PropertyExpectations<Package>(x => x.Id)
                            .IsAutoProperty<string>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Version()
        {
            Assert.True(new PropertyExpectations<Package>(x => x.Version)
                            .IsAutoProperty<string>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}