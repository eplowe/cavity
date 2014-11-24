using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Web.dll")]
[assembly: AssemblyTitle("Cavity.Web.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Web Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Web Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity", Justification = "This is a root namespace.")]