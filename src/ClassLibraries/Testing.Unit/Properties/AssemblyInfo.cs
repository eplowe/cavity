using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: AssemblyDefaultAlias("Cavity.Testing.Unit.dll")]
[assembly: AssemblyTitle("Cavity.Testing.Unit.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Unit Testing Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Unit Testing Library (Release)")]

#endif

[assembly: CLSCompliant(true)]

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]