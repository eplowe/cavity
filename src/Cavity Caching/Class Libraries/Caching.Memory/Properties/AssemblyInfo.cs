using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(false)]
[assembly: AssemblyDefaultAlias("Cavity.Caching.Memory.dll")]
[assembly: AssemblyTitle("Cavity.Caching.Memory.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Caching (Memory) Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Caching (Memory) Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity", Justification = "This is a root namespace.")]