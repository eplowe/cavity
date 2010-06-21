namespace Cavity.Net
{
    using System;

    public class IHttpHeaderDummy : IHttpHeader
    {
        public Token Name
        {
            get
            {
                throw new NotSupportedException();
            }
        }

        public string Value
        {
            get
            {
                throw new NotSupportedException();
            }
        }
    }
}