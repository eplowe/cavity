namespace Cavity.Collections
{
    using Xunit;

    public sealed class PathItemFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<PathItem>()
                            .DerivesFrom<object>()
                            .IsAbstractBaseClass()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void op_ToString()
        {
            var obj = new DerivedPathItem
                          {
                              Name = "name",
                              Value = "value"
                          };

            const string expected = "name: value";
            var actual = obj.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Name()
        {
            Assert.True(new PropertyExpectations<PathItem>(x => x.Name)
                            .XmlAttribute("name")
                            .TypeIs<string>()
                            .Result);
        }

        [Fact]
        public void prop_Value()
        {
            Assert.True(new PropertyExpectations<PathItem>(x => x.Value)
                            .XmlAttribute("value")
                            .TypeIs<string>()
                            .Result);
        }
    }
}