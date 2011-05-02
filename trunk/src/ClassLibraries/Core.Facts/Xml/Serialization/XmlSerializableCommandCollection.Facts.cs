namespace Cavity.Xml.Serialization
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Xml.Serialization;
    using Cavity.Commands;
    using Cavity.IO;
    using Cavity.Xml.XPath;
    using Xunit;

    public sealed class XmlSerializableCommandCollectionFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<XmlSerializableCommandCollection>()
                            .DerivesFrom<Collection<IXmlSerializableCommand>>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .XmlRoot("commands")
                            .Implements<IXmlSerializable>()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new XmlSerializableCommandCollection());
        }

        [Fact]
        public void deserialize()
        {
            using (var temp = new TempDirectory())
            {
                var obj = ("<commands>" +
                           "<command i='1' type='Cavity.Commands.DirectoryCreateCommand, Cavity.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=c0c289e4846931e8'>" +
                           @"<directory.create path='{0}' undo='false' />".FormatWith(temp.Info.FullName) +
                           "</command>" +
                           "<command i='2' type='Cavity.Commands.DirectoryCreateCommand, Cavity.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=c0c289e4846931e8'>" +
                           @"<directory.create path='{0}' undo='true' />".FormatWith(Path.Combine(temp.Info.FullName, "example")) +
                           "</command>" +
                           "</commands>").XmlDeserialize<XmlSerializableCommandCollection>();

                Assert.Equal(2, obj.Count);
                Assert.Equal(temp.Info.FullName, ((DirectoryCreateCommand)obj.First()).Path);
                Assert.Equal(Path.Combine(temp.Info.FullName, "example"), ((DirectoryCreateCommand)obj.Last()).Path);
            }
        }

        [Fact]
        public void deserializeEmpty()
        {
            Assert.NotNull("<commands />".XmlDeserialize<XmlSerializableCommandCollection>());
        }

        [Fact]
        public void op_GetSchema()
        {
            Assert.Throws<NotSupportedException>(() => (new XmlSerializableCommandCollection() as IXmlSerializable).GetSchema());
        }

        [Fact]
        public void op_ReadXml_XmlReaderNull()
        {
            Assert.Throws<ArgumentNullException>(() => (new XmlSerializableCommandCollection() as IXmlSerializable).ReadXml(null));
        }

        [Fact]
        public void op_WriteXml_XmlWriterNull()
        {
            Assert.Throws<ArgumentNullException>(() => (new XmlSerializableCommandCollection() as IXmlSerializable).WriteXml(null));
        }

        [Fact]
        public void serialize()
        {
            using (var temp = new TempDirectory())
            {
                var obj = new XmlSerializableCommandCollection
                {
                    new DirectoryCreateCommand(temp.Info.FullName)
                };

                var navigator = obj.XmlSerialize().CreateNavigator();

                Assert.True(navigator.Evaluate<bool>("1 = count(/commands/command)"));
                var xpath = "1 = count(/commands/command[@type='{0}']/directory.create[@path='{1}'][@undo='false'])".FormatWith(typeof(DirectoryCreateCommand).AssemblyQualifiedName, temp.Info.FullName);
                Assert.True(navigator.Evaluate<bool>(xpath));
            }
        }

        [Fact]
        public void serialize_whenEmpty()
        {
            var obj = new XmlSerializableCommandCollection();

            var navigator = obj.XmlSerialize().CreateNavigator();

            Assert.True(navigator.Evaluate<bool>("1 = count(/commands)"));
        }
    }
}