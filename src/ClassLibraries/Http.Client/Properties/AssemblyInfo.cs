using System;
using System.Reflection;

[assembly: AssemblyDefaultAlias("Cavity.Http.Client.dll")]
[assembly: AssemblyTitle("Cavity.Http.Client.dll")]

#if (DEBUG)
[assembly: AssemblyDescription("Cavity : HTTP Client Library (Debug)")]
#else
[assembly: AssemblyDescription("Cavity : HTTP Client Library (Release)")]
#endif

[assembly: CLSCompliant(true)]