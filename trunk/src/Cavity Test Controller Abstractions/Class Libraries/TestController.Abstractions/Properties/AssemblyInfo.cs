using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.TestController.Abstractions.dll")]
[assembly: AssemblyTitle("Cavity.TestController.Abstractions.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Test Controller Abstractions Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Test Controller Abstractions Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]