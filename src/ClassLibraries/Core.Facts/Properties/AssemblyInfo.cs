using System;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Core.Facts.dll")]
[assembly: AssemblyTitle("Cavity.Core.Facts.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Core Facts Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Core Facts Library (Release)")]

#endif