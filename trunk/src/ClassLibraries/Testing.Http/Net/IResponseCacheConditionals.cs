namespace Cavity.Net
{
    public interface IResponseCacheConditionals
    {
        IResponseContentLanguage IgnoreCacheConditionals();

        IResponseContentLanguage WithEtag();

        IResponseContentLanguage WithExpires();

        IResponseContentLanguage WithLastModified();
    }
}