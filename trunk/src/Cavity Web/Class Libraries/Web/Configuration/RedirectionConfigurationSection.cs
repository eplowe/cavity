namespace Cavity.Configuration
{
    using System;
    using System.Configuration;
    using System.Linq;
    using Cavity.Net;

    public class RedirectionConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("absolute", IsRequired = false, IsDefaultCollection = true)]
        public RedirectionConfigurationElementCollection<AbsoluteUri> Absolutes
        {
            get
            {
                return (RedirectionConfigurationElementCollection<AbsoluteUri>)this["absolute"];
            }
        }

        [ConfigurationProperty("host", IsRequired = false, IsDefaultCollection = true)]
        public RedirectionConfigurationElementCollection<Host> Hosts
        {
            get
            {
                return (RedirectionConfigurationElementCollection<Host>)this["host"];
            }
        }

        [ConfigurationProperty("relative", IsRequired = false, IsDefaultCollection = true)]
        public RedirectionConfigurationElementCollection<RelativeUri> Relatives
        {
            get
            {
                return (RedirectionConfigurationElementCollection<RelativeUri>)this["relative"];
            }
        }

        public AbsoluteUri Redirect(Uri uri)
        {
            if (null == uri)
            {
                throw new ArgumentNullException("uri");
            }

            if (!uri.IsAbsoluteUri)
            {
                throw new ArgumentOutOfRangeException("uri");
            }

            return Host(uri) ?? Absolute(uri) ?? Relative(uri);
        }

        private AbsoluteUri Absolute(Uri uri)
        {
            foreach (var absolute in Absolutes)
            {
                if (absolute.From.Equals(uri.AbsoluteUri))
                {
                    return absolute.To;
                }
                
                var from = new Uri(uri, new Uri(uri.AbsolutePath, UriKind.Relative));
                if (!absolute.From.Equals(from))
                {
                    continue;
                }

                return "{0}{1}".FormatWith(absolute.To, uri.Query);
            }

            return null;
        }

        private AbsoluteUri Host(Uri uri)
        {
            foreach (var baseUri in from host in Hosts
                                    where uri.Host.Equals(host.From)
                                    select new Uri("{0}://{1}".FormatWith(uri.Scheme, host.To)))
            {
                return new Uri(baseUri, new Uri(uri.PathAndQuery, UriKind.Relative));
            }

            return null;
        }

        private AbsoluteUri Relative(Uri uri)
        {
            foreach (var relative in Relatives)
            {
                if (relative.From.Equals(uri.PathAndQuery))
                {
                    return new Uri(uri, new Uri(relative.To, UriKind.Relative));
                }

                if (!relative.From.Equals(uri.AbsolutePath))
                {
                    continue;
                }

                return new Uri(uri, new Uri("{0}{1}".FormatWith(relative.To, uri.Query), UriKind.Relative));
            }

            return null;
        }
    }
}