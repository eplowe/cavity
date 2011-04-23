namespace Cavity.Models
{
    using Cavity;
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
                .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new TaskManager());
        }

        [Fact]
        public void op_Continue()
        {
            IManageTasks obj = new TaskManager();
            obj.Continue();
        }

        [Fact]
        public void op_Pause()
        {
            IManageTasks obj = new TaskManager();
            obj.Pause();
        }

        [Fact]
        public void op_Shutdown()
        {
            IManageTasks obj = new TaskManager();
            obj.Shutdown();
        }

        [Fact]
        public void op_Start_IEnumerableOfString()
        {
            IManageTasks obj = new TaskManager();
            obj.Start(new[] { "abc" });
        }

        [Fact]
        public void op_Start_IEnumerableOfStringNull()
        {
            IManageTasks obj = new TaskManager();
            obj.Start(null);
        }

        [Fact]
        public void op_Stop()
        {
            IManageTasks obj = new TaskManager();
            obj.Stop();
        }
    }
}