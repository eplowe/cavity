////namespace Cavity.IO
////{
////    using System;
////    using System.IO;
////    using System.Threading;
////    using Cavity.Models;
////    using Xunit;

////    public sealed class FileReceiverFacts
////    {
////        [Fact]
////        public void a_definition()
////        {
////            Assert.True(new TypeExpectations<FileReceiver<DummyIProcessFile>>()
////                            .DerivesFrom<object>()
////                            .IsConcreteClass()
////                            .IsUnsealed()
////                            .NoDefaultConstructor()
////                            .IsNotDecorated()
////                            .Implements<IReceiveFile>()
////                            .Implements<IDisposable>()
////                            .Result);
////        }

////        [Fact]
////        public void ctor()
////        {
////            Assert.NotNull(new FileReceiver<DummyIProcessFile>());
////        }

////        [Fact]
////        public void op_OnCreated()
////        {
////            using (var drop = new TempDirectory())
////            {
////                using (var obj = new FileReceiver<DummyIProcessFile>())
////                {
////                    obj.SetDropFolderWatch(drop.Info, "*.txt");
////                    var file = drop.Info.ToFile("example.txt");
////                    file.CreateNew();
////                    Thread.Sleep(new TimeSpan(0, 0, 1));

////                    file.Refresh();
////                    Assert.False(file.Exists);
////                }
////            }
////        }

////        [Fact]
////        public void op_OnRenamed()
////        {
////            using (var drop = new TempDirectory())
////            {
////                var file = drop.Info.ToFile("example.txt");
////                file.CreateNew();
////                using (var obj = new FileReceiver<DummyIProcessFile>())
////                {
////                    obj.SetDropFolderWatch(drop.Info, "*.txt");
////                    file.MoveTo(drop.Info.ToFile("renamed.txt").FullName);
////                    Thread.Sleep(new TimeSpan(0, 0, 1));

////                    file.Refresh();
////                    Assert.False(file.Exists);
////                }
////            }
////        }

////        [Fact]
////        public void op_OnTimerCallback()
////        {
////            using (var drop = new TempDirectory())
////            {
////                var file = drop.Info.ToFile("example.txt");
////                file.CreateNew();
////                using (var obj = new FileReceiver<DummyIProcessFile>())
////                {
////                    obj.SetDropFolderWatch(drop.Info, "*.txt");
////                    Thread.Sleep(new TimeSpan(0, 0, 11));

////                    file.Refresh();
////                    Assert.False(file.Exists);
////                }
////            }
////        }

////        [Fact]
////        public void op_Receive_FileInfo()
////        {
////            using (var drop = new TempDirectory())
////            {
////                var file = drop.Info.ToFile("example.txt");
////                file.CreateNew();
////                using (var obj = new FileReceiver<DummyIProcessFile>())
////                {
////                    obj.SetDropFolderWatch(drop.Info, "*.txt");
////                    obj.Receive(file);

////                    file.Refresh();
////                    Assert.False(file.Exists);
////                }
////            }
////        }

////        [Fact]
////        public void op_Receive_FileInfoNull()
////        {
////            using (var obj = new FileReceiver<DummyIProcessFile>())
////            {
////                Assert.Throws<ArgumentNullException>(() => obj.Receive(null as FileInfo));
////            }
////        }

////        [Fact]
////        public void op_Receive_string()
////        {
////            using (var drop = new TempDirectory())
////            {
////                var file = drop.Info.ToFile("example.txt");
////                file.CreateNew();
////                using (var obj = new FileReceiver<DummyIProcessFile>())
////                {
////                    obj.SetDropFolderWatch(drop.Info, "*.txt");
////                    obj.Receive(file.FullName);

////                    file.Refresh();
////                    Assert.False(file.Exists);
////                }
////            }
////        }

////        [Fact]
////        public void op_Receive_stringEmpty()
////        {
////            using (var obj = new FileReceiver<DummyIProcessFile>())
////            {
////                Assert.Throws<ArgumentOutOfRangeException>(() => obj.Receive(string.Empty));
////            }
////        }

////        [Fact]
////        public void op_Receive_stringNull()
////        {
////            using (var obj = new FileReceiver<DummyIProcessFile>())
////            {
////                Assert.Throws<ArgumentNullException>(() => obj.Receive(null as string));
////            }
////        }

////        [Fact]
////        public void op_SetDropFolderWatch_DirectoryInfo_string()
////        {
////            using (var obj = new FileReceiver<DummyIProcessFile>())
////            {
////                Assert.Throws<ArgumentNullException>(() => obj.SetDropFolderWatch(null, "*.txt"));
////            }
////        }
////    }
////}

