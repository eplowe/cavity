using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Web.Mvc3.dll")]
[assembly: AssemblyTitle("Cavity.Web.Mvc3.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : ASP.NET MVC3 Web Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : ASP.NET MVC3 Web Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]