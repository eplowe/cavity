namespace Cavity.Models
{
    using System.Diagnostics.CodeAnalysis;

    using Xunit;

    public sealed class UserCategoryFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(UserCategory).IsStatic());
        }

        [Fact]
        public void op_Resolve_char_whenLarge()
        {
            Assert.IsType<LargeUserCategory>(UserCategory.Resolve('L'));
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "NonResidential", Justification = "This is not a single word.")]
        public void op_Resolve_char_whenNonResidential()
        {
            Assert.IsType<NonResidentialUserCategory>(UserCategory.Resolve('N'));
        }

        [Fact]
        public void op_Resolve_char_whenResidential()
        {
            Assert.IsType<ResidentialUserCategory>(UserCategory.Resolve('R'));
        }

        [Fact]
        public void op_Resolve_char_whenUnknown()
        {
            Assert.Null(UserCategory.Resolve('x'));
        }
    }
}