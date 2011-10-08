using System;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Diagnostics.Log4Net.Facts.dll")]
[assembly: AssemblyTitle("Cavity.Diagnostics.Log4Net.Facts.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Log4Net Diagnostics Facts Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Log4Net Diagnostics Facts Library (Release)")]

#endif
