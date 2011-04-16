namespace Cavity.Build.Sdk
{
    using System.IO;
    using Cavity;
    using Cavity.IO;
    using Cavity.Win32;
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
        public void prop_Current()
        {
            try
            {
                using (var temp = new TempDirectory())
                {
                    RegistryFacade.Fake[@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows"]["CurrentVersion"] = "v00.00";
                    RegistryFacade.Fake[@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SDKs\Windows\v00.00"]["InstallationFolder"] = temp.Info.FullName;
                    temp.Info.CreateSubdirectory("bin");
                    var exe = new FileInfo(Path.Combine(temp.Info.FullName, @"bin\mc.exe"));
                    using (var stream = exe.Open(FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.WriteLine(string.Empty);
                    }

                    Assert.NotNull(MessageCompiler.Current);
                }
            }
            finally
            {
                RegistryFacade.Reset();
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