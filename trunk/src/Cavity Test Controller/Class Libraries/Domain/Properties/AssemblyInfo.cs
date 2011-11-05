using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.TestController.Domain.dll")]
[assembly: AssemblyTitle("Cavity.TestController.Domain.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Test Controller Domain Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Test Controller Domain Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]