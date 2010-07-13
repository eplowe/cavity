using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: AssemblyDefaultAlias("Cavity.Http.Client.dll")]
[assembly: AssemblyTitle("Cavity.Http.Client.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : HTTP Client Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : HTTP Client Library (Release)")]

#endif

[assembly: CLSCompliant(true)]

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity.Net.Mime")]
[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity.Net.Sockets")]
[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity.Text")]
[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity.Xml")]