namespace Cavity
{
    using System.Collections.Generic;
    using Xunit;

    public sealed class CommandLineFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<CommandLine>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .Result);
        }

        [Fact]
        public void indexer_string()
        {
            var args = new List<string>
                           {
                               "/name"
                           };
            var obj = CommandLine.Load(args);

            Assert.Equal(string.Empty, obj["name"]);
        }

        [Fact]
        public void indexer_stringMissing()
        {
            var args = new List<string>();
            var obj = CommandLine.Load(args);

            Assert.Null(obj["name"]);
        }

        [Fact]
        public void indexer_string_whenValue()
        {
            var args = new List<string>
                           {
                               "/name:value"
                           };
            var obj = CommandLine.Load(args);

            Assert.Equal("value", obj["name"]);
        }

        [Fact]
        public void op_Load_ICollectionOfString()
        {
            var obj = CommandLine.Load(new List<string>());

            Assert.False(obj.Help);
            Assert.False(obj.Info);
        }

        [Fact]
        public void op_Load_ICollectionOfStringNull()
        {
            var obj = CommandLine.Load(null);

            Assert.False(obj.Help);
            Assert.False(obj.Info);
        }

        [Fact]
        public void prop_Help()
        {
            Assert.True(new PropertyExpectations<CommandLine>(x => x.Help)
                            .TypeIs<bool>()
                            .DefaultValueIs(false)
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Info()
        {
            Assert.True(new PropertyExpectations<CommandLine>(x => x.Info)
                            .TypeIs<bool>()
                            .DefaultValueIs(false)
                            .IsNotDecorated()
                            .Result);
        }
    }
}