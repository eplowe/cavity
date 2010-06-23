namespace Cavity.Net
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Net.Sockets;
    using Cavity.Net.Sockets;

    public sealed class Http : IHttp
    {
        public IHttpResponse Send(IHttpRequest request)
        {
            if (null == request)
            {
                throw new ArgumentNullException("request");
            }

            IHttpResponse response = null;
            TcpClient client = null;
            try
            {
                client = request.AbsoluteUri.ToTcpClient();
                response = Http.Send(request, client);
            }
            finally
            {
                if (null != client)
                {
                    client.Close();
                }
            }

            return response;
        }

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "This is an odd rule that seems to be impossible to actually pass.")]
        private static IHttpResponse Send(IHttpRequest request, TcpClient client)
        {
            IHttpResponse response = null;

            using (var stream = client.GetStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    request.Write(writer);
                    writer.Flush();
                    response = Http.Read(stream);
                }
            }

            return response;
        }

        private static IHttpResponse Read(Stream stream)
        {
            HttpResponse response = new HttpResponse();

            using (var reader = new StreamReader(stream))
            {
                response.Read(reader);
            }

            return response;
        }
    }
}