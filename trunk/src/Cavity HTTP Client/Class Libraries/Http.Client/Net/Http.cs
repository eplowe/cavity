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

            IHttpResponse result;

            TcpClient client = null;
            try
            {
                client = request.AbsoluteUri.ToTcpClient();
                client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                client.NoDelay = true;

                result = Send(request, client);
            }
            finally
            {
                if (null != client)
                {
                    client.Close();
                }
            }

            return result;
        }

        private static IHttpResponse Read(Stream stream)
        {
            var result = new HttpResponse();

            using (var reader = new StreamReader(stream))
            {
                result.Read(reader);
            }

            return result;
        }

        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "This is an odd rule that seems to be impossible to actually pass.")]
        private static IHttpResponse Send(IHttpRequest request, 
                                          TcpClient client)
        {
            IHttpResponse result;

            using (var stream = client.GetStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    request.Write(writer);
                    writer.Flush();
                    result = Read(stream);
                }
            }

            return result;
        }
    }
}