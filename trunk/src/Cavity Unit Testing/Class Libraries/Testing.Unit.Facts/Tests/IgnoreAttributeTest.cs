namespace Cavity.Tests
{
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;

    [DebuggerDisplay("")]
    [DebuggerStepThrough]
#if !NET20 && !NET35
    [ExcludeFromCodeCoverage]
#endif
    [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Implements", Justification = "This is for testing purposes.")]
    public sealed class IgnoreAttributeTest
    {
#if !NET20 && !NET35
        public dynamic Data { get; set; }
#endif

        [Browsable(true)]
        [Category("")]
        [Description("")]
        [Editor]
        public string Value { get; set; }
    }
}