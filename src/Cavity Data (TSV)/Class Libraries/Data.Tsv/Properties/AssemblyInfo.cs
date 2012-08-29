using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Data.Tsv.dll")]
[assembly: AssemblyTitle("Cavity.Data.Tsv.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : TSV Data Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : TSV Data Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity.Data")]