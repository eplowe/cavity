using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(false)]
[assembly: AssemblyDefaultAlias("Cavity.Data.Xml.Xunit.Facts.dll")]
[assembly: AssemblyTitle("Cavity.Data.Xml.Xunit.Facts.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : xUnit XML Data Facts Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : xUnit XML Data Facts Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity", Justification = "This is a root namespace.")]