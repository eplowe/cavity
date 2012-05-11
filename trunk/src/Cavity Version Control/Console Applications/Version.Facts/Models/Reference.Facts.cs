namespace Cavity.Models
{
    using System;
    using Cavity;
    using Xunit;

    public sealed class ReferenceFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Reference>()
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
            Assert.NotNull(new Reference());
        }

        [Fact]
        public void ctor_string_string()
        {
            Assert.NotNull(new Reference("example", @"..\..\packages\1.2.3.4\net40\example.dll"));
        }

        [Fact]
        public void prop_Hint()
        {
            Assert.True(new PropertyExpectations<Reference>(x => x.Hint)
                            .IsAutoProperty<string>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Include()
        {
            Assert.True(new PropertyExpectations<Reference>(x => x.Include)
                            .IsAutoProperty<string>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}