namespace Cavity.Transactions
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Xml.Serialization;
    using Cavity.Collections;
    using Cavity.Diagnostics;

    [XmlRoot("operation")]
    public sealed class Operation
    {
        public Operation()
        {
            Trace.WriteIf(Tracing.Enabled, string.Empty);
            Commands = new CommandCollection();
        }

        public Operation(Guid resourceManager)
            : this()
        {
            Trace.WriteIf(Tracing.Enabled, "resourceManager={0}".FormatWith(resourceManager));
            Identity = new EnlistmentIdentity(resourceManager);
        }

        [XmlElement("commands")]
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "XML serialization requires the setter to be public.")]
        public CommandCollection Commands { get; set; }

        [XmlIgnore]
        public EnlistmentIdentity Identity { get; private set; }

        [XmlAttribute("info")]
        public string Info { get; set; }

        public bool Do()
        {
            Trace.WriteIf(Tracing.Enabled, "Identity.ResourceManager={0}, Identity.Instance={1}".FormatWith(Identity.ResourceManager, Identity.Instance));
            if (null == Info)
            {
                throw new InvalidOperationException();
            }

            Trace.WriteIf(Tracing.Enabled, "Commands.Count={0}".FormatWith(Commands.Count));
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
                Trace.WriteIf(Tracing.Enabled, "Identity.ResourceManager={0}, Identity.Instance={1}".FormatWith(Identity.ResourceManager, Identity.Instance));
            }

            Recovery.Exclude(this, success);
        }

        public bool Undo()
        {
            Trace.WriteIf(Tracing.Enabled, "Identity.ResourceManager={0}, Identity.Instance={1}".FormatWith(Identity.ResourceManager, Identity.Instance));
            if (null == Info)
            {
                throw new InvalidOperationException();
            }

            Trace.WriteIf(Tracing.Enabled, "Commands.Count={0}".FormatWith(Commands.Count));
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