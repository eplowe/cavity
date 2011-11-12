using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Http.Abstractions.Facts.dll")]
[assembly: AssemblyTitle("Cavity.Http.Abstractions.Facts.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : HTTP Abstractions Facts Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : HTTP Abstractions Facts Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]