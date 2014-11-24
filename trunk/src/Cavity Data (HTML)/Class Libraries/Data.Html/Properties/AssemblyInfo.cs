using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(false)]
[assembly: AssemblyDefaultAlias("Cavity.Data.Html.dll")]
[assembly: AssemblyTitle("Cavity.Data.Html.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Data HTML Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Data HTML Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity", Justification = "This is a root namespace.")]