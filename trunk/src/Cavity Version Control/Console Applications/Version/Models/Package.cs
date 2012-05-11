namespace Cavity.Models
{
    using System.Text;

    public sealed class Package
    {
        public Package()
        {
        }

        public Package(string id)
        {
            Id = id;
        }

        public Package(string id, 
                       string version)
        {
            Id = id;
            Version = version;
        }

        public string Id { get; set; }

        public string Version { get; set; }

        public override string ToString()
        {
            var buffer = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(Id))
            {
                buffer.Append(Id);
            }

            if (!string.IsNullOrWhiteSpace(Version))
            {
                if (0 != buffer.Length)
                {
                    buffer.Append('.');
                }

                buffer.Append(Version);
            }

            return buffer.ToString();
        }
    }
}