using System;
using System.Reflection;

[assembly: AssemblyDefaultAlias("Example.StructureMap.Console.exe")]
[assembly: AssemblyTitle("Example.StructureMap.Console.exe")]

#if (DEBUG)

[assembly: AssemblyDescription("Example StructureMap Console Application (Debug)")]

#else

[assembly: AssemblyDescription("Example StructureMap Console Application (Release)")]

#endif

[assembly: CLSCompliant(true)]