using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Http.Generalizations.dll")]
[assembly: AssemblyTitle("Cavity.Http.Generalizations.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : HTTP Generalizations Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : HTTP Generalizations Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]