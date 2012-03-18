namespace Cavity
{
    using System;

    using Xunit;

    public sealed class StaticFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(Static<DateTime?>).IsStatic());
        }

        [Fact]
        public void op_Reset()
        {
            try
            {
                Static<string>.Instance = "example";
                Static<string>.Reset();

                Assert.Null(Static<string>.Instance);
            }
            finally
            {
                Static<DateTime?>.Reset();
            }
        }

        [Fact]
        public void prop_Instance_get()
        {
            Assert.Null(Static<string>.Instance);
        }

        [Fact]
        public void prop_Instance_set()
        {
            try
            {
                var expected = DateTime.UtcNow.AddDays(1);
                Static<DateTime?>.Instance = expected;

                var actual = Static<DateTime?>.Instance;

                Assert.Equal(expected, actual);
            }
            finally
            {
                Static<DateTime?>.Reset();
            }
        }
    }
}