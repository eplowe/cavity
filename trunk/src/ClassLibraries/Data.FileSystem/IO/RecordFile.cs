namespace Cavity.IO
{
    using System.Collections.Generic;
    using System.Text;
    using System.Xml.XPath;

    public sealed class RecordFile
    {
        public RecordFile()
        {
            Headers = new Dictionary<string, string>();
        }

        public IXPathNavigable Body { get; set; }

        public IDictionary<string, string> Headers { get; private set; }

        public override string ToString()
        {
            var buffer = new StringBuilder();
            foreach (var header in Headers)
            {
                buffer.AppendLine("{0}: {1}".FormatWith(header.Key, header.Value));
            }

            buffer.AppendLine(string.Empty);
            if (null != Body)
            {
                buffer.Append(Body.CreateNavigator().OuterXml);
            }

            return buffer.ToString();
        }
    }
}