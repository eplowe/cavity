using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Http.Client.dll")]
[assembly: AssemblyTitle("Cavity.Http.Client.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : HTTP Client Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : HTTP Client Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]