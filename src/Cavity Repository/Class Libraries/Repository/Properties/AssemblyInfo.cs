using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Repository.dll")]
[assembly: AssemblyTitle("Cavity.Repository.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Repository Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Repository Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity", Justification = "This is a root namespace.")]