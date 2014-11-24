using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Testing.Unit.dll")]
[assembly: AssemblyTitle("Cavity.Testing.Unit.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Unit Testing Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Unit Testing Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity", Justification = "This is a root namespace.")]