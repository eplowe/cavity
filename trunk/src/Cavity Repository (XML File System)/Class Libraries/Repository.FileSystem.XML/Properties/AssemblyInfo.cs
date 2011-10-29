using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Repository.FileSystem.XML.dll")]
[assembly: AssemblyTitle("Cavity.Repository.FileSystem.XML.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : XML File System Repository Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : XML File System Repository Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]