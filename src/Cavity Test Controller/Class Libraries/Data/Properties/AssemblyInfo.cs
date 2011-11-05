using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.TestController.Data.dll")]
[assembly: AssemblyTitle("Cavity.TestController.Data.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Test Controller Data Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Test Controller Data Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]