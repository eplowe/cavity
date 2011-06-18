﻿namespace Cavity.Collections
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Xml.Serialization;
    using Cavity.IO;
    using Cavity.Xml.XPath;
    using Moq;
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
                           "<command i='1' type='{0}'>".FormatWith(typeof(DirectoryCreateCommand).AssemblyQualifiedName) +
                           @"<directory.create path='{0}' undo='false' />".FormatWith(temp.Info.FullName) +
                           "</command>" +
                           "<command i='1' type='{0}'>".FormatWith(typeof(DirectoryCreateCommand).AssemblyQualifiedName) +
                           @"<directory.create path='{0}' undo='true' />".FormatWith(example.FullName) +
                           "</command>" +
                           "</commands>").XmlDeserialize<CommandCollection>();

                Assert.Equal(2, obj.Count);
                Assert.Equal(temp.Info.FullName, ((DirectoryCreateCommand)obj.First()).Path);
                Assert.Equal(example.FullName, ((DirectoryCreateCommand)obj.Last()).Path);
            }
        }

        [Fact]
        public void deserialize_whenEmpty()
        {
            Assert.NotNull("<commands />".XmlDeserialize<CommandCollection>());
        }

        [Fact]
        public void deserialize_whenEmptyType()
        {
            using (var temp = new TempDirectory())
            {
                Assert.Throws<InvalidOperationException>(() => ("<commands>" +
                                                                "<command i='1' type=''>" +
                                                                @"<directory.create path='{0}' undo='false' />".FormatWith(temp.Info.FullName) +
                                                                "</command>" +
                                                                "</commands>").XmlDeserialize<CommandCollection>());
            }
        }

        [Fact]
        public void deserialize_whenOpenClose()
        {
            Assert.NotNull("<commands></commands>".XmlDeserialize<CommandCollection>());
        }

        [Fact]
        public void op_Do()
        {
            Assert.True(new CommandCollection().Do());
        }

        [Fact]
        public void op_Do_whenFalse()
        {
            var command = new Mock<ICommand>();
            command
                .Setup(x => x.Act())
                .Returns(false);
            var obj = new CommandCollection
            {
                command.Object
            };

            Assert.False(obj.Do());
        }

        [Fact]
        public void op_Do_whenTrue()
        {
            var command = new Mock<ICommand>();
            command
                .Setup(x => x.Act())
                .Returns(true);
            var obj = new CommandCollection
            {
                command.Object
            };

            Assert.True(obj.Do());
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
        public void op_Undo()
        {
            Assert.True(new CommandCollection().Undo());
        }

        [Fact]
        public void op_Undo_whenFalse()
        {
            var command = new Mock<ICommand>();
            command
                .Setup(x => x.Revert())
                .Returns(false);
            var obj = new CommandCollection
            {
                command.Object
            };

            Assert.False(obj.Undo());
        }

        [Fact]
        public void op_Undo_whenTrue()
        {
            var command = new Mock<ICommand>();
            command
                .Setup(x => x.Revert())
                .Returns(true);
            var obj = new CommandCollection
            {
                command.Object
            };

            Assert.True(obj.Undo());
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