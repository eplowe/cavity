using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(false)]
[assembly: AssemblyDefaultAlias("Cavity.Data.Xml.Xunit.dll")]
[assembly: AssemblyTitle("Cavity.Data.Xml.Xunit.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : xUnit XML Data Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : xUnit XML Data Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]