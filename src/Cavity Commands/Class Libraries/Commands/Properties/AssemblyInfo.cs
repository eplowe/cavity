using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Commands.dll")]
[assembly: AssemblyTitle("Cavity.Commands.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Commands Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Commands Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity", Justification = "This is a root namespace.")]