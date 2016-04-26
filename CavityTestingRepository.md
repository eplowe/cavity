## NuGet Packages ##

http://nuget.org/List/Packages/Cavity.Testing.Repository

## using Cavity.Data _[Facts](http://code.google.com/p/cavity/source/browse/#svn%2Ftrunk%2Fsrc%2FCavity%20Repository%20Testing%2FClass%20Libraries%2FTesting.Repository.Facts%2FData)_ ##

[RepositoryExpectations&lt;T&gt;](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Repository%20Testing/Class%20Libraries/Testing.Repository/Data/RepositoryExpectations%601.cs)

```
// verify value type behaviour
new RepositoryExpectations<int>().VerifyAll<FileRepository<int>>();

// verify reference type behaviour
new RepositoryExpectations<Example>().VerifyAll<FileRepository<Example>>();
```