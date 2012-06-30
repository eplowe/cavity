using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("CRLF.exe")]
[assembly: AssemblyTitle("CRLF.exe")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Line Endings Application (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Line Endings Application (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Cavity", Justification = "This is a new project.")]

[assembly: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "CRLF", Justification = "This casing is intentional.")]
