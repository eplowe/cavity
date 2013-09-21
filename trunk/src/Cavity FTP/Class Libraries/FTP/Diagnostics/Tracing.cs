﻿namespace Cavity.Diagnostics
{
    using System.Diagnostics;

    internal static class Tracing
    {
        internal static TraceSwitch Is
        {
            get
            {
                return new TraceSwitch("Cavity.FTP", string.Empty);
            }
        }
    }
}