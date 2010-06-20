using System;
using System.Reflection;

[assembly: AssemblyDefaultAlias("Cavity.Http.dll")]
[assembly: AssemblyTitle("Cavity.Http.dll")]

#if (DEBUG)
[assembly: AssemblyDescription("Cavity : HTTP Library (Debug)")]
#else
[assembly: AssemblyDescription("Cavity : HTTP Library (Release)")]
#endif

[assembly: CLSCompliant(true)]