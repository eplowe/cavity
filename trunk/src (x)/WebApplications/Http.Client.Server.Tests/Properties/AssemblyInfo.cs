using System;
using System.Reflection;

[assembly: CLSCompliant(true)]
[assembly: AssemblyDefaultAlias("Cavity.Http.Client.Server.Tests.dll")]
[assembly: AssemblyTitle("Cavity.Http.Client.Server.Tests.dll")]

#if (DEBUG)

[assembly: AssemblyDescription("Cavity : HTTP Client-Server Tests Library (Debug)")]

#else

[assembly: AssemblyDescription("Cavity : HTTP Client-Server Tests Library (Release)")]

#endif