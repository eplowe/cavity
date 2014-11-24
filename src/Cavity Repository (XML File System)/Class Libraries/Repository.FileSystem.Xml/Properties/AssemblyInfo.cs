using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Repository.FileSystem.Xml.dll")]
[assembly: AssemblyTitle("Cavity.Repository.FileSystem.Xml.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : XML File System Repository Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : XML File System Repository Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity", Justification = "This is a root namespace.")]