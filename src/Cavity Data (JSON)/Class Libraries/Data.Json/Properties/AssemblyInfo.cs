using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(false)]
[assembly: AssemblyDefaultAlias("Cavity.Data.Json.dll")]
[assembly: AssemblyTitle("Cavity.Data.Json.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : JSON Data Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : JSON Data Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity", Justification = "This is a root namespace.")]