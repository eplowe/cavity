## NuGet Package ##

http://nuget.org/List/Packages/Cavity.Diagnostics.Log4Net

## Usage ##

This package should only be installed to an application project such as a Web Application or Console Application.

The package configures a custom trace listener which directs logging to log4net.

The purpose is to abstract away the invocation of a specific logging provider by allowing all tracing to be implemented by using [System.Diagnostics.Trace](http://msdn.microsoft.com/library/system.diagnostics.trace) or [System.Diagnostics.Debug](http://msdn.microsoft.com/library/system.diagnostics.debug).