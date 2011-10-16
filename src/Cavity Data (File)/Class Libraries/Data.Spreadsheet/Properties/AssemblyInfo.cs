using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Data.File.dll")]
[assembly: AssemblyTitle("Cavity.Data.File.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Data File Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Data File Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity.Data")]