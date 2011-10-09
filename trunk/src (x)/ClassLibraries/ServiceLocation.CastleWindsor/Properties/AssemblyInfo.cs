using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.ServiceLocation.CastleWindsor.dll")]
[assembly: AssemblyTitle("Cavity.ServiceLocation.CastleWindsor.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Castle Windsor Service Location Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Castle Windsor Service Location Library (Release)")]

#endif

[assembly: SuppressMessage("Microsoft.Design", "CA2210:AssembliesShouldHaveValidStrongNames", Justification = "CommonServiceLocator.WindsorAdapter is not strongly named.")]