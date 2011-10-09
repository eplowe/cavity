using System;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Example.Autofac.Console.exe")]
[assembly: AssemblyTitle("Example.Autofac.Console.exe")]

#if (DEBUG)

[assembly: AssemblyDescription("Example Autofac Console Application (Debug)")]

#else

[assembly: AssemblyDescription("Example Autofac Console Application (Release)")]

#endif