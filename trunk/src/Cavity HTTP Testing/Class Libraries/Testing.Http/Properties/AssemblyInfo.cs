using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Testing.Http.dll")]
[assembly: AssemblyTitle("Cavity.Testing.Http.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : HTTP Testing Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : HTTP Testing Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]