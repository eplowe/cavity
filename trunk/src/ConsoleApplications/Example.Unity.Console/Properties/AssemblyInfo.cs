using System;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Example.Unity.Console.exe")]
[assembly: AssemblyTitle("Example.Unity.Console.exe")]

#if (DEBUG)

[assembly: AssemblyDescription("Example Unity Console Application (Debug)")]

#else

[assembly: AssemblyDescription("Example Unity Console Application (Release)")]

#endif