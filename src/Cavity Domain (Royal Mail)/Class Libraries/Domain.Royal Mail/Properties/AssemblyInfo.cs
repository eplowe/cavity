using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.UnitedKingdom.dll")]
[assembly: AssemblyTitle("Cavity.UnitedKingdom.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : United Kingdom Domain Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : United Kingdom Domain Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]