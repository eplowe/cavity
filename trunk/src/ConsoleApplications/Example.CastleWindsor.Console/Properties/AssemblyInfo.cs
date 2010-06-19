using System;
using System.Reflection;

[assembly: AssemblyDefaultAlias("Example.CastleWindsor.Console.exe")]
[assembly: AssemblyTitle("Example.CastleWindsor.Console.exe")]

#if (DEBUG)
[assembly: AssemblyDescription("Example Castle Windsor Console Application (Debug)")]
#else
[assembly: AssemblyDescription("Example Castle Windsor Console Application (Release)")]
#endif

[assembly: CLSCompliant(true)]