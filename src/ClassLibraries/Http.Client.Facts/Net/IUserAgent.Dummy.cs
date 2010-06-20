namespace Cavity.Net
{
    using System;

    public class IUserAgentDummy : IUserAgent
    {
        public string Value
        {
            get
            {
                throw new NotSupportedException();
            }
        }
    }
}