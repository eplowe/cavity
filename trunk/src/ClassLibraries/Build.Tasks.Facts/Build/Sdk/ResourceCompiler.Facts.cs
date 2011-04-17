namespace Cavity.Build.Sdk
{
    using System.Collections.Generic;
    using Xunit;

    public sealed class ResourceCompilerFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<ResourceCompiler>()
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
            var files = new List<string>
            {
                "example.1",
                "example.2"
            };

            using (new FakePlatformSdk())
            {
                const string expected = "-r example.1 example.2";
                var actual = ResourceCompiler.Current.ToArguments(files);

                Assert.Equal(expected, actual);
            }
        }

        [Fact]
        public void prop_Current()
        {
            using (new FakePlatformSdk())
            {
                Assert.NotNull(ResourceCompiler.Current);
            }
        }
    }
}