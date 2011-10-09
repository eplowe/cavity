using System;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Commands.dll")]
[assembly: AssemblyTitle("Cavity.Commands.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Commands Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Commands Library (Release)")]

#endif