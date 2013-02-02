using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("LINES.exe")]
[assembly: AssemblyTitle("LINES.exe")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : LINES Console Application (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : LINES Console Application (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "LINES", Justification = "This casing is correct.")]
