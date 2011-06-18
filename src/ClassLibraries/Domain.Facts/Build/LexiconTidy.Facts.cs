namespace Cavity.Build
{
    using System.IO;
    using Cavity.IO;
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;
    using Moq;
    using Xunit;

    public sealed class LexiconTidyFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<LexiconTidy>()
                            .DerivesFrom<Task>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new LexiconTidy());
        }

        [Fact]
        public void op_Execute_IEnumerable()
        {
            using (var file = new TempFile())
            {
                file.Info.AppendLine("CANONICAL,SYNONYMS");
                file.Info.AppendLine("1,One");
                file.Info.AppendLine("1,Unit");

                var obj = new LexiconTidy
                {
                    BuildEngine = new Mock<IBuildEngine>().Object,
                    Paths = new ITaskItem[]
                    {
                        new TaskItem(file.Info.FullName)
                    }
                };

                Assert.True(obj.Execute());

                file.Info.Refresh();
                Assert.True(file.Info.Exists);

                Assert.True(File.ReadAllText(file.Info.FullName).Contains("1,One;Unit"));
            }
        }

        [Fact]
        public void op_Execute_IEnumerableEmpty()
        {
            var obj = new LexiconTidy
            {
                BuildEngine = new Mock<IBuildEngine>().Object,
                Paths = new ITaskItem[]
                {
                }
            };

            Assert.False(obj.Execute());
        }

        [Fact]
        public void op_Execute_IEnumerableNull()
        {
            var obj = new LexiconTidy
            {
                BuildEngine = new Mock<IBuildEngine>().Object
            };

            Assert.False(obj.Execute());
        }

        [Fact]
        public void prop_Paths()
        {
            Assert.True(new PropertyExpectations<LexiconTidy>(p => p.Paths)
                            .IsAutoProperty<ITaskItem[]>()
                            .IsDecoratedWith<RequiredAttribute>()
                            .Result);
        }
    }
}