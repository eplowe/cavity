namespace Cavity.Transactions
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Xml.Serialization;
    using Cavity.Diagnostics;
    using Cavity.Xml.Serialization;

    [XmlRoot("operation")]
    public sealed class Operation
    {
        public Operation()
        {
            Commands = new XmlSerializableCommandCollection();
        }

        public Operation(Guid resourceManager)
            : this()
        {
            Trace.WriteIf(Tracing.Enabled, string.Empty);
            Identity = new EnlistmentIdentity(resourceManager);
        }

        [XmlElement("commands")]
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "XML serialization requires the setter to be public.")]
        public XmlSerializableCommandCollection Commands { get; set; }

        [XmlIgnore]
        public EnlistmentIdentity Identity { get; private set; }

        [XmlAttribute("info")]
        public string Info { get; set; }

        public bool Do()
        {
            Trace.WriteIf(Tracing.Enabled, string.Empty);
            if (null == Info)
            {
                throw new InvalidOperationException();
            }

            if (0 == Commands.Count)
            {
                return true;
            }

            foreach (var command in Commands)
            {
                Recovery.Include(this);
                if (!command.Act())
                {
                    return false;
                }
            }

            return true;
        }

        public void Done(bool success)
        {
            if (null != Info)
            {
                Trace.WriteIf(Tracing.Enabled, string.Empty);
            }

            Recovery.Exclude(this, success);
        }

        public bool Undo()
        {
            Trace.WriteIf(Tracing.Enabled, string.Empty);
            if (null == Info)
            {
                throw new InvalidOperationException();
            }

            if (0 == Commands.Count)
            {
                return true;
            }

            foreach (var command in Commands.Reverse())
            {
                Recovery.Include(this);
                if (!command.Revert())
                {
                    return false;
                }
            }

            return true;
        }
    }
}