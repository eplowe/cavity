using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(false)]
[assembly: AssemblyDefaultAlias("Cavity.Http.Testing.Facts.dll")]
[assembly: AssemblyTitle("Cavity.Http.Testing.Facts.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : HTTP Testing Facts Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : HTTP Testing Facts Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity", Justification = "This is a root namespace.")]