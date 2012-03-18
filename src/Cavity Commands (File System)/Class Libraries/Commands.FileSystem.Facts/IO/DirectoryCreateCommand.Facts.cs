namespace Cavity.IO
{
    using System;
    using System.IO;

    using Cavity.Xml.XPath;

    using Xunit;

    public sealed class DirectoryCreateCommandFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<DirectoryCreateCommand>()
                            .DerivesFrom<Command>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .XmlRoot("directory.create")
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new DirectoryCreateCommand());
        }

        [Fact]
        public void ctor_DirectoryInfo()
        {
            using (var temp = new TempDirectory())
            {
                Assert.NotNull(new DirectoryCreateCommand(temp.Info));
            }
        }

        [Fact]
        public void ctor_DirectoryInfoNull()
        {
            Assert.NotNull(new DirectoryCreateCommand(null as DirectoryInfo));
        }

        [Fact]
        public void ctor_DirectoryInfo_bool()
        {
            using (var temp = new TempDirectory())
            {
                Assert.NotNull(new DirectoryCreateCommand(temp.Info, true));
            }
        }

        [Fact]
        public void ctor_bool()
        {
            Assert.NotNull(new DirectoryCreateCommand(true));
        }

        [Fact]
        public void ctor_string()
        {
            using (var temp = new TempDirectory())
            {
                Assert.NotNull(new DirectoryCreateCommand(temp.Info.FullName));
            }
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.NotNull(new DirectoryCreateCommand(string.Empty));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.NotNull(new DirectoryCreateCommand(null as string));
        }

        [Fact]
        public void ctor_string_bool()
        {
            using (var temp = new TempDirectory())
            {
                Assert.NotNull(new DirectoryCreateCommand(temp.Info.FullName, true));
            }
        }

        [Fact]
        public void deserialize()
        {
            var obj = @"<directory.create path='C:\' undo='true' unidirectional='true' />".XmlDeserialize<DirectoryCreateCommand>();

            Assert.Equal(@"C:\", obj.Path);
            Assert.True(obj.Undo);
            Assert.True(obj.Unidirectional);
        }

        [Fact]
        public void deserializeEmpty()
        {
            var obj = "<directory.create />".XmlDeserialize<DirectoryCreateCommand>();

            Assert.Null(obj.Path);
            Assert.False(obj.Undo);
            Assert.False(obj.Unidirectional);
        }

        [Fact]
        public void op_Act()
        {
            using (var temp = new TempDirectory())
            {
                var obj = new DirectoryCreateCommand
                              {
                                  Path = temp.Info.ToDirectory("example").FullName, 
                                  Undo = true
                              };

                Assert.True(obj.Act());
                Assert.True(obj.Undo);
                Assert.True(new DirectoryInfo(obj.Path).Exists);
            }
        }

        [Fact]
        public void op_Act_whenPathAlreadyExists()
        {
            using (var temp = new TempDirectory())
            {
                var obj = new DirectoryCreateCommand
                              {
                                  Path = temp.Info.FullName, 
                                  Undo = true
                              };

                Assert.True(obj.Act());
                Assert.False(obj.Undo);
                Assert.True(new DirectoryInfo(obj.Path).Exists);
            }
        }

        [Fact]
        public void op_GetSchema()
        {
            Assert.Throws<NotSupportedException>(() => new DirectoryCreateCommand().GetSchema());
        }

        [Fact]
        public void op_ReadXml_XmlReaderNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DirectoryCreateCommand().ReadXml(null));
        }

        [Fact]
        public void op_Revert()
        {
            using (var temp = new TempDirectory())
            {
                var obj = new DirectoryCreateCommand
                              {
                                  Path = temp.Info.ToDirectory("example").FullName, 
                                  Undo = true
                              };

                Assert.True(obj.Act());
                Assert.True(obj.Revert());
                Assert.False(new DirectoryInfo(obj.Path).Exists);
            }
        }

        [Fact]
        public void op_Revert_whenNotUndo()
        {
            using (var temp = new TempDirectory())
            {
                var obj = new DirectoryCreateCommand
                              {
                                  Path = temp.Info.ToDirectory("example").FullName, 
                                  Undo = true
                              };

                Assert.True(obj.Act());
                obj.Undo = false;

                Assert.True(obj.Revert());
                Assert.True(new DirectoryInfo(obj.Path).Exists);
            }
        }

        [Fact]
        public void op_Revert_whenUnidirectional()
        {
            using (var temp = new TempDirectory())
            {
                var obj = new DirectoryCreateCommand(true)
                              {
                                  Path = temp.Info.ToDirectory("example").FullName, 
                                  Undo = true
                              };

                Assert.True(obj.Act());
                Assert.True(obj.Revert());
                Assert.True(new DirectoryInfo(obj.Path).Exists);
            }
        }

        [Fact]
        public void op_WriteXml_XmlWriterNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DirectoryCreateCommand().WriteXml(null));
        }

        [Fact]
        public void prop_Path()
        {
            Assert.True(new PropertyExpectations<DirectoryCreateCommand>(p => p.Path)
                            .IsAutoProperty<string>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Undo()
        {
            Assert.True(new PropertyExpectations<DirectoryCreateCommand>(p => p.Undo)
                            .TypeIs<bool>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void serialize()
        {
            var obj = new DirectoryCreateCommand
                          {
                              Path = @"C:\", 
                              Undo = true
                          };

            var navigator = obj.XmlSerialize().CreateNavigator();

            if (null == navigator.NameTable)
            {
                Assert.NotNull(navigator.NameTable);
            }
            else
            {
                Assert.True(navigator.Evaluate<bool>(@"1 = count(/directory.create[@path='C:\'][@undo='true'][@unidirectional='false'])"));
            }
        }

        [Fact]
        public void serialize_whenPathNull()
        {
            var obj = new DirectoryCreateCommand();

            var navigator = obj.XmlSerialize().CreateNavigator();

            if (null == navigator.NameTable)
            {
                Assert.NotNull(navigator.NameTable);
            }
            else
            {
                Assert.True(navigator.Evaluate<bool>(@"1 = count(/directory.create[not(@path)])"));
            }
        }
    }
}