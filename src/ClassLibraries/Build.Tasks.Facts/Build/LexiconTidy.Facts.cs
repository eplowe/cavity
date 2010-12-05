namespace Cavity.Build
{
    using System.IO;
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
                            .IsSealed()
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
            var file = new FileInfo(Path.GetTempFileName());
            try
            {
                using (var stream = file.Open(FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.WriteLine("CANONICAL,SYNONYMS");
                        writer.WriteLine("1,One");
                        writer.WriteLine("1,Unit");
                    }
                }

                var obj = new LexiconTidy
                {
                    BuildEngine = new Mock<IBuildEngine>().Object,
                    Paths = new ITaskItem[]
                    {
                        new TaskItem(file.FullName)
                    }
                };

                Assert.True(obj.Execute());

                file.Refresh();
                Assert.True(file.Exists);

                Assert.True(File.ReadAllText(file.FullName).Contains("1,One;Unit"));
            }
            finally
            {
                file.Refresh();
                if (file.Exists)
                {
                    file.Delete();
                }
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