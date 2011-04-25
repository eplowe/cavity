using System;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.TaskManagement.dll")]
[assembly: AssemblyTitle("Cavity.TaskManagement.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : Task Management Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : Task Management Library (Release)")]

#endif

[assembly:log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]