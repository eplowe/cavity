namespace Cavity.Net
{
    using System.Xml.Serialization;

    [XmlRoot("httpClient")]
    public sealed class HttpClientSettings
    {
        [XmlAttribute("keepAlive")]
        public bool KeepAlive
        {
            get;
            set;
        }
    }
}