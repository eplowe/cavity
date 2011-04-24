namespace Cavity.Models
{
    using System;
    using Xunit;

    public sealed class TaskManagerFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<TaskManager>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IDisposable>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            using (var obj = new TaskManager())
            {
                Assert.NotNull(obj);
            }
        }

        [Fact]
        public void op_Continue()
        {
            using (var obj = new TaskManager())
            {
                (obj as IManageTasks).Start(null);
                (obj as IManageTasks).Pause();
                (obj as IManageTasks).Continue();
            }
        }

        [Fact]
        public void op_Pause()
        {
            using (var obj = new TaskManager())
            {
                (obj as IManageTasks).Start(null);
                (obj as IManageTasks).Pause();
            }
        }

        [Fact]
        public void op_Shutdown()
        {
            using (var obj = new TaskManager())
            {
                (obj as IManageTasks).Shutdown();
            }
        }

        [Fact]
        public void op_Start_IEnumerableOfString()
        {
            using (var obj = new TaskManager())
            {
                (obj as IManageTasks).Start(new[]
                {
                    "abc"
                });
            }
        }

        [Fact]
        public void op_Start_IEnumerableOfStringNull()
        {
            using (var obj = new TaskManager())
            {
                (obj as IManageTasks).Start(null);
            }
        }

        [Fact]
        public void op_Stop()
        {
            using (var obj = new TaskManager())
            {
                (obj as IManageTasks).Start(null);
                (obj as IManageTasks).Stop();
            }
        }
    }
}