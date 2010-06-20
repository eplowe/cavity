namespace Cavity.Net
{
    using System;

    public class IHttpResponseDummy : IHttpResponse
    {
        public StatusLine StatusLine
        {
            get
            {
                throw new NotSupportedException();
            }
        }
    }
}