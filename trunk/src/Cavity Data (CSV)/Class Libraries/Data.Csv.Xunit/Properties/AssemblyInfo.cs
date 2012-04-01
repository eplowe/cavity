using System;
using System.Reflection;

[assembly: CLSCompliant(false)]
[assembly: AssemblyDefaultAlias("Cavity.Data.Csv.Xunit.dll")]
[assembly: AssemblyTitle("Cavity.Data.Csv.Xunit.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : xUnit CSV Data Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : xUnit CSV Data Library (Release)")]

#endif