namespace Cavity.Build
{
    using System;
    using System.IO;
    using Cavity.Diagnostics;
    using Cavity.IO;
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;
    using Moq;
    using Xunit;

    public sealed class MessageLibraryFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<MessageLibrary>()
                            .DerivesFrom<Task>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new MessageLibrary());
        }

        ////[Fact]
        ////public void op_Execute()
        ////{
        ////    try
        ////    {
        ////        using (var temp = new TempDirectory())
        ////        {
        ////            var file = new FileInfo(Path.Combine(temp.Info.FullName, "example.mc"));
        ////            using (var stream = file.Open(FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
        ////            {
        ////                using (var writer = new StreamWriter(stream))
        ////                {
        ////                    writer.WriteLine(string.Empty);
        ////                }
        ////            }

        ////            using (var workingDirectory = new TempDirectory())
        ////            {
        ////                using (var stream = new FileInfo(Path.Combine(workingDirectory.Info.FullName, "example.rc"))
        ////                    .Open(FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
        ////                {
        ////                    using (var writer = new StreamWriter(stream))
        ////                    {
        ////                        writer.WriteLine(string.Empty);
        ////                    }
        ////                }

        ////                using (var stream = new FileInfo(Path.Combine(workingDirectory.Info.FullName, "example.res"))
        ////                    .Open(FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
        ////                {
        ////                    using (var writer = new StreamWriter(stream))
        ////                    {
        ////                        writer.WriteLine(string.Empty);
        ////                    }
        ////                }

        ////                ProcessFacade.Mock = new FakeProcess();

        ////                var task = new MessageLibrary
        ////                {
        ////                    BuildEngine = new Mock<IBuildEngine>().Object,
        ////                    WorkingDirectory = workingDirectory.Info.FullName,
        ////                    FrameworkSdkBin = @"C:\",
        ////                    VCInstallDirectory = @"C:\",
        ////                    Output = Path.Combine(workingDirectory.Info.FullName, "example.dll"),
        ////                    Files = new ITaskItem[]
        ////                    {
        ////                        new TaskItem(file.FullName)
        ////                    }
        ////                };

        ////                Assert.True(task.Execute());
        ////            }
        ////        }
        ////    }
        ////    finally
        ////    {
        ////        ProcessFacade.Reset();
        ////    }
        ////}

        ////[Fact]
        ////public void op_Execute_whenInvalidOperation()
        ////{
        ////    try
        ////    {
        ////        using (var temp = new TempDirectory())
        ////        {
        ////            var file = new FileInfo(Path.Combine(temp.Info.FullName, "example.mc"));
        ////            using (var stream = file.Open(FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
        ////            {
        ////                using (var writer = new StreamWriter(stream))
        ////                {
        ////                    writer.WriteLine(string.Empty);
        ////                }
        ////            }

        ////            using (var workingDirectory = new TempDirectory())
        ////            {
        ////                using (var stream = new FileInfo(Path.Combine(workingDirectory.Info.FullName, "example.rc"))
        ////                    .Open(FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
        ////                {
        ////                    using (var writer = new StreamWriter(stream))
        ////                    {
        ////                        writer.WriteLine(string.Empty);
        ////                    }
        ////                }

        ////                using (var stream = new FileInfo(Path.Combine(workingDirectory.Info.FullName, "example.res"))
        ////                    .Open(FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
        ////                {
        ////                    using (var writer = new StreamWriter(stream))
        ////                    {
        ////                        writer.WriteLine(string.Empty);
        ////                    }
        ////                }

        ////                var process = new Mock<IProcess>();
        ////                process
        ////                    .SetupProperty(x => x.StartInfo);
        ////                process
        ////                    .Setup(x => x.Start())
        ////                    .Returns(true);

        ////                using (var stream = new MemoryStream())
        ////                {
        ////                    using (var writer = new StreamWriter(stream))
        ////                    {
        ////                        using (var reader = new StreamReader(stream))
        ////                        {
        ////                            const string expected = "example";
        ////                            writer.Write(expected);
        ////                            writer.Flush();
        ////                            stream.Position = 0;

        ////                            process
        ////                                .SetupGet(x => x.StandardOutput)
        ////                                .Returns(reader);

        ////                            ProcessFacade.Mock = process.Object;

        ////                            var task = new MessageLibrary
        ////                            {
        ////                                BuildEngine = new Mock<IBuildEngine>().Object,
        ////                                WorkingDirectory = workingDirectory.Info.FullName,
        ////                                FrameworkSdkBin = @"C:\",
        ////                                Output = Path.Combine(workingDirectory.Info.FullName, "example.dll"),
        ////                                Files = new ITaskItem[]
        ////                                {
        ////                                    new TaskItem(file.FullName)
        ////                                }
        ////                            };

        ////                            Assert.False(task.Execute());
        ////                        }
        ////                    }
        ////                }
        ////            }
        ////        }
        ////    }
        ////    finally
        ////    {
        ////        ProcessFacade.Reset();
        ////    }
        ////}

        ////[Fact]
        ////public void op_Execute_whenOutputDirectoryMissing()
        ////{
        ////    try
        ////    {
        ////        using (var temp = new TempDirectory())
        ////        {
        ////            var file = new FileInfo(Path.Combine(temp.Info.FullName, "example.mc"));
        ////            using (var stream = file.Open(FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
        ////            {
        ////                using (var writer = new StreamWriter(stream))
        ////                {
        ////                    writer.WriteLine(string.Empty);
        ////                }
        ////            }

        ////            using (var workingDirectory = new TempDirectory())
        ////            {
        ////                using (var stream = new FileInfo(Path.Combine(workingDirectory.Info.FullName, "example.rc"))
        ////                    .Open(FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
        ////                {
        ////                    using (var writer = new StreamWriter(stream))
        ////                    {
        ////                        writer.WriteLine(string.Empty);
        ////                    }
        ////                }

        ////                using (var stream = new FileInfo(Path.Combine(workingDirectory.Info.FullName, "example.res"))
        ////                    .Open(FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
        ////                {
        ////                    using (var writer = new StreamWriter(stream))
        ////                    {
        ////                        writer.WriteLine(string.Empty);
        ////                    }
        ////                }

        ////                ProcessFacade.Mock = new FakeProcess();

        ////                var task = new MessageLibrary
        ////                {
        ////                    BuildEngine = new Mock<IBuildEngine>().Object,
        ////                    WorkingDirectory = workingDirectory.Info.FullName,
        ////                    FrameworkSdkBin = @"C:\",
        ////                    VCInstallDirectory = @"C:\",
        ////                    Output = Path.Combine(Path.Combine(workingDirectory.Info.FullName, "bin"), "example.dll"),
        ////                    Files = new ITaskItem[]
        ////                    {
        ////                        new TaskItem(file.FullName)
        ////                    }
        ////                };

        ////                Assert.True(task.Execute());
        ////            }
        ////        }
        ////    }
        ////    finally
        ////    {
        ////        ProcessFacade.Reset();
        ////    }
        ////}

        [Fact]
        public void op_Execute_whenOutputUnspecified()
        {
            try
            {
                using (var temp = new TempDirectory())
                {
                    var file = new FileInfo(Path.Combine(temp.Info.FullName, "example.mc"));
                    using (var stream = file.Open(FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                    {
                        using (var writer = new StreamWriter(stream))
                        {
                            writer.WriteLine(string.Empty);
                        }
                    }

                    using (var workingDirectory = new TempDirectory())
                    {
                        using (var stream = new FileInfo(Path.Combine(workingDirectory.Info.FullName, "example.rc"))
                            .Open(FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                        {
                            using (var writer = new StreamWriter(stream))
                            {
                                writer.WriteLine(string.Empty);
                            }
                        }

                        using (var stream = new FileInfo(Path.Combine(workingDirectory.Info.FullName, "example.res"))
                            .Open(FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                        {
                            using (var writer = new StreamWriter(stream))
                            {
                                writer.WriteLine(string.Empty);
                            }
                        }

                        ProcessFacade.Mock = new FakeProcess();

                        var task = new MessageLibrary
                        {
                            BuildEngine = new Mock<IBuildEngine>().Object,
                            WorkingDirectory = workingDirectory.Info.FullName,
                            Files = new ITaskItem[]
                            {
                                new TaskItem(file.FullName)
                            }
                        };

                        Assert.Throws<ArgumentNullException>(() => task.Execute());
                    }
                }
            }
            finally
            {
                ProcessFacade.Reset();
            }
        }

        ////[Fact]
        ////public void op_Execute_whenWorkingFolderMissing()
        ////{
        ////    try
        ////    {
        ////        using (var temp = new TempDirectory())
        ////        {
        ////            var file = new FileInfo(Path.Combine(temp.Info.FullName, "example.mc"));
        ////            using (var stream = file.Open(FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
        ////            {
        ////                using (var writer = new StreamWriter(stream))
        ////                {
        ////                    writer.WriteLine(string.Empty);
        ////                }
        ////            }

        ////            using (var workingDirectory = new TempDirectory())
        ////            {
        ////                workingDirectory.Info.Delete();

        ////                ProcessFacade.Mock = new FakeProcess();

        ////                var task = new MessageLibrary
        ////                {
        ////                    BuildEngine = new Mock<IBuildEngine>().Object,
        ////                    WorkingDirectory = workingDirectory.Info.FullName,
        ////                    FrameworkSdkBin = @"C:\",
        ////                    Output = Path.Combine(workingDirectory.Info.FullName, "example.dll"),
        ////                    Files = new ITaskItem[]
        ////                    {
        ////                        new TaskItem(file.FullName)
        ////                    }
        ////                };

        ////                Assert.Throws<ArgumentOutOfRangeException>(() => task.Execute());
        ////                Assert.True(workingDirectory.Info.Exists);
        ////            }
        ////        }
        ////    }
        ////    finally
        ////    {
        ////        ProcessFacade.Reset();
        ////    }
        ////}

        ////[Fact]
        ////public void op_Execute_whenWorkingFolderUnspecified()
        ////{
        ////    try
        ////    {
        ////        using (var temp = new TempDirectory())
        ////        {
        ////            var file = new FileInfo(Path.Combine(temp.Info.FullName, "example.mc"));
        ////            using (var stream = file.Open(FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
        ////            {
        ////                using (var writer = new StreamWriter(stream))
        ////                {
        ////                    writer.WriteLine(string.Empty);
        ////                }
        ////            }

        ////            ProcessFacade.Mock = new FakeProcess();

        ////            var task = new MessageLibrary
        ////            {
        ////                BuildEngine = new Mock<IBuildEngine>().Object,
        ////                FrameworkSdkBin = @"C:\",
        ////                Output = Path.Combine(temp.Info.FullName, "example.dll"),
        ////                Files = new ITaskItem[]
        ////                {
        ////                    new TaskItem(file.FullName)
        ////                }
        ////            };

        ////            Assert.Throws<ArgumentOutOfRangeException>(() => task.Execute());
        ////        }
        ////    }
        ////    finally
        ////    {
        ////        ProcessFacade.Reset();
        ////    }
        ////}

        [Fact]
        public void prop_Files()
        {
            Assert.True(new PropertyExpectations<MessageLibrary>(p => p.Files)
                            .IsAutoProperty<ITaskItem[]>()
                            .IsDecoratedWith<RequiredAttribute>()
                            .Result);
        }

        [Fact]
        public void prop_FrameworkSdkBin()
        {
            Assert.True(new PropertyExpectations<MessageLibrary>(p => p.FrameworkSdkBin)
                            .IsAutoProperty<string>()
                            .IsDecoratedWith<RequiredAttribute>()
                            .Result);
        }

        [Fact]
        public void prop_Output()
        {
            Assert.True(new PropertyExpectations<MessageLibrary>(p => p.Output)
                            .IsAutoProperty<string>()
                            .IsDecoratedWith<RequiredAttribute>()
                            .Result);
        }

        [Fact]
        public void prop_VCInstallDirectory()
        {
            Assert.True(new PropertyExpectations<MessageLibrary>(p => p.VCInstallDirectory)
                            .IsAutoProperty<string>()
                            .IsDecoratedWith<RequiredAttribute>()
                            .Result);
        }

        [Fact]
        public void prop_WorkingDirectory()
        {
            Assert.True(new PropertyExpectations<MessageLibrary>(p => p.WorkingDirectory)
                            .IsAutoProperty<string>()
                            .IsDecoratedWith<RequiredAttribute>()
                            .Result);
        }
    }
}