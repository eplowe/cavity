using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Data.Csv.dll")]
[assembly: AssemblyTitle("Cavity.Data.Csv.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : CSV Data Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : CSV Data Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity.Data", Justification = "This is a root namespace.")]