using System;
using System.Reflection;

[assembly: AssemblyDefaultAlias("Cavity.Testing.Unit.Facts.dll")]
[assembly: AssemblyTitle("Cavity.Testing.Unit.Facts.dll")]

#if (DEBUG)
[assembly: AssemblyDescription("Cavity : Unit Testing Facts Library (Debug)")]
#else
[assembly: AssemblyDescription("Cavity : Unit Testing Facts Library (Release)")]
#endif

#region CLS
[assembly: CLSCompliant(true)]
#endregion