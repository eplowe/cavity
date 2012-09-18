using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("CSV.exe")]
[assembly: AssemblyTitle("CSV.exe")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : CSV Console Application (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : CSV Console Application (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CSV", Justification = "This casing is correct.")]
