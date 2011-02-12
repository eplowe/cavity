using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Build.Tasks.dll")]
[assembly: AssemblyTitle("Cavity.Build.Tasks.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Build Tasks Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Build Tasks Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Naming", "CA1701:ResourceStringCompoundWordsShouldBeCasedCorrectly", MessageId = "XPath", Scope = "resource", Target = "Cavity.Properties.Resources.resources")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1703:ResourceStringsShouldBeSpelledCorrectly", MessageId = "csproj", Scope = "resource", Target = "Cavity.Properties.Resources.resources")]