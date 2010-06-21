namespace Cavity.Net
{
    using System;

    public class IHttpMessageDummy : IHttpMessage
    {
        public IHttpBody Body
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

        public IHttpHeaderCollection Headers
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