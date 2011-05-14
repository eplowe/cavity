namespace Cavity.Diagnostics
{
    using System.Diagnostics;

    internal static class Tracing
    {
        internal static bool Enabled
        {
            get
            {
                return new BooleanSwitch("Cavity.Commands", string.Empty).Enabled;
            }
        }
    }
}