using System;
using System.Reflection;

[assembly: AssemblyDefaultAlias("Example.dll")]
[assembly: AssemblyTitle("Example.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Example Library (Debug)")]

#else

[assembly: AssemblyDescription("Example Library (Release)")]

#endif

[assembly: CLSCompliant(true)]