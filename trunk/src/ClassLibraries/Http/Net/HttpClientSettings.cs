namespace Cavity.Net
{
    using System;
    using System.Xml.Serialization;
    using Microsoft.Practices.ServiceLocation;

    public sealed class HttpClientSettings
    {
        private IUserAgent _userAgent;

        public HttpClientSettings()
        {
            this.UserAgent = ServiceLocator.Current.GetInstance<IUserAgent>();
        }

        public bool KeepAlive
        {
            get;
            set;
        }

        public IUserAgent UserAgent
        {
            get
            {
                return this._userAgent;
            }

            set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                this._userAgent = value;
            }
        }
    }
}