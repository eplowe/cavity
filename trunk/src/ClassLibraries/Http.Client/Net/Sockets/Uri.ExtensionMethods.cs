namespace Cavity.Net.Sockets
{
    using System;
    using System.Net;
    using System.Net.Sockets;

    public static class UriExtensionMethods
    {
        public static TcpClient ToTcpClient(this Uri absoluteUri)
        {
            if (null == absoluteUri)
            {
                throw new ArgumentNullException("absoluteUri");
            }
            else if (!absoluteUri.IsAbsoluteUri)
            {
                throw new ArgumentOutOfRangeException("absoluteUri");
            }

            Uri proxy = null;
            using (var web = new WebClient())
            {
                proxy = web.Proxy.GetProxy(absoluteUri);
            }

            return new TcpClient(proxy.DnsSafeHost, proxy.Port);
        }
    }
}