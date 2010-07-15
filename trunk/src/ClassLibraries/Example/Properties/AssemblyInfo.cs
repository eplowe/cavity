using System;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Example.dll")]
[assembly: AssemblyTitle("Example.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Example Library (Debug)")]

#else

[assembly: AssemblyDescription("Example Library (Release)")]

#endif