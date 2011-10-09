using System;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Example.dll")]
[assembly: AssemblyTitle("Example.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Example Website (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Example Website (Release)")]

#endif