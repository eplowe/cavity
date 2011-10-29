using System;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Testing.HTTP.Facts.dll")]
[assembly: AssemblyTitle("Cavity.Testing.HTTP.Facts.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : HTTP Testing Facts Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : HTTP Testing Facts Library (Release)")]

#endif