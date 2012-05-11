namespace Cavity
{
    using System;
    using System.Collections.Generic;

    using Cavity.IO;
    using Cavity.Testing;

    using Xunit;

    public sealed class ProgramFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(Program).IsStatic());
        }

        [Fact]
        public void op_Main_strings()
        {
            var args = new List<string>
                           {
                               "/?"
                           };

            Program.Main(args.ToArray());
        }

        [Fact]
        public void op_Process_CommandLineHelp_string()
        {
            using (var temp = new TempDirectory())
            {
                var args = new List<string>
                               {
                                   "/?"
                               };

                Program.Process(CommandLine.Load(args.ToArray()), temp.Info.FullName);
            }
        }

        [Fact]
        public void op_Process_CommandLineInfo_string()
        {
            using (var temp = new TempDirectory())
            {
                ProjectFile.Create(temp.Info.ToFile("example.csproj"));
                var args = new List<string>
                               {
                                   "/i"
                               };

                Program.Process(CommandLine.Load(args.ToArray()), temp.Info.FullName);
            }
        }

        [Fact]
        public void op_Process_CommandLineNull_string()
        {
            using (var temp = new TempDirectory())
            {
                Assert.Throws<ArgumentNullException>(() => Program.Process(null, temp.Info.FullName));
            }
        }
    }
}