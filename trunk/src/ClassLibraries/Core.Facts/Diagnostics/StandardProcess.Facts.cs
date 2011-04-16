namespace Cavity.Diagnostics
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using Cavity;
    using Xunit;

    public sealed class StandardProcessFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<StandardProcess>()
                .DerivesFrom<object>()
                .IsConcreteClass()
                .IsSealed()
                .HasDefaultConstructor()
                .IsNotDecorated()
                .Implements<IProcess>()
                .Result);
        }

        [Fact]
        public void ctor()
        {
            using (var obj = new StandardProcess())
            {
                Assert.NotNull(obj);
            }
        }

        [Fact]
        public void prop_ExitCode()
        {
            Assert.True(new PropertyExpectations<StandardProcess>(p => p.ExitCode)
                            .TypeIs<int>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_ExitCode_get()
        {
            Assert.Throws<InvalidOperationException>(() => new StandardProcess().ExitCode);
        }

        [Fact]
        public void prop_StartInfo()
        {
            Assert.True(new PropertyExpectations<StandardProcess>(p => p.StartInfo)
                            .TypeIs<ProcessStartInfo>()
                            .DefaultValueIsNotNull()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_StandardError()
        {
            Assert.True(new PropertyExpectations<StandardProcess>(p => p.StandardError)
                            .TypeIs<StreamReader>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_StandardError_get()
        {
            Assert.Throws<InvalidOperationException>(() => new StandardProcess().StandardError);
        }

        [Fact]
        public void prop_StandardOutput()
        {
            Assert.True(new PropertyExpectations<StandardProcess>(p => p.StandardOutput)
                            .TypeIs<StreamReader>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_StandardOutput_get()
        {
            Assert.Throws<InvalidOperationException>(() => new StandardProcess().StandardOutput);
        }

        [Fact]
        public void op_Start()
        {
            using (var obj = new StandardProcess())
            {
                obj.StartInfo = new ProcessStartInfo
                {
                    Arguments = string.Empty,
                    FileName = "cmd.exe",
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    WorkingDirectory = Path.GetTempPath()
                };

                const bool expected = true;
                var actual = obj.Start();

                Assert.Equal(expected, actual);
            }
        }
    }
}