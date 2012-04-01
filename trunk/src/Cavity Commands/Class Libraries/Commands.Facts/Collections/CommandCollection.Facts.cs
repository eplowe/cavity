namespace Cavity.Collections
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

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
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .XmlRoot("commands")
                            .XmlSerializable()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new CommandCollection());
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
        public void op_Equals_object()
        {
            var obj = new CommandCollection
                          {
                              new DerivedCommand()
                          };

            var comparand = new CommandCollection
                                {
                                    new DerivedCommand()
                                };

            Assert.True(obj.Equals(comparand));
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(new CommandCollection().Equals(null));
        }

        [Fact]
        public void op_Equals_objectSame()
        {
            var obj = new CommandCollection();

            // ReSharper disable EqualExpressionComparison
            Assert.True(obj.Equals(obj));

            // ReSharper restore EqualExpressionComparison
        }

        [Fact]
        public void op_Equals_object_whenFalse()
        {
            var obj = new CommandCollection
                          {
                              new Mock<ICommand>().Object
                          };

            var comparand = new CommandCollection
                                {
                                    new DerivedCommand()
                                };

            Assert.False(obj.Equals(comparand));
        }

        [Fact]
        public void op_GetHashCode()
        {
            var expected = string.Empty.GetHashCode();
            var actual = new CommandCollection().GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString()
        {
            var expected = string.Empty;
            var actual = new CommandCollection().ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString_whenSingleItem()
        {
            var expected = "Cavity.DerivedCommand" + Environment.NewLine;

            var obj = new CommandCollection
                          {
                              new DerivedCommand()
                          };
            var actual = obj.ToString();

            Assert.Equal(expected, actual);
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
        public void xml_deserialize()
        {
            var obj = ("<commands>" +
                       "<command i='1' type='{0}'>".FormatWith(typeof(DerivedCommand).AssemblyQualifiedName) +
                       @"<command.derived undo='false' />" +
                       "</command>" +
                       "<command i='1' type='{0}'>".FormatWith(typeof(DerivedCommand).AssemblyQualifiedName) +
                       @"<command.derived undo='true' />" +
                       "</command>" +
                       "</commands>").XmlDeserialize<CommandCollection>();

            Assert.Equal(2, obj.Count);
            Assert.IsType<DerivedCommand>(obj.First());
            Assert.IsType<DerivedCommand>(obj.Last());
        }

        [Fact]
        public void xml_deserialize_whenEmptyType()
        {
            using (var temp = new TempDirectory())
            {
                // ReSharper disable AccessToDisposedClosure
                Assert.Throws<InvalidOperationException>(() => ("<commands>" +
                                                                "<command i='1' type=''>" +
                                                                @"<directory.create path='{0}' undo='false' />".FormatWith(temp.Info.FullName) +
                                                                "</command>" +
                                                                "</commands>").XmlDeserialize<CommandCollection>());

                // ReSharper restore AccessToDisposedClosure
            }
        }

        [Fact]
        public void xml_serialize()
        {
            using (var temp = new TempDirectory())
            {
                var obj = new CommandCollection
                              {
                                  new DerivedCommand()
                              };

                var navigator = obj.XmlSerialize().CreateNavigator();

                Assert.True(navigator.Evaluate<bool>("1 = count(/commands/command)"));
                var xpath = "1 = count(/commands/command[@type='{0}']/command.derived[@undo='false'])".FormatWith(typeof(DerivedCommand).AssemblyQualifiedName, temp.Info.FullName);
                Assert.True(navigator.Evaluate<bool>(xpath));
            }
        }
    }
}