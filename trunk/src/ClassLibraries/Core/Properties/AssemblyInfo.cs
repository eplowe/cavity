using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: AssemblyDefaultAlias("Cavity.Core.dll")]
[assembly: AssemblyTitle("Cavity.Core.dll")]

#if (DEBUG)
[assembly: AssemblyDescription("Cavity : Core Library (Debug)")]
#else
[assembly: AssemblyDescription("Cavity : Core Library (Release)")]
#endif

[assembly: CLSCompliant(true)]

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]
[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity.IO")]
[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity.Xml")]