using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.ServiceLocation.StructureMap.dll")]
[assembly: AssemblyTitle("Cavity.ServiceLocation.StructureMap.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : StructureMap Service Location Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : StructureMap Service Location Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames", Justification = "StructureMapAdapter is not strongly named.")]