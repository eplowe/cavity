using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.TaskManagement.Abstractions.dll")]
[assembly: AssemblyTitle("Cavity.TaskManagement.Abstractions.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Task Management Abstractions Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Task Management Abstractions Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity", Justification = "This is a root namespace.")]