namespace Cavity.Build
{
    using Cavity;
    using Microsoft.Build.Utilities;
    using Xunit;

    public sealed class MessageCompilationFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<MessageCompilation>()
                            .DerivesFrom<Task>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new MessageCompilation());
        }

        [Fact]
        public void op_Execute()
        {
            Assert.False(new MessageCompilation().Execute());
        }
    }
}