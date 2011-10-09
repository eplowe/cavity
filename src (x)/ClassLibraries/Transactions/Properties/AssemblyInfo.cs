using System;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Transactions.dll")]
[assembly: AssemblyTitle("Cavity.Transactions.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Transactions Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Transactions Library (Release)")]

#endif