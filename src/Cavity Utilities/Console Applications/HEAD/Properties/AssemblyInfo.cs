using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("HEAD.exe")]
[assembly: AssemblyTitle("HEAD.exe")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : HEAD Console Application (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : HEAD Console Application (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "HEAD", Justification = "This casing is correct.")]
