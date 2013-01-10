namespace Cavity
{
    using System;

    using Xunit;

    public sealed class IssueAttributeFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<IssueAttribute>().DerivesFrom<Attribute>()
                                                              .IsConcreteClass()
                                                              .IsSealed()
                                                              .NoDefaultConstructor()
                                                              .AttributeUsage(AttributeTargets.All, true, true)
                                                              .Result);
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new IssueAttribute("Example"));
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.NotNull(new IssueAttribute(string.Empty));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.NotNull(new IssueAttribute(null));
        }

        [Fact]
        public void prop_Description()
        {
            Assert.True(new PropertyExpectations<IssueAttribute>(x => x.Description)
                            .IsAutoProperty<string>()
                            .Result);
        }

        [Fact]
        public void prop_Reference()
        {
            Assert.True(new PropertyExpectations<IssueAttribute>(x => x.Reference)
                            .IsAutoProperty<string>()
                            .Result);
        }

        [Issue("A description.", Reference = "#1234")]
        public void usage()
        {
        }
    }
}