using System;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.ServiceLocation.dll")]
[assembly: AssemblyTitle("Cavity.ServiceLocation.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Service Location Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Service Location Library (Release)")]

#endif