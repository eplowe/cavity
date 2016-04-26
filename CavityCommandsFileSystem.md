## NuGet Packages ##

http://nuget.org/List/Packages/Cavity.Commands.FileSystem

## using Cavity.IO _[Facts](http://code.google.com/p/cavity/source/browse/#svn%2Ftrunk%2Fsrc%2FCavity%20Commands%20(File%20System)%2FClass%20Libraries%2FCommands.FileSystem.Facts%2FIO)_ ##

[DirectoryCreateCommand](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Commands%20(File%20System)/Class%20Libraries/Commands.FileSystem/IO/DirectoryCreateCommand.cs)

```
var command = @"<directory.create path='C:\example' undo='true' unidirectional='true' />"
    .XmlDeserialize<DirectoryCreateCommand>();
if (!command.Act())
{
    command.Revert();
}
```

[FileCopyCommand](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Commands%20(File%20System)/Class%20Libraries/Commands.FileSystem/IO/FileCopyCommand.cs)

```
var command = @"<file.copy source='C:\from.txt' destination='C:\to.txt' undo='true' unidirectional='true' />"
    .XmlDeserialize<FileCopyCommand>();
if (!command.Act())
{
    command.Revert();
}
```

[FileMoveCommand](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Commands%20(File%20System)/Class%20Libraries/Commands.FileSystem/IO/FileMoveCommand.cs)

```
var command = @"<file.move source='C:\from.txt' destination='C:\to.txt' undo='true' unidirectional='true' />"
    .XmlDeserialize<FileMoveCommand>();
if (!command.Act())
{
    command.Revert();
}
```