namespace Cavity.Build.Sdk
{
    using System.Collections.Generic;
    using System.IO;
    using Xunit;

    public sealed class LinkCompilerFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<LinkCompiler>()
                            .DerivesFrom<CompilerBase>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void op_ToArguments_IEnumerableOfString()
        {
            var obj = LinkCompiler.Current;
            obj.Out = new FileInfo(Path.Combine(Path.GetTempPath(), "Example.dll"));

            var files = new List<string>
            {
                "1.res",
                "2.res"
            };

            var expected = "-dll -noentry -machine:X86 -out:\"{0}\" 1.res 2.res".FormatWith(obj.Out.FullName);
            var actual = obj.ToArguments(files);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Current()
        {
            Assert.NotNull(LinkCompiler.Current);
        }

        [Fact]
        public void prop_Machine()
        {
            Assert.True(new PropertyExpectations<LinkCompiler>(p => p.Machine)
                            .TypeIs<string>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Out()
        {
            Assert.True(new PropertyExpectations<LinkCompiler>(p => p.Out)
                            .TypeIs<FileInfo>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}