using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.TaskManagement.dll")]
[assembly: AssemblyTitle("Cavity.TaskManagement.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Task Management Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Task Management Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]