using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

#region General information
[assembly: AssemblyCompany("Cavity")]
[assembly: AssemblyProduct("http://code.google.com/p/cavity/")]
[assembly: AssemblyCopyright("Copyright © 2010 Alan Dean")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: NeutralResourcesLanguageAttribute("en-GB")]
#endregion

#if (DEBUG)
[assembly: AssemblyConfiguration("Debug Build")]
#else
[assembly: AssemblyConfiguration("Release Build")]
#endif

#region COM
[assembly: ComVisible(false)]
#endregion