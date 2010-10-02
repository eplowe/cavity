namespace Cavity.Net
{
    using System;

    public sealed class IResponseCacheControlDummy : IResponseCacheControl
    {
        IResponseCacheConditionals IResponseCacheControl.HasCacheControl(string value)
        {
            throw new NotSupportedException();
        }

        IResponseContentLanguage IResponseCacheControl.HasCacheControlNone()
        {
            throw new NotSupportedException();
        }

        IResponseCacheConditionals IResponseCacheControl.HasCacheControlPrivate()
        {
            throw new NotSupportedException();
        }

        IResponseCacheConditionals IResponseCacheControl.HasCacheControlPublic()
        {
            throw new NotSupportedException();
        }

        IResponseContentLanguage IResponseCacheControl.IgnoreCacheControl()
        {
            throw new NotSupportedException();
        }
    }
}