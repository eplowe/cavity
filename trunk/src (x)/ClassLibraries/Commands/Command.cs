namespace Cavity
{
    using System;
    using System.Xml;
    using System.Xml.Schema;

    public abstract class Command : ICommand
    {
        protected Command()
        {
        }

        protected Command(bool unidirectional)
        {
            Unidirectional = unidirectional;
        }

        public bool Undo { get; set; }

        public bool Unidirectional { get; set; }

        public abstract bool Act();

        public abstract bool Revert();

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

            var attribute = reader.GetAttribute("undo");
            if (!string.IsNullOrEmpty(attribute))
            {
                Undo = XmlConvert.ToBoolean(attribute);
            }

            attribute = reader.GetAttribute("unidirectional");
            if (!string.IsNullOrEmpty(attribute))
            {
                Unidirectional = XmlConvert.ToBoolean(attribute);
            }
        }

        public virtual void WriteXml(XmlWriter writer)
        {
            if (null == writer)
            {
                throw new ArgumentNullException("writer");
            }

            writer.WriteAttributeString("undo", XmlConvert.ToString(Undo));
            writer.WriteAttributeString("unidirectional", XmlConvert.ToString(Unidirectional));
        }
    }
}