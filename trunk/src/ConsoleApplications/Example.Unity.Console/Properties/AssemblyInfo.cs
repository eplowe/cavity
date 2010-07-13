using System;
using System.Reflection;

[assembly: AssemblyDefaultAlias("Example.Unity.Console.exe")]
[assembly: AssemblyTitle("Example.Unity.Console.exe")]

#if (DEBUG)

[assembly: AssemblyDescription("Example Unity Console Application (Debug)")]

#else

[assembly: AssemblyDescription("Example Unity Console Application (Release)")]

#endif

[assembly: CLSCompliant(true)]