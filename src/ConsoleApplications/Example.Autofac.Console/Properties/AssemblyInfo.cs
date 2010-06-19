using System;
using System.Reflection;

[assembly: AssemblyDefaultAlias("Example.Autofac.Console.exe")]
[assembly: AssemblyTitle("Example.Autofac.Console.exe")]

#if (DEBUG)
[assembly: AssemblyDescription("Example Autofac Console Application (Debug)")]
#else
[assembly: AssemblyDescription("Example Autofac Console Application (Release)")]
#endif

[assembly: CLSCompliant(true)]