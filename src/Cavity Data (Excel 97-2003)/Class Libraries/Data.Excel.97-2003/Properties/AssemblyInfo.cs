using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(false)]
[assembly: AssemblyDefaultAlias("Cavity.Data.Excel.97-2003.dll")]
[assembly: AssemblyTitle("Cavity.Data.Excel.97-2003.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Excel 97-2003 Data Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Excel 97-2003 Data Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]