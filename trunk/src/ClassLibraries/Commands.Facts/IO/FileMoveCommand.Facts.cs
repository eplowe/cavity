namespace Cavity.IO
{
    using System;
    using System.IO;
    using Cavity.Xml.XPath;
    using Xunit;

    public sealed class FileMoveCommandFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<FileMoveCommand>()
                            .DerivesFrom<Command>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .XmlRoot("file.move")
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new FileMoveCommand());
        }

        [Fact]
        public void ctor_FileInfo_FileInfo()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToFile("from.txt");
                var destination = temp.Info.ToFile("to.txt");

                Assert.NotNull(new FileMoveCommand(source, destination));
            }
        }

        [Fact]
        public void ctor_FileInfo_FileInfo_bool()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToFile("from.txt");
                var destination = temp.Info.ToFile("to.txt");

                Assert.NotNull(new FileMoveCommand(source, destination, true));
            }
        }

        [Fact]
        public void ctor_bool()
        {
            Assert.NotNull(new FileMoveCommand(true));
        }

        [Fact]
        public void ctor_string_string()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToFile("from.txt").FullName;
                var destination = temp.Info.ToFile("to.txt").FullName;

                Assert.NotNull(new FileMoveCommand(source, destination));
            }
        }

        [Fact]
        public void ctor_string_string_bool()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToFile("from.txt").FullName;
                var destination = temp.Info.ToFile("to.txt").FullName;

                Assert.NotNull(new FileMoveCommand(source, destination, true));
            }
        }

        [Fact]
        public void deserialize()
        {
            var obj = @"<file.move source='C:\from.txt' destination='C:\to.txt' undo='true' unidirectional='true' />".XmlDeserialize<FileMoveCommand>();

            Assert.Equal(@"C:\from.txt", obj.Source);
            Assert.Equal(@"C:\to.txt", obj.Destination);
            Assert.True(obj.Undo);
            Assert.True(obj.Unidirectional);
        }

        [Fact]
        public void deserializeEmpty()
        {
            var obj = "<file.move />".XmlDeserialize<FileMoveCommand>();

            Assert.Null(obj.Source);
            Assert.Null(obj.Destination);
            Assert.False(obj.Undo);
            Assert.False(obj.Unidirectional);
        }

        [Fact]
        public void op_Act()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToFile("from.txt");
                source.CreateNew("example");
                var destination = temp.Info.ToFile("to.txt");

                var obj = new FileMoveCommand(source, destination);

                Assert.True(obj.Act());
                Assert.True(obj.Undo);

                source.Refresh();
                Assert.False(source.Exists);
                Assert.Equal("example", destination.ReadToEnd());
            }
        }

        [Fact]
        public void op_Act_whenDestinationAlreadyExists()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToFile("from.txt");
                source.CreateNew();
                var destination = temp.Info.ToFile("to.txt");
                destination.CreateNew();

                var obj = new FileMoveCommand(source, destination);

                Assert.Throws<IOException>(() => obj.Act());
            }
        }

        [Fact]
        public void op_Act_whenSourceMissing()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToFile("from.txt");
                var destination = temp.Info.ToFile("to.txt");

                var obj = new FileMoveCommand(source, destination);

                Assert.Throws<FileNotFoundException>(() => obj.Act());
            }
        }

        [Fact]
        public void op_GetSchema()
        {
            Assert.Throws<NotSupportedException>(() => new FileMoveCommand().GetSchema());
        }

        [Fact]
        public void op_ReadXml_XmlReaderNull()
        {
            Assert.Throws<ArgumentNullException>(() => new FileMoveCommand().ReadXml(null));
        }

        [Fact]
        public void op_Revert()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToFile("from.txt");
                source.CreateNew("example");
                var destination = temp.Info.ToFile("to.txt");

                var obj = new FileMoveCommand(source, destination);

                Assert.True(obj.Act());
                source.Refresh();
                Assert.False(source.Exists);
                Assert.True(destination.Exists);

                Assert.True(obj.Revert());
                source.Refresh();
                Assert.True(source.Exists);
                destination.Refresh();
                Assert.False(destination.Exists);
            }
        }

        [Fact]
        public void op_Revert_whenNotUndo()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToFile("from.txt");
                source.CreateNew("example");
                var destination = temp.Info.ToFile("to.txt");

                var obj = new FileMoveCommand(source, destination);

                Assert.True(obj.Act());
                obj.Undo = false;

                Assert.True(obj.Revert());
                source.Refresh();
                Assert.False(source.Exists);
                Assert.Equal("example", destination.ReadToEnd());
            }
        }

        [Fact]
        public void op_Revert_whenUnidirectional()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToFile("from.txt");
                source.CreateNew("example");
                var destination = temp.Info.ToFile("to.txt");

                var obj = new FileMoveCommand(source, destination, true);

                Assert.True(obj.Act());
                Assert.False(obj.Undo);

                Assert.True(obj.Revert());
                Assert.Equal("example", destination.ReadToEnd());
            }
        }

        [Fact]
        public void op_WriteXml_XmlWriterNull()
        {
            Assert.Throws<ArgumentNullException>(() => new FileMoveCommand().WriteXml(null));
        }

        [Fact]
        public void prop_Destination()
        {
            Assert.True(new PropertyExpectations<FileMoveCommand>(p => p.Destination)
                            .IsAutoProperty<string>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Source()
        {
            Assert.True(new PropertyExpectations<FileMoveCommand>(p => p.Source)
                            .IsAutoProperty<string>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void serialize()
        {
            var obj = new FileMoveCommand
            {
                Source = @"C:\from.txt",
                Destination = @"C:\to.txt",
                Undo = true
            };

            var navigator = obj.XmlSerialize().CreateNavigator();

            if (null == navigator.NameTable)
            {
                Assert.NotNull(navigator.NameTable);
            }
            else
            {
                Assert.True(navigator.Evaluate<bool>(@"1 = count(/file.move[@source='C:\from.txt'][@destination='C:\to.txt'][@undo='true'][@unidirectional='false'])"));
            }
        }

        [Fact]
        public void serialize_whenPathNull()
        {
            var obj = new FileMoveCommand();

            var navigator = obj.XmlSerialize().CreateNavigator();

            if (null == navigator.NameTable)
            {
                Assert.NotNull(navigator.NameTable);
            }
            else
            {
                Assert.True(navigator.Evaluate<bool>(@"1 = count(/file.move[not(@source)][not(@destination)])"));
            }
        }
    }
}