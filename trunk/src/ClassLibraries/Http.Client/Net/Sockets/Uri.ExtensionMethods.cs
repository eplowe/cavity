namespace Cavity.Net.Sockets
{
    using System;
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

            return new TcpClient(absoluteUri.DnsSafeHost, absoluteUri.Port);
        }
    }
}