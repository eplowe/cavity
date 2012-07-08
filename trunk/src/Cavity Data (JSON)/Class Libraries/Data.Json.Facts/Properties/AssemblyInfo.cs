using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(false)]
[assembly: AssemblyDefaultAlias("Cavity.Data.Json.Facts.dll")]
[assembly: AssemblyTitle("Cavity.Data.Json.Facts.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : JSON Data Facts Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : JSON Data Facts Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]