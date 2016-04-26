## NuGet Packages ##

http://nuget.org/List/Packages/Cavity.Repository

## using Cavity.Data _[Facts](http://code.google.com/p/cavity/source/browse/#svn%2Ftrunk%2Fsrc%2FCavity%20Repository%2FClass%20Libraries%2FRepository.Facts%2FData)_ ##

[Record&lt;T&gt;](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Repository/Class%20Libraries/Repository/Data/Record%601.cs)

```
var record = new Record<int>
{
    Value = 123
};
```

[IRepository&lt;T&gt;](http://code.google.com/p/cavity/source/browse/trunk/src/Cavity%20Repository/Class%20Libraries/Repository/Data/IRepository%601.cs)

```
class ExampleRepository : IRepository<T>
{
    bool IRepository<T>.Delete(AbsoluteUri urn)
    {
    }

    bool IRepository<T>.Delete(AlphaDecimal key)
    {
    }

    bool IRepository<T>.Exists(AbsoluteUri urn)
    {
    }

    bool IRepository<T>.Exists(AlphaDecimal key)
    {
    }

    IRecord<T> IRepository<T>.Insert(IRecord<T> record)
    {
    }

    bool IRepository<T>.Match(AbsoluteUri urn, EntityTag etag)
    {
    }

    bool IRepository<T>.Match(AlphaDecimal key, EntityTag etag)
    {
    }

    bool IRepository<T>.ModifiedSince(AbsoluteUri urn, DateTime value)
    {
    }

    bool IRepository<T>.ModifiedSince(AlphaDecimal key, DateTime value)
    {
    }

    IEnumerable<IRecord<T>> IRepository<T>.Query(XPathExpression expression)
    {
    }

    IRecord<T> IRepository<T>.Select(AbsoluteUri urn)
    {
    }

    IRecord<T> IRepository<T>.Select(AlphaDecimal key)
    {
    }

    AlphaDecimal? IRepository<T>.ToKey(AbsoluteUri urn)
    {
    }

    AbsoluteUri IRepository<T>.ToUrn(AlphaDecimal key)
    {
    }

    IRecord<T> IRepository<T>.Update(IRecord<T> record)
    {
    }

    IRecord<T> IRepository<T>.Upsert(IRecord<T> record)
    {
    }
}
```