using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(false)]
[assembly: AssemblyDefaultAlias("Cavity.Caching.Web.dll")]
[assembly: AssemblyTitle("Cavity.Caching.Web.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Caching (Web) Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Caching (Web) Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]