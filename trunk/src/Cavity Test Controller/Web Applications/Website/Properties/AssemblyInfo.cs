using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.TestController.Website.dll")]
[assembly: AssemblyTitle("Cavity.TestController.Website.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Test Controller Website Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Test Controller Website Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]
[assembly: SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames", Justification = "There are unsigned dependencies.")]