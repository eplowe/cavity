using System;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.MessageLibrary.Installer.dll")]
[assembly: AssemblyTitle("Cavity.MessageLibrary.Installer.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Message Library Installer (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Message Library Installer (Release)")]

#endif