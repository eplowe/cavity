using System;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Diagnostics.Log4Net.dll")]
[assembly: AssemblyTitle("Cavity.Diagnostics.Log4Net.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : log4net Diagnostics Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : log4net Diagnostics Library (Release)")]

#endif

#if !NET20
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config", Watch = true)]
#endif