namespace Cavity.Commands
{
    using System;
    using System.IO;
    using Cavity.IO;
    using Cavity.Xml.XPath;
    using Xunit;

    public sealed class FileCopyCommandFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<FileCopyCommand>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .XmlRoot("file.copy")
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new FileCopyCommand());
        }

        [Fact]
        public void ctor_FileInfo_FileInfo()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToFile("from.txt");
                var destination = temp.Info.ToFile("to.txt");

                Assert.NotNull(new FileCopyCommand(source, destination));
            }
        }

        [Fact]
        public void ctor_FileInfo_FileInfo_bool()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToFile("from.txt");
                var destination = temp.Info.ToFile("to.txt");

                Assert.NotNull(new FileCopyCommand(source, destination, true));
            }
        }

        [Fact]
        public void ctor_bool()
        {
            Assert.NotNull(new FileCopyCommand(true));
        }

        [Fact]
        public void ctor_string_string()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToFile("from.txt").FullName;
                var destination = temp.Info.ToFile("to.txt").FullName;

                Assert.NotNull(new FileCopyCommand(source, destination));
            }
        }

        [Fact]
        public void ctor_string_string_bool()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToFile("from.txt").FullName;
                var destination = temp.Info.ToFile("to.txt").FullName;

                Assert.NotNull(new FileCopyCommand(source, destination, true));
            }
        }

        [Fact]
        public void deserialize()
        {
            var obj = @"<file.copy source='C:\from.txt' destination='C:\to.txt' undo='true' unidirectional='true' />".XmlDeserialize<FileCopyCommand>();

            Assert.Equal(@"C:\from.txt", obj.Source);
            Assert.Equal(@"C:\to.txt", obj.Destination);
            Assert.True(obj.Undo);
            Assert.True(obj.Unidirectional);
        }

        [Fact]
        public void deserializeEmpty()
        {
            var obj = "<file.copy />".XmlDeserialize<FileCopyCommand>();

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

                var obj = new FileCopyCommand(source, destination);

                Assert.True(obj.Act());
                Assert.True(obj.Undo);
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

                var obj = new FileCopyCommand(source, destination);

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

                var obj = new FileCopyCommand(source, destination);

                Assert.Throws<FileNotFoundException>(() => obj.Act());
            }
        }

        [Fact]
        public void op_GetSchema()
        {
            Assert.Throws<NotSupportedException>(() => new FileCopyCommand().GetSchema());
        }

        [Fact]
        public void op_ReadXml_XmlReaderNull()
        {
            Assert.Throws<ArgumentNullException>(() => new FileCopyCommand().ReadXml(null));
        }

        [Fact]
        public void op_Revert()
        {
            using (var temp = new TempDirectory())
            {
                var source = temp.Info.ToFile("from.txt");
                source.CreateNew("example");
                var destination = temp.Info.ToFile("to.txt");

                var obj = new FileCopyCommand(source, destination);

                Assert.True(obj.Act());
                Assert.True(destination.Exists);
                Assert.True(obj.Revert());
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

                var obj = new FileCopyCommand(source, destination);

                Assert.True(obj.Act());
                obj.Undo = false;

                Assert.True(obj.Revert());
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

                var obj = new FileCopyCommand(source, destination, true);

                Assert.True(obj.Act());
                Assert.False(obj.Undo);

                Assert.True(obj.Revert());
                Assert.Equal("example", destination.ReadToEnd());
            }
        }

        [Fact]
        public void op_WriteXml_XmlWriterNull()
        {
            Assert.Throws<ArgumentNullException>(() => new FileCopyCommand().WriteXml(null));
        }

        [Fact]
        public void prop_Destination()
        {
            Assert.True(new PropertyExpectations<FileCopyCommand>(p => p.Destination)
                            .IsAutoProperty<string>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Source()
        {
            Assert.True(new PropertyExpectations<FileCopyCommand>(p => p.Source)
                            .IsAutoProperty<string>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void serialize()
        {
            var obj = new FileCopyCommand
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
                Assert.True(navigator.Evaluate<bool>(@"1 = count(/file.copy[@source='C:\from.txt'][@destination='C:\to.txt'][@undo='true'][@unidirectional='false'])"));
            }
        }

        [Fact]
        public void serialize_whenPathNull()
        {
            var obj = new FileCopyCommand();

            var navigator = obj.XmlSerialize().CreateNavigator();

            if (null == navigator.NameTable)
            {
                Assert.NotNull(navigator.NameTable);
            }
            else
            {
                Assert.True(navigator.Evaluate<bool>(@"1 = count(/file.copy[not(@source)][not(@destination)])"));
            }
        }
    }
}