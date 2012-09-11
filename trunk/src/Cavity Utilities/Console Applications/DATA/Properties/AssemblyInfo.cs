using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("DATA.exe")]
[assembly: AssemblyTitle("DATA.exe")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Data Conversion Application (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Data Conversion Application (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "DATA", Justification = "This casing is correct.")]
