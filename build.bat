@ECHO off

FOR /f "tokens=1,2*" %%a IN ('date /T') DO SET date=%%a %%b

SET y=%date:~6,4%
SET m=%date:~0,2%
SET d=%date:~3,2%

FOR /f "tokens=1-2 delims=/:" %%a IN ("%TIME%") DO (SET t=%%a%%b)

SET utc=%y%-%m%-%d%+%t%

MSBUILD build.xml ^
/p:Configuration=Release ^
/p:TargetFrameworkVersion=v2.0 ^
/p:Versioning=Subversion ^
/l:FileLogger,Microsoft.Build.Engine;logfile="%utc% build [v2.0].log";append=true;verbosity=diagnostic;encoding=utf-8

PAUSE

MSBUILD build.xml ^
/p:Configuration=Release ^
/p:TargetFrameworkVersion=v3.5 ^
/p:Versioning=Subversion ^
/l:FileLogger,Microsoft.Build.Engine;logfile="%utc% build [v3.5].log";append=true;verbosity=diagnostic;encoding=utf-8

PAUSE

MSBUILD build.xml ^
/p:Configuration=Release ^
/p:TargetFrameworkVersion=v4.0 ^
/p:Versioning=Subversion ^
/l:FileLogger,Microsoft.Build.Engine;logfile="%utc% build [v4.0].log";append=true;verbosity=diagnostic;encoding=utf-8

CHOICE /T 10 /D N /M "Do you want to bundle?"
IF ERRORLEVEL == 2 GOTO END

PAUSE

MSBUILD bundle.xml ^
/p:Configuration=Release ^
/p:TargetFrameworkVersion=v2.0 ^
/p:Versioning=Subversion ^
/l:FileLogger,Microsoft.Build.Engine;logfile="%utc% bundle [v2.0].log";append=true;verbosity=diagnostic;encoding=utf-8

PAUSE

MSBUILD bundle.xml ^
/p:Configuration=Release ^
/p:TargetFrameworkVersion=v3.5 ^
/p:Versioning=Subversion ^
/l:FileLogger,Microsoft.Build.Engine;logfile="%utc% bundle [v3.5].log";append=true;verbosity=diagnostic;encoding=utf-8

PAUSE

MSBUILD bundle.xml ^
/p:Configuration=Release ^
/p:TargetFrameworkVersion=v4.0 ^
/p:Versioning=Subversion ^
/l:FileLogger,Microsoft.Build.Engine;logfile="%utc% bundle [v4.0].log";append=true;verbosity=diagnostic;encoding=utf-8

:END