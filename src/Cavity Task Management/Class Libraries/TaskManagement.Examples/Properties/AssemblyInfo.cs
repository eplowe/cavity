using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.TaskManagement.Examples.dll")]
[assembly: AssemblyTitle("Cavity.TaskManagement.Examples.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Task Management Examples Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Task Management Examples Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]