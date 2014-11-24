using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Version.exe")]
[assembly: AssemblyTitle("Version.exe")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Version Control Console (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Version Control Console (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity", Justification = "This is a root namespace.")]