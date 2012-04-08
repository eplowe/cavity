using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(false)]
[assembly: AssemblyDefaultAlias("Cavity.$safeprojectname$.dll")]
[assembly: AssemblyTitle("Cavity.$safeprojectname$.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : $safeprojectname$ Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : $safeprojectname$ Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]