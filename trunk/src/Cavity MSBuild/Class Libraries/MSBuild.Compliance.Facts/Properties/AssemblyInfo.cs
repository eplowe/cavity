using System;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Build.Facts.dll")]
[assembly: AssemblyTitle("Cavity.Build.Facts.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Build Facts Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Build Facts Library (Release)")]

#endif