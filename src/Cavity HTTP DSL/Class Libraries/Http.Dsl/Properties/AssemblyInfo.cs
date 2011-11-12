using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Http.Dsl.dll")]
[assembly: AssemblyTitle("Cavity.Http.Dsl.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : HTTP DSL Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : HTTP DSL Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]