namespace Cavity.Tests
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Xml.Serialization;
    using Cavity.Fluent;
    using Cavity.Properties;

    public sealed class XmlSerializableTest : ITestExpectation
    {
        public XmlSerializableTest(Type type,
                                   bool verifyDeserialization)
        {
            Type = type;
            VerifyDeserialization = verifyDeserialization;
        }

        private Type Type { get; set; }

        private bool VerifyDeserialization { get; set; }

        public bool Check()
        {
            var instance = Activator.CreateInstance(Type) as IXmlSerializable;
            if (null == instance)
            {
                throw new UnitTestException(string.Format(CultureInfo.InvariantCulture, Resources.ImplementationTestException_NoneMessage, Type.Name, "IXmlSerializable"));
            }

            return CheckExceptions(instance)
                   && CheckDeserialization("<{0} />")
                   && CheckDeserialization("<{0}></{0}>");
        }

        private bool CheckDeserialization(string format)
        {
            if (!VerifyDeserialization)
            {
                return true;
            }

            var attribute = Attribute.GetCustomAttribute(Type, typeof(XmlRootAttribute)) as XmlRootAttribute;

            return CheckDeserialization(format, null == attribute ? Type.Name : attribute.ElementName);
        }

        private bool CheckDeserialization(string format,
                                          string name)
        {
            var xml = string.Format(CultureInfo.InvariantCulture, format, name);
            using (var stream = new MemoryStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(xml);
                    writer.Flush();
                    stream.Position = 0;

                    if (Activator.CreateInstance(Type).Equals(new XmlSerializer(Type).Deserialize(stream)))
                    {
                        return true;
                    }

                    throw new UnitTestException(string.Format(CultureInfo.InvariantCulture, Resources.XmlSerialization_EqualsNew, xml, Type.Name));
                }
            }
        }

        private bool CheckExceptions(IXmlSerializable instance)
        {
            try
            {
                // ReSharper disable ReturnValueOfPureMethodIsNotUsed
                instance.GetSchema();

                // ReSharper restore ReturnValueOfPureMethodIsNotUsed
                throw new UnitTestException(string.Format(CultureInfo.InvariantCulture, Resources.XmlSerialization_GetSchema, Type.Name));
            }
            catch (NotSupportedException)
            {
            }

            try
            {
                // ReSharper disable ReturnValueOfPureMethodIsNotUsed
                instance.ReadXml(null);

                // ReSharper restore ReturnValueOfPureMethodIsNotUsed
                throw new UnitTestException(string.Format(CultureInfo.InvariantCulture, Resources.XmlSerialization_ReadXmlNull, Type.Name));
            }
            catch (ArgumentNullException)
            {
            }

            try
            {
                // ReSharper disable ReturnValueOfPureMethodIsNotUsed
                instance.WriteXml(null);

                // ReSharper restore ReturnValueOfPureMethodIsNotUsed
                throw new UnitTestException(string.Format(CultureInfo.InvariantCulture, Resources.XmlSerialization_WriteXmlNull, Type.Name));
            }
            catch (ArgumentNullException)
            {
            }

            return true;
        }
    }
}