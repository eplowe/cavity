namespace Cavity.Diagnostics
{
    using System.Diagnostics;

    using Xunit;

    public sealed class Log4NetTraceListenerFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Log4NetTraceListener>()
                            .DerivesFrom<TraceListener>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new Log4NetTraceListener());
        }

        [Fact]
        public void op_Fail_string()
        {
            new Log4NetTraceListener().Fail("message");
        }

        [Fact]
        public void op_Fail_string_string()
        {
            new Log4NetTraceListener().Fail("message", "detail");
        }
    }
}