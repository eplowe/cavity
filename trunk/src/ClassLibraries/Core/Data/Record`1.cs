namespace Cavity.Data
{
    using System;
    using System.Xml.Serialization;

    public sealed class Record<T> : ValueObject<Record<T>>, IRecord<T>
    {
        public Record()
        {
            RegisterProperty(x => x.Cacheability);
            RegisterProperty(x => x.Created);
            RegisterProperty(x => x.Etag);
            RegisterProperty(x => x.Expiration);
            RegisterProperty(x => x.Key);
            RegisterProperty(x => x.Modified);
            RegisterProperty(x => x.Status);
            RegisterProperty(x => x.Urn);

            RegisterProperty(x => x.Entity);
        }

        [XmlIgnore]
        public string Cacheability { get; set; }

        [XmlIgnore]
        public DateTime? Created { get; set; }

        public string Entity { get; set; }

        [XmlIgnore]
        public string Etag { get; set; }

        [XmlIgnore]
        public string Expiration { get; set; }

        [XmlIgnore]
        public AlphaDecimal? Key { get; set; }

        [XmlIgnore]
        public DateTime? Modified { get; set; }

        [XmlIgnore]
        public int? Status { get; set; }

        [XmlIgnore]
        public AbsoluteUri Urn { get; set; }

        [XmlIgnore]
        public T Value { get; set; }
    }
}