namespace Cavity.Win32
{
    using System.Diagnostics.CodeAnalysis;
    using Cavity.Collections.Generic;

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This naming is deliberate.")]
    public sealed class FakeRegistry : MultitonCollection<string, MultitonCollection<string, object>>
    {
    }
}