using System;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Testing.Http.Facts.dll")]
[assembly: AssemblyTitle("Cavity.Testing.Http.Facts.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : HTTP Testing Facts Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : HTTP Testing Facts Library (Release)")]

#endif