using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("TSV.exe")]
[assembly: AssemblyTitle("TSV.exe")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : TSV Console Application (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : TSV Console Application (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "TSV", Justification = "This casing is correct.")]
