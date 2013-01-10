namespace Cavity
{
    using System;

    using Xunit;

    public sealed class CommentAttributeFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<CommentAttribute>().DerivesFrom<Attribute>()
                                                                .IsConcreteClass()
                                                                .IsSealed()
                                                                .NoDefaultConstructor()
                                                                .AttributeUsage(AttributeTargets.All, true, true)
                                                                .Result);
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new CommentAttribute("Example"));
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.NotNull(new CommentAttribute(string.Empty));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.NotNull(new CommentAttribute(null));
        }

        [Fact]
        public void prop_Value()
        {
            Assert.True(new PropertyExpectations<CommentAttribute>(x => x.Value)
                            .IsAutoProperty<string>()
                            .Result);
        }

        [Comment("An example.")]
        public void usage()
        {
        }
    }
}