namespace Cavity.Diagnostics
{
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;

    internal static class Tracing
    {
        internal static bool Enabled
        {
            get
            {
                return new BooleanSwitch("Cavity.Core", string.Empty).Enabled;
            }
        }
    }
}