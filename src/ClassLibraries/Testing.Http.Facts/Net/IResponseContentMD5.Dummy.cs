namespace Cavity.Net
{
    using System;

    public sealed class IResponseContentMD5Dummy : IResponseContentMD5
    {
        IResponseContent IResponseContentMD5.HasContentMD5()
        {
            throw new NotSupportedException();
        }

        IResponseContent IResponseContentMD5.IgnoreContentMD5()
        {
            throw new NotSupportedException();
        }
    }
}