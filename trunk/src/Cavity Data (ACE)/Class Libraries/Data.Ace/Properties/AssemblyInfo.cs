using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Data.Ace.dll")]
[assembly: AssemblyTitle("Cavity.Data.Ace.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : ACE Data Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : ACE Data Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity.Data", Justification = "This is a root namespace.")]