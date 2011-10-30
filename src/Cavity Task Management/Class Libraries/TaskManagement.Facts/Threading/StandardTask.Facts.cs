namespace Cavity.Threading
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Moq;
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
        public void op_CreateInstance_CancellationToken()
        {
            var expected = new Mock<IThreadedObject>();
            using (var obj = new DerivedStandardTask(expected.Object))
            {
                using (var actual = obj.CreateInstance())
                {
                    Assert.Same(expected.Object, actual);
                }
            }
        }

        [Fact]
        public void op_Run_CancellationToken()
        {
            using (var obj = new DerivedStandardTask(new Mock<IThreadedObject>().Object))
            {
                obj.Run(new CancellationToken());
            }
        }

        [Fact]
        public void op_Run_CancellationToken_whenException()
        {
            using (var obj = new DerivedStandardTask(new Mock<IThreadedObject>().Object)
            {
                ThrowException = true
            })
            {
                obj.Run(new CancellationToken());
            }
        }

        [Fact]
        public void op_Run_CancellationToken_whenNullInstance()
        {
            using (var obj = new DerivedStandardTask(null))
            {
                obj.Run(new CancellationToken());
            }
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