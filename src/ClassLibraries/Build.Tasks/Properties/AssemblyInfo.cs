using System;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Build.Tasks.dll")]
[assembly: AssemblyTitle("Cavity.Build.Tasks.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Build Tasks Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Build Tasks Library (Release)")]

#endif