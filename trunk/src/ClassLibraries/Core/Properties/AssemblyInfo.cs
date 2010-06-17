using System;
using System.Reflection;

[assembly: AssemblyDefaultAlias("Cavity.Core.dll")]
[assembly: AssemblyTitle("Cavity.Core.dll")]

#if (DEBUG)
[assembly: AssemblyDescription("Cavity : Core Library (Debug)")]
#else
[assembly: AssemblyDescription("Cavity : Core Library (Release)")]
#endif

[assembly: CLSCompliant(true)]