namespace Cavity.Transactions
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
#if !NET20
    using System.Linq;
#endif
    using System.Xml.Serialization;
    using Cavity.Collections;
    using Cavity.Diagnostics;

    [XmlRoot("operation")]
    public class Operation
    {
        public Operation()
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, string.Empty);
            Commands = new CommandCollection();
        }

        public Operation(Guid resourceManager)
            : this()
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, "resourceManager={0}".FormatWith(resourceManager));
            Identity = new EnlistmentIdentity(resourceManager);
        }

        [XmlElement("commands")]
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "XML serialization requires the setter to be public.")]
        public CommandCollection Commands { get; set; }

        [XmlIgnore]
        public EnlistmentIdentity Identity { get; protected set; }

        [XmlAttribute("info")]
        public string Info { get; set; }

        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Do", Justification = "This naming is intentional.")]
        public virtual bool Do()
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, "Identity.ResourceManager={0}, Identity.Instance={1}".FormatWith(Identity.ResourceManager, Identity.Instance));
            if (null == Info)
            {
                throw new InvalidOperationException();
            }

            Trace.WriteIf(Tracing.Is.TraceVerbose, "Commands.Count={0}".FormatWith(Commands.Count));
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

        public virtual void Done(bool success)
        {
            if (null != Info)
            {
                Trace.WriteIf(Tracing.Is.TraceVerbose, "Identity.ResourceManager={0}, Identity.Instance={1}".FormatWith(Identity.ResourceManager, Identity.Instance));
            }

            Recovery.Exclude(this, success);
        }

        public virtual bool Undo()
        {
            Trace.WriteIf(Tracing.Is.TraceVerbose, "Identity.ResourceManager={0}, Identity.Instance={1}".FormatWith(Identity.ResourceManager, Identity.Instance));
            if (null == Info)
            {
                throw new InvalidOperationException();
            }

            Trace.WriteIf(Tracing.Is.TraceVerbose, "Commands.Count={0}".FormatWith(Commands.Count));
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