namespace Cavity.Net
{
    using System;

    public sealed class IRequestMethodDummy : IRequestMethod
    {
        IResponseStatus IRequestMethod.Delete()
        {
            throw new NotSupportedException();
        }

        IResponseStatus IRequestMethod.Get()
        {
            throw new NotSupportedException();
        }

        IResponseStatus IRequestMethod.Get(bool head)
        {
            throw new NotSupportedException();
        }

        IResponseStatus IRequestMethod.Head()
        {
            throw new NotSupportedException();
        }

        IResponseStatus IRequestMethod.Options()
        {
            throw new NotSupportedException();
        }

        IResponseStatus IRequestMethod.Post(IHttpContent content)
        {
            throw new NotSupportedException();
        }

        IResponseStatus IRequestMethod.Put(IHttpContent content)
        {
            throw new NotSupportedException();
        }

        IResponseStatus IRequestMethod.Use(string method)
        {
            throw new NotSupportedException();
        }

        IResponseStatus IRequestMethod.Use(string method, bool head)
        {
            throw new NotSupportedException();
        }

        IResponseStatus IRequestMethod.Use(string method, IHttpContent content)
        {
            throw new NotSupportedException();
        }
    }
}