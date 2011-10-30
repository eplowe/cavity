namespace Cavity.Diagnostics
{
    using Xunit;

    public sealed class TaskCounterFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(TaskCounter).IsStatic());
        }

        [Fact]
        public void op_Increment()
        {
            TaskCounter.Increment();
        }
    }
}