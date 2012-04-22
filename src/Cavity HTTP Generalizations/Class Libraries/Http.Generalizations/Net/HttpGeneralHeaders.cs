namespace Cavity.Net
{
    public static class HttpGeneralHeaders
    {
        public static string CacheControl
        {
            get
            {
                return "Cache-Control";
            }
        }

        public static string Connection
        {
            get
            {
                return "Connection";
            }
        }

        public static string Date
        {
            get
            {
                return "Date";
            }
        }

        public static string Pragma
        {
            get
            {
                return "Pragma";
            }
        }

        public static string Trailer
        {
            get
            {
                return "Trailer";
            }
        }

        public static string TransferEncoding
        {
            get
            {
                return "Transfer-Encoding";
            }
        }

        public static string Upgrade
        {
            get
            {
                return "Upgrade";
            }
        }

        public static string Via
        {
            get
            {
                return "Via";
            }
        }

        public static string Warning
        {
            get
            {
                return "Warning";
            }
        }
    }
}