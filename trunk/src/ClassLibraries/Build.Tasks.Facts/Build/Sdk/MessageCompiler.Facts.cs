﻿namespace Cavity.Build.Sdk
{
    using System.Collections.Generic;
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
        public void op_ToArguments_IEnumerableOfString()
        {
            var files = new List<string>
            {
                "example.1",
                "example.2"
            };

            using (new FakePlatformSdk())
            {
                const string expected = "-u -U example.1 example.2";
                var actual = MessageCompiler.Current.ToArguments(files);

                Assert.Equal(expected, actual);
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
    }
}