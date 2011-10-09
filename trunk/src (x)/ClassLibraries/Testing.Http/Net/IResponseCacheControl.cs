namespace Cavity.Net
{
    public interface IResponseCacheControl
    {
        IResponseCacheConditionals HasCacheControl(string value);

        IResponseContentLanguage HasCacheControlNone();

        IResponseCacheConditionals HasCacheControlPrivate();

        IResponseCacheConditionals HasCacheControlPublic();

        IResponseContentLanguage IgnoreCacheControl();
    }
}