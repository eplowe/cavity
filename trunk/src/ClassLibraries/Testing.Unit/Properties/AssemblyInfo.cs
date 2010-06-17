using System;
using System.Reflection;

[assembly: AssemblyDefaultAlias("Cavity.Testing.Unit.dll")]
[assembly: AssemblyTitle("Cavity.Testing.Unit.dll")]

#if (DEBUG)
[assembly: AssemblyDescription("Cavity : Unit Testing Library (Debug)")]
#else
[assembly: AssemblyDescription("Cavity : Unit Testing Library (Release)")]
#endif

[assembly: CLSCompliant(true)]