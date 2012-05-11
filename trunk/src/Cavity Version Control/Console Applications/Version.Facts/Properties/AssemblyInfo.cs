using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(false)]
[assembly: AssemblyDefaultAlias("Version.Facts.dll")]
[assembly: AssemblyTitle("Version.Facts.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Version Control Facts Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Version Control Facts Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]