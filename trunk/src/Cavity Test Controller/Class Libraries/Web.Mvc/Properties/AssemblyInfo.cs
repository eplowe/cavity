using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Web.Mvc.dll")]
[assembly: AssemblyTitle("Cavity.Web.Mvc.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Web MVC Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Web MVC Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]