namespace Cavity.Diagnostics
{
    using System;
    using Cavity;
    using Xunit;

    public sealed class ProcessFacadeFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(ProcessFacade).IsStatic());
        }
    }
}