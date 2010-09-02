using System;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Domain.dll")]
[assembly: AssemblyTitle("Cavity.Domain.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Domain Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Domain Library (Release)")]

#endif