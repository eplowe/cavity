using System;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Example.CastleWindsor.Console.exe")]
[assembly: AssemblyTitle("Example.CastleWindsor.Console.exe")]

#if (DEBUG)

[assembly: AssemblyDescription("Example Castle Windsor Console Application (Debug)")]

#else

[assembly: AssemblyDescription("Example Castle Windsor Console Application (Release)")]

#endif