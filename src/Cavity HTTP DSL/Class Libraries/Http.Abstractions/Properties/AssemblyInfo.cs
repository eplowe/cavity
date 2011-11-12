using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Http.Abstractions.dll")]
[assembly: AssemblyTitle("Cavity.Http.Abstractions.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : HTTP Abstractions Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : HTTP Abstractions Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]