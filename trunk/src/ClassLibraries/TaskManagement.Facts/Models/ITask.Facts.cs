namespace Cavity.Models
{
    using System;
    using Cavity;
    using Cavity.Data;
    using Xunit;

    public sealed class ITaskFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ITask>()
                            .IsInterface()
                            .Result);
        }

        [Fact]
        public void ITask_Execute_DataCollection()
        {
            try
            {
                var value = (new ITaskDummy() as ITask).Execute(new DataCollection());
                Assert.NotNull(value);
            }
            catch (NotSupportedException)
            {
            }
        }
    }
}