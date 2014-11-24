using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(false)]
[assembly: AssemblyDefaultAlias("Cavity.Data.Html.Xunit.dll")]
[assembly: AssemblyTitle("Cavity.Data.Html.Xunit.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : xUnit HTML Data Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : xUnit HTML Data Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity", Justification = "This is a root namespace.")]