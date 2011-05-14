namespace Cavity
{
    using Xunit;

    public sealed class CommandFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Command>()
                            .DerivesFrom<object>()
                            .IsAbstractBaseClass()
                            .IsNotDecorated()
                            .Implements<ICommand>()
                            .Result);
        }

        [Fact]
        public void prop_Undo()
        {
            Assert.True(new PropertyExpectations<Command>(p => p.Undo)
                            .TypeIs<bool>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Unidirectional()
        {
            Assert.True(new PropertyExpectations<Command>(p => p.Unidirectional)
                            .TypeIs<bool>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}