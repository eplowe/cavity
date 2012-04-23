using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(false)]
[assembly: AssemblyDefaultAlias("Cavity.Data.Html.Facts.dll")]
[assembly: AssemblyTitle("Cavity.Data.Html.Facts.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Data HTML Facts Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Data HTML Facts Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]