## NuGet Packages ##

http://nuget.org/List/Packages/Cavity.Commands

## using Cavity _[Facts](http://code.google.com/p/cavity/source/browse/#svn%2Ftrunk%2Fsrc%2FCavity%20Commands%2FClass%20Libraries%2FCommands.Facts)_ ##

[ICommand](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Commands/Class%20Libraries/Commands/ICommand.cs)

```
class ExampleCommand : ICommand
{
    bool ICommand.Act()
    {
    }
    
    bool ICommand.Revert()
    {
    }
    
    XmlSchema IXmlSerializable.GetSchema()
    {
    }
    
    void IXmlSerializable.ReadXml(XmlReader reader)
    {
    }
    
    void IXmlSerializable.WriteXml(XmlWriter writer)
    {
    }
}
```

[Command](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Commands/Class%20Libraries/Commands/Command.cs)

```
[XmlRoot("example")]
class ExampleCommand : Command
{
    public override bool Act()
    {
    }
    
    public override bool Revert()
    {
    }
}
```

## using Cavity.Collections _[Facts](http://code.google.com/p/cavity/source/browse/#svn%2Ftrunk%2Fsrc%2FCavity%20Commands%2FClass%20Libraries%2FCommands.Facts%2FCollections)_ ##

[CommandCollection](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Commands/Class%20Libraries/Commands/Collections/CommandCollection.cs)

```
var commands = new CommandCollection
{
    new ExampleCommand()
};

var file = new DirectoryInfo("C:\\").ToFile("commands.xml");

file.CreateNew(commands.XmlSerialize());

commands = file.ReadToEnd().XmlDeserialize<CommandCollection>();
if (!commands.Do())
{
    commands.Undo();
}
```