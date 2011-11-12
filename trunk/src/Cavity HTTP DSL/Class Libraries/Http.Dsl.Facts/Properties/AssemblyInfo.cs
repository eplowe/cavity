using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Http.Dsl.Facts.dll")]
[assembly: AssemblyTitle("Cavity.Http.Dsl.Facts.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : HTTP DSL Facts Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : HTTP DSL Facts Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]