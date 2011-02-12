using System;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.TaskManagement.Service.exe")]
[assembly: AssemblyTitle("Cavity.TaskManagement.Service.exe")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Task Management Service (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Task Management Service (Release)")]

#endif