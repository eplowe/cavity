using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Commands.FileSystem.dll")]
[assembly: AssemblyTitle("Cavity.Commands.FileSystem.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : File System Commands Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : File System Commands Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]