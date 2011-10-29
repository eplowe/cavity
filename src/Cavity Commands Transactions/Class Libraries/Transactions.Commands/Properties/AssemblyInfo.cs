using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Transactions.Commands.dll")]
[assembly: AssemblyTitle("Cavity.Transactions.Commands.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Commands Transactions Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Commands Transactions Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity")]