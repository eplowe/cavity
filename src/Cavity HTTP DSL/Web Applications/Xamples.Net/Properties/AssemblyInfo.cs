using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Xamples.Net.dll")]
[assembly: AssemblyTitle("Cavity.Xamples.Net.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Xamples.Net Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Xamples.Net Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]