using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(false)]
[assembly: AssemblyDefaultAlias("Cavity.Data.Excel.OpenXML.dll")]
[assembly: AssemblyTitle("Cavity.Data.Excel.OpenXML.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Excel Open XML Data Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Excel Open XML Data Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "XML", Justification = "This casing is correct.")]