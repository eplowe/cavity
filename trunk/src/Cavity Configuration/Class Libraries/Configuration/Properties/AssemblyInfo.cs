using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Configuration.dll")]
[assembly: AssemblyTitle("Cavity.Configuration.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Configuration Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Configuration Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]