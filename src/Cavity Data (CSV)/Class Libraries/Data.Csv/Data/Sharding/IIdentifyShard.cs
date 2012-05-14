namespace Cavity.Data.Sharding
{
    using Cavity.Collections;

    public interface IIdentifyShard
    {
        string IdentifyShard(KeyStringDictionary entry);
    }
}