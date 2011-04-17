namespace Cavity.Build.Sdk
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using Cavity.Diagnostics;
    using Cavity.IO;
    using Cavity.Win32;
    using Moq;
    using Xunit;

    public sealed class CompilerBaseFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<CompilerBase>()
                            .DerivesFrom<object>()
                            .IsAbstractBaseClass()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void op_Compile_DirectoryInfoNull_IEnumerableOfFileInfo()
        {
            DirectoryInfo outputPath = null;
            var files = new List<FileInfo>
            {
                new FileInfo(@"C:\example.file")
            };

            var mock = new Mock<CompilerBase>(new FileInfo(@"C:\example.exe"));

            Assert.Throws<ArgumentNullException>(() => mock.Object.Compile(outputPath, files));
        }

        [Fact]
        public void op_Compile_DirectoryInfoNull_IEnumerableOfFileInfoEmpty()
        {
            var outputPath = new DirectoryInfo(Path.GetTempPath());
            var files = new List<FileInfo>();

            var mock = new Mock<CompilerBase>(new FileInfo(@"C:\example.exe"));

            Assert.Throws<ArgumentOutOfRangeException>(() => mock.Object.Compile(outputPath, files));
        }

        [Fact]
        public void op_Compile_DirectoryInfoNull_IEnumerableOfFileInfo_whenFileNotFound()
        {
            var outputPath = new DirectoryInfo(Path.GetTempPath());
            var files = new List<FileInfo>
            {
                new FileInfo(@"C:\example.file")
            };

            var mock = new Mock<CompilerBase>(new FileInfo(@"C:\example.exe"));

            Assert.Throws<Win32Exception>(() => mock.Object.Compile(outputPath, files));
        }

        [Fact]
        public void op_Compile_DirectoryInfo_IEnumerableOfFileInfo()
        {
            try
            {
                using (var temp = new TempDirectory())
                {
                    var file = new FileInfo(Path.Combine(temp.Info.FullName, "example.file"));
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
                    {
                        using (var writer = new StreamWriter(stream))
                        {
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

                                const string args = "/?";
                                var exe = new FileInfo(@"C:\example.exe");
                                var mock = new Mock<CompilerBase>(exe);
                                mock
                                    .Setup(x => x.ToArguments(It.IsAny<IEnumerable<string>>()))
                                    .Returns(args);
                                var actual = mock.Object.Compile(temp.Info, files);

                                Assert.Equal(expected, actual);

                                Assert.Equal(args, process.Object.StartInfo.Arguments);
                                Assert.Equal(exe.FullName, process.Object.StartInfo.FileName);
                                Assert.True(process.Object.StartInfo.RedirectStandardError);
                                Assert.True(process.Object.StartInfo.RedirectStandardOutput);
                                Assert.False(process.Object.StartInfo.UseShellExecute);
                                Assert.Equal(temp.Info.FullName, process.Object.StartInfo.WorkingDirectory);
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
        public void op_Compile_DirectoryInfo_IEnumerableOfFileInfo_whenToolName()
        {
            try
            {
                using (var temp = new TempDirectory())
                {
                    var file = new FileInfo(Path.Combine(temp.Info.FullName, "example.file"));
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
                    {
                        using (var writer = new StreamWriter(stream))
                        {
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

                                const string args = "/?";
                                const string exe = "example.exe";
                                var mock = new Mock<CompilerBase>(exe);
                                mock
                                    .Setup(x => x.ToArguments(It.IsAny<IEnumerable<string>>()))
                                    .Returns(args);
                                var actual = mock.Object.Compile(temp.Info, files);

                                Assert.Equal(expected, actual);

                                Assert.Equal(args, process.Object.StartInfo.Arguments);
                                Assert.Equal(exe, process.Object.StartInfo.FileName);
                                Assert.True(process.Object.StartInfo.RedirectStandardError);
                                Assert.True(process.Object.StartInfo.RedirectStandardOutput);
                                Assert.False(process.Object.StartInfo.UseShellExecute);
                                Assert.Equal(temp.Info.FullName, process.Object.StartInfo.WorkingDirectory);
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
        public void op_Compile_DirectoryInfo_IEnumerableOfFileInfoNull()
        {
            var outputPath = new DirectoryInfo(Path.GetTempPath());
            IEnumerable<FileInfo> files = null;

            var mock = new Mock<CompilerBase>(new FileInfo(@"C:\example.exe"));

            Assert.Throws<ArgumentNullException>(() => mock.Object.Compile(outputPath, files));
        }

        [Fact]
        public void op_Compile_DirectoryInfo_IEnumerableOfFileInfo_whenError()
        {
            try
            {
                var outputPath = new DirectoryInfo(Path.GetTempPath());

                using (var temp = new TempDirectory())
                {
                    var file = new FileInfo(Path.Combine(temp.Info.FullName, "example.file"));
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
                    {
                        using (var outputWriter = new StreamWriter(outputStream))
                        {
                            using (var outputReader = new StreamReader(outputStream))
                            {
                                outputWriter.Write("example");
                                outputWriter.Flush();
                                outputStream.Position = 0;

                                process
                                    .SetupGet(x => x.StandardOutput)
                                    .Returns(outputReader);

                                using (var errorStream = new MemoryStream())
                                {
                                    using (var errorWriter = new StreamWriter(errorStream))
                                    {
                                        using (var errorReader = new StreamReader(errorStream))
                                        {
                                            errorWriter.Write("error");
                                            errorWriter.Flush();
                                            errorStream.Position = 0;

                                            process
                                                .SetupGet(x => x.StandardError)
                                                .Returns(errorReader);

                                            ProcessFacade.Mock = process.Object;

                                            const string args = "/?";
                                            var exe = new FileInfo(@"C:\example.exe");
                                            var mock = new Mock<CompilerBase>(exe);
                                            mock
                                                .Setup(x => x.ToArguments(It.IsAny<IEnumerable<string>>()))
                                                .Returns(args);

                                            Assert.Throws<InvalidOperationException>(() => mock.Object.Compile(outputPath, files));
                                        }
                                    }
                                }
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
        public void op_ToArguments_string_IEnumerableOfString()
        {
            var files = new List<string>
            {
                "example.1",
                "example.2"
            };

            using (new FakePlatformSdk())
            {
                const string expected = "-a -B example.1 example.2";
                var actual = CompilerBase.ToArguments("-a -B", files);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_ToArguments_stringNull_IEnumerableOfString()
        {
            var files = new List<string>
            {
                "example.1",
                "example.2"
            };

            using (new FakePlatformSdk())
            {
                const string expected = "example.1 example.2";
                var actual = CompilerBase.ToArguments(null, files);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_ToArguments_stringEmpty_IEnumerableOfString()
        {
            var files = new List<string>
            {
                "example.1",
                "example.2"
            };

            using (new FakePlatformSdk())
            {
                const string expected = "example.1 example.2";
                var actual = CompilerBase.ToArguments(string.Empty, files);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void op_ToArguments_string_IEnumerableOfStringEmpty()
        {
            using (new FakePlatformSdk())
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => CompilerBase.ToArguments("-a -B", new List<string>()));
            }
        }

        [Fact]
        public void op_ToArguments_string_IEnumerableOfStringNull()
        {
            using (new FakePlatformSdk())
            {
                Assert.Throws<ArgumentNullException>(() => CompilerBase.ToArguments("-a -B", null));
            }
        }

        [Fact]
        public void op_ToFileNames_IEnumerableOfFileInfo()
        {
            var files = new List<FileInfo>
            {
                new FileInfo(@"C:\example.file")
            };

            Assert.Equal("example.file", CompilerBase.ToFileNames(files).First());
        }

        [Fact]
        public void op_ToFileNames_IEnumerableOfFileInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => CompilerBase.ToFileNames(null));
        }

        [Fact]
        public void op_ToApplicationPath_string()
        {
            using (new FakePlatformSdk())
            {
                Assert.NotNull(CompilerBase.ToApplicationPath("mc.exe"));
            }
        }

        [Fact]
        public void op_ToApplicationPath_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => CompilerBase.ToApplicationPath(null));
        }

        [Fact]
        public void op_ToApplicationPath_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => CompilerBase.ToApplicationPath(string.Empty));
        }

        [Fact]
        public void op_ToApplicationPath_string_whenBinDirectoryMissing()
        {
            try
            {
                using (var temp = new TempDirectory())
                {
                    RegistryFacade.Fake[@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows"]["CurrentVersion"] = "v00.00";
                    RegistryFacade.Fake[@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v00.00"]["InstallationFolder"] = temp.Info.FullName;

                    Assert.Null(CompilerBase.ToApplicationPath("example.exe"));
                }
            }
            finally
            {
                RegistryFacade.Reset();
            }
        }

        [Fact]
        public void op_ToApplicationPath_string_whenCurrentVersionMissing()
        {
            try
            {
                RegistryFacade.Fake["foo"]["bar"] = "example";

                Assert.Null(CompilerBase.ToApplicationPath("example.exe"));
            }
            finally
            {
                RegistryFacade.Reset();
            }
        }

        [Fact]
        public void op_ToApplicationPath_string_whenExeFileMissing()
        {
            try
            {
                using (var temp = new TempDirectory())
                {
                    RegistryFacade.Fake[@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows"]["CurrentVersion"] = "v00.00";
                    RegistryFacade.Fake[@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v00.00"]["InstallationFolder"] = temp.Info.FullName;
                    temp.Info.CreateSubdirectory("bin");

                    Assert.Null(CompilerBase.ToApplicationPath("example.exe"));
                }
            }
            finally
            {
                RegistryFacade.Reset();
            }
        }

        [Fact]
        public void op_ToApplicationPath_string_whenInstallationFolderDirectoryMissing()
        {
            try
            {
                using (var temp = new TempDirectory())
                {
                    RegistryFacade.Fake[@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows"]["CurrentVersion"] = "v00.00";
                    RegistryFacade.Fake[@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v00.00"]["InstallationFolder"] = temp.Info.FullName;
                }

                Assert.Null(CompilerBase.ToApplicationPath("example.exe"));
            }
            finally
            {
                RegistryFacade.Reset();
            }
        }

        [Fact]
        public void op_ToApplicationPath_string_whenInstallationFolderMissing()
        {
            try
            {
                RegistryFacade.Fake[@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows"]["CurrentVersion"] = "v00.00";

                Assert.Null(CompilerBase.ToApplicationPath("example.exe"));
            }
            finally
            {
                RegistryFacade.Reset();
            }
        }
    }
}