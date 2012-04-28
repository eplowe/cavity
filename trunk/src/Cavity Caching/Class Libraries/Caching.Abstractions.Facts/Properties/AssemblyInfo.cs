using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(false)]
[assembly: AssemblyDefaultAlias("Cavity.Caching.Abstractions.Facts.dll")]
[assembly: AssemblyTitle("Cavity.Caching.Abstractions.Facts.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Caching Abstractions Facts Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Caching Abstractions Facts Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]