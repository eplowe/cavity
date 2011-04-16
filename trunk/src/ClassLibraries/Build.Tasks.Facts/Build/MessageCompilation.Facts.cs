namespace Cavity.Build
{
    using Cavity;
    using Microsoft.Build.Framework;
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
            ////Assert.False(new MessageCompilation().Execute());
        }

        [Fact]
        public void prop_Files()
        {
            Assert.True(new PropertyExpectations<MessageCompilation>(p => p.Files)
                            .IsAutoProperty<ITaskItem[]>()
                            .IsDecoratedWith<RequiredAttribute>()
                            .Result);
        }

        [Fact]
        public void prop_WorkingDirectory()
        {
            Assert.True(new PropertyExpectations<MessageCompilation>(p => p.WorkingDirectory)
                            .IsAutoProperty<string>()
                            .IsDecoratedWith<RequiredAttribute>()
                            .Result);
        }
    }
}