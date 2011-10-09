using System;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Repository.dll")]
[assembly: AssemblyTitle("Cavity.Repository.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Repository Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Repository Library (Release)")]

#endif