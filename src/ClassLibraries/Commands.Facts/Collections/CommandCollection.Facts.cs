namespace Cavity.Collections
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Xml.Serialization;
    using Cavity.IO;
    using Cavity.Xml.XPath;
    using Xunit;

    public sealed class CommandCollectionFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<CommandCollection>()
                            .DerivesFrom<Collection<ICommand>>()
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
            Assert.NotNull(new CommandCollection());
        }

        [Fact]
        public void deserialize()
        {
            using (var temp = new TempDirectory())
            {
                var example = temp.Info.ToDirectory("example");
                var obj = ("<commands>" +
                           "<command i='1' type='Cavity.IO.DirectoryCreateCommand, Cavity.Commands, Version=1.0.0.0, Culture=neutral, PublicKeyToken=c0c289e4846931e8'>" +
                           @"<directory.create path='{0}' undo='false' />".FormatWith(temp.Info.FullName) +
                           "</command>" +
                           "<command i='2' type='Cavity.IO.DirectoryCreateCommand, Cavity.Commands, Version=1.0.0.0, Culture=neutral, PublicKeyToken=c0c289e4846931e8'>" +
                           @"<directory.create path='{0}' undo='true' />".FormatWith(example.FullName) +
                           "</command>" +
                           "</commands>").XmlDeserialize<CommandCollection>();

                Assert.Equal(2, obj.Count);
                Assert.Equal(temp.Info.FullName, ((DirectoryCreateCommand)obj.First()).Path);
                Assert.Equal(example.FullName, ((DirectoryCreateCommand)obj.Last()).Path);
            }
        }

        [Fact]
        public void deserializeEmpty()
        {
            Assert.NotNull("<commands />".XmlDeserialize<CommandCollection>());
        }

        [Fact]
        public void op_GetSchema()
        {
            Assert.Throws<NotSupportedException>(() => (new CommandCollection() as IXmlSerializable).GetSchema());
        }

        [Fact]
        public void op_ReadXml_XmlReaderNull()
        {
            Assert.Throws<ArgumentNullException>(() => (new CommandCollection() as IXmlSerializable).ReadXml(null));
        }

        [Fact]
        public void op_WriteXml_XmlWriterNull()
        {
            Assert.Throws<ArgumentNullException>(() => (new CommandCollection() as IXmlSerializable).WriteXml(null));
        }

        [Fact]
        public void serialize()
        {
            using (var temp = new TempDirectory())
            {
                var obj = new CommandCollection
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
            var obj = new CommandCollection();

            var navigator = obj.XmlSerialize().CreateNavigator();

            Assert.True(navigator.Evaluate<bool>("1 = count(/commands)"));
        }
    }
}