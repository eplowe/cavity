using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(false)]
[assembly: AssemblyDefaultAlias("Cavity.Core.Facts.dll")]
[assembly: AssemblyTitle("Cavity.Core.Facts.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Core Facts Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Core Facts Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "XPath", Scope = "namespace", Target = "Cavity.Xml.XPath", Justification = "Following the BCL naming style.")]