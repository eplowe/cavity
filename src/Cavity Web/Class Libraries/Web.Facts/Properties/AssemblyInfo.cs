using System;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Web.Facts.dll")]
[assembly: AssemblyTitle("Cavity.Web.Facts.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Web Facts Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Web Facts Library (Release)")]

#endif