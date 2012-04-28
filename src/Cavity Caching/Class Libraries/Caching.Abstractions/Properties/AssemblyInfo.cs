using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(false)]
[assembly: AssemblyDefaultAlias("Cavity.Caching.Abstractions.dll")]
[assembly: AssemblyTitle("Cavity.Caching.Abstractions.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Caching Abstractions Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Caching Abstractions Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]