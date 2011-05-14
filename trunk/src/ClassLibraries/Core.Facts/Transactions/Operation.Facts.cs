////namespace Cavity.Transactions
////{
////    using System;
////    using System.IO;
////    using System.Linq;
////    using Cavity.Commands;
////    using Cavity.IO;
////    using Cavity.Xml.Serialization;
////    using Cavity.Xml.XPath;
////    using Xunit;

////    public sealed class OperationFacts
////    {
////        [Fact]
////        public void a_definition()
////        {
////            Assert.True(new TypeExpectations<Operation>()
////                            .DerivesFrom<object>()
////                            .IsConcreteClass()
////                            .IsSealed()
////                            .HasDefaultConstructor()
////                            .XmlRoot("operation")
////                            .Result);
////        }

////        [Fact]
////        public void ctor()
////        {
////            Assert.NotNull(new Operation());
////        }

////        [Fact]
////        public void ctor_Guid()
////        {
////            Assert.NotNull(new Operation(Guid.NewGuid()));
////        }

////        [Fact]
////        public void deserialize()
////        {
////            using (var temp = new TempDirectory())
////            {
////                var obj = ("<operation completed='true' info='example'>" +
////                           "<commands>" +
////                           "<command type='Cavity.Commands.DirectoryCreateCommand, Cavity.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=c0c289e4846931e8'>" +
////                           @"<directory.create path='{0}' undo='false' />".FormatWith(temp.Info.FullName) +
////                           "</command>" +
////                           "</commands>" +
////                           "</operation>").XmlDeserialize<Operation>();

////                Assert.Equal("example", obj.Info);
////                Assert.Equal(1, obj.Commands.Count);
////                Assert.Equal(temp.Info.FullName, ((DirectoryCreateCommand)obj.Commands.First()).Path);
////            }
////        }

////        [Fact]
////        public void deserializeEmpty()
////        {
////            Assert.NotNull("<operation />".XmlDeserialize<Operation>());
////        }

////        [Fact]
////        public void op_Do()
////        {
////            try
////            {
////                using (var temp = new TempDirectory())
////                {
////                    Recovery.MasterDirectory = temp.Info.ToDirectory("Recovery");
////                    var path1 = temp.Info.ToDirectory("1").FullName;
////                    var path2 = temp.Info.ToDirectory("2").FullName;
////                    var obj = new Operation(Guid.NewGuid())
////                    {
////                        Info = Guid.NewGuid().ToString()
////                    };
////                    obj.Commands.Add(new DirectoryCreateCommand(path1));
////                    obj.Commands.Add(new DirectoryCreateCommand(path2));

////                    Assert.True(obj.Do());
////                    Assert.True(new DirectoryInfo(path1).Exists);
////                    Assert.True(new DirectoryInfo(path2).Exists);
////                }
////            }
////            finally
////            {
////                Recovery.MasterDirectory = null;
////            }
////        }

////        [Fact]
////        public void op_Do_whenEmpty()
////        {
////            var obj = new Operation(Guid.NewGuid())
////            {
////                Info = Guid.NewGuid().ToString()
////            };

////            Assert.True(obj.Do());
////        }

////        [Fact]
////        public void op_Do_whenInfoNull()
////        {
////            Assert.Throws<InvalidOperationException>(() => new Operation(Guid.NewGuid()).Do());
////        }

////        [Fact]
////        public void op_Undo()
////        {
////            try
////            {
////                using (var temp = new TempDirectory())
////                {
////                    Recovery.MasterDirectory = temp.Info.ToDirectory("Recovery");
////                    var path1 = temp.Info.ToDirectory("1").FullName;
////                    var path2 = temp.Info.ToDirectory("1").ToDirectory("2").FullName;
////                    var obj = new Operation(Guid.NewGuid())
////                    {
////                        Info = Guid.NewGuid().ToString()
////                    };
////                    obj.Commands.Add(new DirectoryCreateCommand(path1));
////                    obj.Commands.Add(new DirectoryCreateCommand(path2));

////                    Assert.True(obj.Do());
////                    Assert.True(new DirectoryInfo(path1).Exists);
////                    Assert.True(new DirectoryInfo(path2).Exists);

////                    Assert.True(obj.Undo());
////                    Assert.False(new DirectoryInfo(path2).Exists);
////                    Assert.False(new DirectoryInfo(path1).Exists);
////                }
////            }
////            finally
////            {
////                Recovery.MasterDirectory = null;
////            }
////        }

////        [Fact]
////        public void op_Undo_whenEmpty()
////        {
////            var obj = new Operation(Guid.NewGuid())
////            {
////                Info = Guid.NewGuid().ToString()
////            };

////            Assert.True(obj.Undo());
////        }

////        [Fact]
////        public void op_Undo_whenInfoNull()
////        {
////            Assert.Throws<InvalidOperationException>(() => new Operation(Guid.NewGuid()).Undo());
////        }

////        [Fact]
////        public void prop_Commands()
////        {
////            Assert.NotNull(new PropertyExpectations<Operation>(p => p.Commands)
////                               .TypeIs<XmlSerializableCommandCollection>()
////                               .XmlElement("commands")
////                               .Result);
////        }

////        [Fact]
////        public void prop_Identity()
////        {
////            Assert.NotNull(new PropertyExpectations<Operation>(p => p.Identity)
////                               .IsAutoProperty<EnlistmentIdentity>()
////                               .XmlIgnore()
////                               .Result);
////        }

////        [Fact]
////        public void prop_Info()
////        {
////            Assert.NotNull(new PropertyExpectations<Operation>(p => p.Info)
////                               .IsAutoProperty<string>()
////                               .XmlAttribute("info")
////                               .Result);
////        }

////        [Fact]
////        public void serialize()
////        {
////            using (var temp = new TempDirectory())
////            {
////                var obj = new Operation(Guid.NewGuid())
////                {
////                    Info = "example"
////                };
////                obj.Commands.Add(new DirectoryCreateCommand(temp.Info.FullName));

////                var navigator = obj.XmlSerialize().CreateNavigator();

////                Assert.True(navigator.Evaluate<bool>("1 = count(/operation/commands/command)"));
////                var xpath = "1 = count(/operation[@info='example']/commands/command[@type='{0}']/directory.create[@path='{1}'][@undo='false'])".FormatWith(typeof(DirectoryCreateCommand).AssemblyQualifiedName, temp.Info.FullName);
////                Assert.True(navigator.Evaluate<bool>(xpath));
////            }
////        }

////        [Fact]
////        public void serialize_whenEmpty()
////        {
////            var obj = new Operation(Guid.NewGuid());

////            var navigator = obj.XmlSerialize().CreateNavigator();

////            Assert.True(navigator.Evaluate<bool>("1 = count(/operation)"));
////        }
////    }
////}