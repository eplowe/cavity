using System;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Testing.Http.dll")]
[assembly: AssemblyTitle("Cavity.Testing.Http.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : HTTP Testing Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : HTTP Testing Library (Release)")]

#endif