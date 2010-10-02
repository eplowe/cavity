namespace Cavity.Net
{
    using System;

    public sealed class IResponseCacheConditionalsDummy : IResponseCacheConditionals
    {
        IResponseContentLanguage IResponseCacheConditionals.IgnoreCacheConditionals()
        {
            throw new NotSupportedException();
        }

        IResponseContentLanguage IResponseCacheConditionals.WithEtag()
        {
            throw new NotSupportedException();
        }

        IResponseContentLanguage IResponseCacheConditionals.WithExpires()
        {
            throw new NotSupportedException();
        }

        IResponseContentLanguage IResponseCacheConditionals.WithLastModified()
        {
            throw new NotSupportedException();
        }
    }
}