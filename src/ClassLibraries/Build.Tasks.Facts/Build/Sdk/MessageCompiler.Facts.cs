namespace Cavity.Build.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Cavity;
    using Cavity.Diagnostics;
    using Cavity.IO;
    using Cavity.Win32;
    using Moq;
    using Xunit;

    public sealed class MessageCompilerFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<MessageCompiler>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .NoDefaultConstructor()
                .IsNotDecorated()
                .Result);
        }

        [Fact]
        public void op_Compile_DirectoryInfoNull_IEnumerableOfFileInfo()
        {
            DirectoryInfo outputPath = null;
            var files = new List<FileInfo>
            {
                new FileInfo(@"C:\Temp\example.mc")
            };

            using (new FakePlatformSdk())
            {
                Assert.Throws<ArgumentNullException>(() => MessageCompiler.Current.Compile(outputPath, files));
            }
        }

        [Fact]
        public void op_Compile_DirectoryInfoNull_IEnumerableOfFileInfoEmpty()
        {
            var outputPath = new DirectoryInfo(Path.GetTempPath());
            var files = new List<FileInfo>();

            using (new FakePlatformSdk())
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => MessageCompiler.Current.Compile(outputPath, files));
            }
        }

        [Fact]
        public void op_Compile_DirectoryInfo_IEnumerableOfFileInfoNull()
        {
            var outputPath = new DirectoryInfo(Path.GetTempPath());
            IEnumerable<FileInfo> files = null;

            using (new FakePlatformSdk())
            {
                Assert.Throws<ArgumentNullException>(() => MessageCompiler.Current.Compile(outputPath, files));
            }
        }

        [Fact]
        public void op_Compile_DirectoryInfoNull_IEnumerableOfFileInfo_whenFileNotFound()
        {
            var outputPath = new DirectoryInfo(Path.GetTempPath());
            var files = new List<FileInfo>
            {
                new FileInfo(@"C:\Temp\example.mc")
            };

            using (new FakePlatformSdk())
            {
                Assert.Throws<FileNotFoundException>(() => MessageCompiler.Current.Compile(outputPath, files));
            }
        }

        [Fact]
        public void op_Compile_DirectoryInfo_IEnumerableOfFileInfo()
        {
            try
            {
                var outputPath = new DirectoryInfo(Path.GetTempPath());

                using (var temp = new TempDirectory())
                {
                    var file = new FileInfo(Path.Combine(temp.Info.FullName, "example.mc"));
                    var files = new List<FileInfo>
                    {
                        file
                    };

                    using (var stream = file.Open(FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                    {
                        using (var writer = new StreamWriter(stream))
                        {
                            writer.WriteLine(string.Empty);
                        }
                    }

                    var process = new Mock<IProcess>();
                    process
                        .SetupProperty(x => x.StartInfo);
                    process
                        .Setup(x => x.Start())
                        .Returns(true);

                    using (var stream = new MemoryStream())
                    using (var writer = new StreamWriter(stream))
                    using (var reader = new StreamReader(stream))
                    {
                        const string expected = "example";
                        writer.Write(expected);
                        writer.Flush();
                        stream.Position = 0;

                        process
                            .SetupGet(x => x.StandardOutput)
                            .Returns(reader);

                        ProcessFacade.Mock = process.Object;
                        using (var sdk = new FakePlatformSdk())
                        {
                            var actual = MessageCompiler.Current.Compile(outputPath, files);

                            Assert.Equal(expected, actual);

                            Assert.Equal("-u -U example.mc", process.Object.StartInfo.Arguments);
                            Assert.Equal(Path.Combine(sdk.InstallationFolder.Info.FullName, @"Bin\MC.exe"), process.Object.StartInfo.FileName);
                            Assert.True(process.Object.StartInfo.RedirectStandardError);
                            Assert.True(process.Object.StartInfo.RedirectStandardOutput);
                            Assert.False(process.Object.StartInfo.UseShellExecute);
                            Assert.False(new DirectoryInfo(process.Object.StartInfo.WorkingDirectory).Exists);
                        }
                    }
                }
            }
            finally
            {
                ProcessFacade.Reset();
            }
        }

        [Fact]
        public void op_Compile_DirectoryInfo_IEnumerableOfFileInfo_whenError()
        {
            try
            {
                var outputPath = new DirectoryInfo(Path.GetTempPath());

                using (var temp = new TempDirectory())
                {
                    var file = new FileInfo(Path.Combine(temp.Info.FullName, "example.mc"));
                    var files = new List<FileInfo>
                    {
                        file
                    };

                    using (var stream = file.Open(FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                    {
                        using (var writer = new StreamWriter(stream))
                        {
                            writer.WriteLine(string.Empty);
                        }
                    }

                    var process = new Mock<IProcess>();
                    process
                        .SetupProperty(x => x.StartInfo);
                    process
                        .SetupGet(x => x.ExitCode)
                        .Returns(1);
                    process
                        .Setup(x => x.Start())
                        .Returns(true);

                    using (var outputStream = new MemoryStream())
                    using (var outputWriter = new StreamWriter(outputStream))
                    using (var outputReader = new StreamReader(outputStream))
                    {
                        outputWriter.Write("example");
                        outputWriter.Flush();
                        outputStream.Position = 0;

                        process
                            .SetupGet(x => x.StandardOutput)
                            .Returns(outputReader);

                        using (var errorStream = new MemoryStream())
                        using (var errorWriter = new StreamWriter(errorStream))
                        using (var errorReader = new StreamReader(errorStream))
                        {
                            errorWriter.Write("error");
                            errorWriter.Flush();
                            errorStream.Position = 0;

                            process
                                .SetupGet(x => x.StandardError)
                                .Returns(errorReader);

                            ProcessFacade.Mock = process.Object;
                            using (new FakePlatformSdk())
                            {
                                Assert.Throws<InvalidOperationException>(() => MessageCompiler.Current.Compile(outputPath, files));
                            }
                        }
                    }
                }
            }
            finally
            {
                ProcessFacade.Reset();
            }
        }

        [Fact]
        public void prop_Current()
        {
            using (new FakePlatformSdk())
            {
                Assert.NotNull(MessageCompiler.Current);
            }
        }

        [Fact]
        public void prop_Current_whenExeFileMissing()
        {
            try
            {
                using (var temp = new TempDirectory())
                {
                    RegistryFacade.Fake[@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows"]["CurrentVersion"] = "v00.00";
                    RegistryFacade.Fake[@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v00.00"]["InstallationFolder"] = temp.Info.FullName;
                    temp.Info.CreateSubdirectory("bin");

                    Assert.Null(MessageCompiler.Current);
                }
            }
            finally
            {
                RegistryFacade.Reset();
            }
        }

        [Fact]
        public void prop_Current_whenBinDirectoryMissing()
        {
            try
            {
                using (var temp = new TempDirectory())
                {
                    RegistryFacade.Fake[@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows"]["CurrentVersion"] = "v00.00";
                    RegistryFacade.Fake[@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v00.00"]["InstallationFolder"] = temp.Info.FullName;

                    Assert.Null(MessageCompiler.Current);
                }
            }
            finally
            {
                RegistryFacade.Reset();
            }
        }

        [Fact]
        public void prop_Current_whenInstallationFolderDirectoryMissing()
        {
            try
            {
                using (var temp = new TempDirectory())
                {
                    RegistryFacade.Fake[@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows"]["CurrentVersion"] = "v00.00";
                    RegistryFacade.Fake[@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v00.00"]["InstallationFolder"] = temp.Info.FullName;
                }

                Assert.Null(MessageCompiler.Current);
            }
            finally
            {
                RegistryFacade.Reset();
            }
        }

        [Fact]
        public void prop_Current_whenInstallationFolderMissing()
        {
            try
            {
                RegistryFacade.Fake[@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows"]["CurrentVersion"] = "v00.00";

                Assert.Null(MessageCompiler.Current);
            }
            finally
            {
                RegistryFacade.Reset();
            }
        }

        [Fact]
        public void prop_Current_whenCurrentVersionMissing()
        {
            try
            {
                RegistryFacade.Fake["foo"]["bar"] = "example";

                Assert.Null(MessageCompiler.Current);
            }
            finally
            {
                RegistryFacade.Reset();
            }
        }
    }
}