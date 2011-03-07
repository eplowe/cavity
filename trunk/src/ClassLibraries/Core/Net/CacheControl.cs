namespace Cavity.Net
{
    public sealed class CacheControl : HttpHeader
    {
        private static readonly CacheControl _nocache = new CacheControl("no-cache");
        private static readonly CacheControl _nostore = new CacheControl("no-store");
        private static readonly CacheControl _private = new CacheControl("private");
        private static readonly CacheControl _public = new CacheControl("public");

        public CacheControl()
            : this(string.Empty)
        {
        }

        public CacheControl(string value)
            : base("Cache-Control", value)
        {
        }

        public static CacheControl NoCache
        {
            get
            {
                return _nocache;
            }
        }

        public static CacheControl NoStore
        {
            get
            {
                return _nostore;
            }
        }

        public static CacheControl Private
        {
            get
            {
                return _private;
            }
        }

        public static CacheControl Public
        {
            get
            {
                return _public;
            }
        }
    }
}