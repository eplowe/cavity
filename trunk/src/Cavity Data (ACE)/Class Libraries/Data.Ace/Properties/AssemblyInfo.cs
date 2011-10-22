using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Data.Spreadsheet.dll")]
[assembly: AssemblyTitle("Cavity.Data.Spreadsheet.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Data Spreadsheet Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Data Spreadsheet Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity.Data")]