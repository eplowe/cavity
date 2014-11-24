using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(false)]
[assembly: AssemblyDefaultAlias("Cavity.Caching.Web.Facts.dll")]
[assembly: AssemblyTitle("Cavity.Caching.Web.Facts.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Caching (Web) Facts Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Caching (Web) Facts Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity", Justification = "This is a root namespace.")]