namespace Cavity.Collections
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
#if !NET20
    using System.Linq;
#endif
    using System.Text;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;

    using Cavity.Diagnostics;

    [XmlRoot("commands")]
    public class CommandCollection : Collection<ICommand>, 
                                     IXmlSerializable
    {
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Do", Justification = "This naming is intentional.")]
        public virtual bool Do()
        {
#if NET20
            Trace.WriteIf(Tracing.Is.TraceVerbose, StringExtensionMethods.FormatWith("Count={0}", Count));
#else
            Trace.WriteIf(Tracing.Is.TraceVerbose, "Count={0}".FormatWith(Count));
#endif
            if (0 == Count)
            {
                return true;
            }

            foreach (var command in this)
            {
                CommandCounter.Increment();
                if (!command.Act())
                {
                    return false;
                }
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var cast = obj as CommandCollection;

            return !ReferenceEquals(null, cast)
                   && string.Equals(ToString(), cast.ToString(), StringComparison.Ordinal);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            var buffer = new StringBuilder();
            foreach (var item in this)
            {
                buffer.AppendLine(item.ToString());
            }

            return buffer.ToString();
        }

        public virtual bool Undo()
        {
#if NET20
            Trace.WriteIf(Tracing.Is.TraceVerbose, StringExtensionMethods.FormatWith("Count={0}", Count));
#else
            Trace.WriteIf(Tracing.Is.TraceVerbose, "Count={0}".FormatWith(Count));
#endif
            if (0 == Count)
            {
                return true;
            }

#if NET20
            for (var i = 0; i < Count; i++)
            {
                var command = this[Count - 1 - i];
                CommandCounter.Increment();
                if (!command.Revert())
                {
                    return false;
                }
            }
#else
            foreach (var command in this.Reverse())
            {
                CommandCounter.Increment();
                if (!command.Revert())
                {
                    return false;
                }
            }

#endif

            return true;
        }

        public virtual XmlSchema GetSchema()
        {
            throw new NotSupportedException();
        }

        public virtual void ReadXml(XmlReader reader)
        {
            if (null == reader)
            {
                throw new ArgumentNullException("reader");
            }

            if (reader.IsEmptyElement)
            {
                reader.Read();
                return;
            }

            var name = reader.Name;
            while (reader.Read())
            {
                if (XmlNodeType.EndElement == reader.NodeType &&
                    reader.Name == name)
                {
                    reader.Read();
                    break;
                }

                while ("command".Equals(reader.Name, StringComparison.OrdinalIgnoreCase))
                {
                    var attribute = reader.GetAttribute("type");
                    if (string.IsNullOrEmpty(attribute))
                    {
                        throw new InvalidOperationException();
                    }

#if NET20
                    Add((ICommand)StringExtensionMethods.XmlDeserialize(reader.ReadInnerXml(), Type.GetType(attribute)));
#else
                    Add((ICommand)reader.ReadInnerXml().XmlDeserialize(Type.GetType(attribute)));
#endif
                }
            }
        }

        public virtual void WriteXml(XmlWriter writer)
        {
            if (null == writer)
            {
                throw new ArgumentNullException("writer");
            }

            foreach (var item in Items)
            {
                writer.WriteStartElement("command");
                writer.WriteAttributeString("type", item.GetType().AssemblyQualifiedName);
#if NET20
                writer.WriteRaw(ObjectExtensionMethods.XmlSerialize(item).CreateNavigator().OuterXml);
#else
                writer.WriteRaw(item.XmlSerialize().CreateNavigator().OuterXml);
#endif
                writer.WriteEndElement();
            }
        }
    }
}