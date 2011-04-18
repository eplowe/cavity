namespace Cavity.Build.Sdk
{
    using System.Collections.Generic;
    using System.IO;
    using Xunit;

    public sealed class MessageCompilerFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<MessageCompiler>()
                            .DerivesFrom<CompilerBase>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void ctor_FileInfo()
        {
            Assert.NotNull(new MessageCompiler(new FileInfo("MC.exe")));
        }

        [Fact]
        public void op_ToArguments_IEnumerableOfString()
        {
            var files = new List<string>
            {
                "example.1",
                "example.2"
            };

            const string expected = "-u -U example.1 example.2";
            var actual = new MessageCompiler(new FileInfo("MC.exe")).ToArguments(files);

            Assert.Equal(expected, actual);
        }
    }
}