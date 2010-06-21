namespace Cavity.Net
{
    using System;
    using System.IO;

    public class IHttpBodyDummy : IHttpBody
    {
        public IHttpBody Read(StreamReader reader)
        {
            throw new NotSupportedException();
        }
    }
}