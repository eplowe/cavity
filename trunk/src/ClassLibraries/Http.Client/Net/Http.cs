namespace Cavity.Net
{
    using System;
    using System.IO;
    using System.Net.Sockets;
    using System.Text;
    using Cavity.Net.Mime;
    using Cavity.Net.Sockets;
    using Microsoft.Practices.ServiceLocation;

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
            HttpResponse response = null;

            using (var reader = new StreamReader(stream))
            {
                var status = Http.ReadStatusLine(reader);
                var headers = Http.ReadHeaders(reader);

                response = new HttpResponse(
                    status,
                    headers,
                    ServiceLocator.Current.GetInstance<IMediaType>(headers.ContentType.MediaType).ToBody(reader));
            }

            return response;
        }

        private static StatusLine ReadStatusLine(StreamReader reader)
        {
            return StatusLine.Parse(reader.ReadLine());
        }

        private static HttpHeaderCollection ReadHeaders(StreamReader reader)
        {
            StringBuilder buffer = new StringBuilder();
            while (true)
            {
                string line = reader.ReadLine();
                if (null == line || 0 == line.Length)
                {
                    break;
                }

                buffer.AppendLine(line);
            }

            return HttpHeaderCollection.Parse(buffer.ToString());
        }
    }
}