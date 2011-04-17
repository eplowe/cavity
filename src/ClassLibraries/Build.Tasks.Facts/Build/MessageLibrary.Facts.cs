namespace Cavity.Build
{
    using Cavity;
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;
    using Xunit;

    public sealed class MessageLibraryFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<MessageLibrary>()
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
            Assert.NotNull(new MessageLibrary());
        }

        [Fact]
        public void op_Execute()
        {
            ////Assert.False(new MessageLibrary().Execute());
        }

        [Fact]
        public void prop_Files()
        {
            Assert.True(new PropertyExpectations<MessageLibrary>(p => p.Files)
                            .IsAutoProperty<ITaskItem[]>()
                            .IsDecoratedWith<RequiredAttribute>()
                            .Result);
        }

        [Fact]
        public void prop_WorkingDirectory()
        {
            Assert.True(new PropertyExpectations<MessageLibrary>(p => p.WorkingDirectory)
                            .IsAutoProperty<string>()
                            .IsDecoratedWith<RequiredAttribute>()
                            .Result);
        }
    }
}