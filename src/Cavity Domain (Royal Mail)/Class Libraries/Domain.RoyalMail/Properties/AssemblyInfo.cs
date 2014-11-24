using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.RoyalMail.dll")]
[assembly: AssemblyTitle("Cavity.RoyalMail.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Royal Mail Domain Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Royal Mail Domain Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity", Justification = "This is a root namespace.")]