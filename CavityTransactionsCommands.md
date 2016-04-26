## NuGet Packages ##

http://nuget.org/List/Packages/Cavity.Transactions.Commands

## using Cavity.Transactions _[Facts](http://code.google.com/p/cavity/source/browse/#svn%2Ftrunk%2Fsrc%2FCavity%20Commands%20Transactions%2FClass%20Libraries%2FTransactions.Commands.Facts%2FTransactions)_ ##

[DurableEnlistmentNotification](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Commands%20Transactions/Class%20Libraries/Transactions.Commands/Transactions/DurableEnlistmentNotification.cs)

```
class Example : DurableEnlistmentNotification
{
    public Example(Guid identifier, EnlistmentOptions enlistmentOptions)
            : base(identifier, enlistmentOptions)
    {
    }

    protected override bool ConfigureOperation()
    {
        return true;
    }
}
```

```
using (var scope = new TransactionScope())
{
    new Example(Guid.NewGuid(), EnlistmentOptions.None)
        .Operation
        .Commands
        .Add(new DirectoryCreateCommand(path));

    scope.Complete();
}
```