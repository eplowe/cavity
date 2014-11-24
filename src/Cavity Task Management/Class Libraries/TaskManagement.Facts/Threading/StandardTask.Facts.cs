namespace Cavity.Threading
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public sealed class StandardTaskFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<StandardTask>()
                            .DerivesFrom<object>()
                            .IsAbstractBaseClass()
                            .IsNotDecorated()
                            .Implements<ITask>()
                            .Implements<IDisposable>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            using (var obj = new DerivedStandardTask())
            {
                Assert.IsAssignableFrom<StandardTask>(obj);
            }
        }

        [Fact]
        public void op_Run_CancellationToken()
        {
            using (var obj = new DerivedStandardTask())
            {
                obj.Run(new CancellationToken());
            }
        }

        [Fact]
        public void prop_CancellationToken()
        {
            Assert.True(new PropertyExpectations<StandardTask>(x => x.CancellationToken)
                            .TypeIs<CancellationToken>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_ContinuationOptions()
        {
            Assert.True(new PropertyExpectations<StandardTask>(x => x.ContinuationOptions)
                            .TypeIs<TaskContinuationOptions>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_ContinuationOptions_get()
        {
            const TaskContinuationOptions expected = TaskContinuationOptions.None;
            var actual = new DerivedStandardTask().ContinuationOptions;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_CreationOptions()
        {
            Assert.True(new PropertyExpectations<StandardTask>(x => x.CreationOptions)
                            .TypeIs<TaskCreationOptions>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_CreationOptions_get()
        {
            const TaskCreationOptions expected = TaskCreationOptions.None;
            var actual = new DerivedStandardTask().CreationOptions;

            Assert.Equal(expected, actual);
        }
    }
}