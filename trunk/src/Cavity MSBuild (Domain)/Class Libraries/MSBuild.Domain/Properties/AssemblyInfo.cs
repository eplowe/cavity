using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.MSBuild.Domain.dll")]
[assembly: AssemblyTitle("Cavity.MSBuild.Domain.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Domain MSBuild Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Domain MSBuild Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity", Justification = "This is a root namespace.")]