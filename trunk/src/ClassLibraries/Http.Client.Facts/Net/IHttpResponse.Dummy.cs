namespace Cavity.Net
{
    using System;

    public class IHttpResponseDummy : IHttpMessageDummy, IHttpResponse
    {
        public StatusLine StatusLine
        {
            get
            {
                throw new NotSupportedException();
            }

            set
            {
                throw new NotSupportedException();
            }
        }
    }
}