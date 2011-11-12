using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Xamples.Net.Facts.dll")]
[assembly: AssemblyTitle("Cavity.Xamples.Net.Facts.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Xamples.Net Facts Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Xamples.Net Facts Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]