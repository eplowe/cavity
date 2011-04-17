namespace Cavity.Diagnostics
{
    using Moq;
    using Xunit;

    public sealed class ProcessFacadeFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(ProcessFacade).IsStatic());
        }

        [Fact]
        public void op_Reset()
        {
            var expected = new Mock<IProcess>().Object;
            ProcessFacade.Mock = expected;

            ProcessFacade.Reset();

            Assert.IsType<StandardProcess>(ProcessFacade.Current);
        }

        [Fact]
        public void prop_Current_get()
        {
            Assert.IsType<StandardProcess>(ProcessFacade.Current);
        }

        [Fact]
        public void prop_Mock_get()
        {
            Assert.Null(ProcessFacade.Mock);
        }

        [Fact]
        public void prop_Mock_set()
        {
            try
            {
                var expected = new Mock<IProcess>().Object;
                ProcessFacade.Mock = expected;

                var actual = ProcessFacade.Mock;

                Assert.Same(expected, actual);
            }
            finally
            {
                ProcessFacade.Reset();
            }
        }
    }
}