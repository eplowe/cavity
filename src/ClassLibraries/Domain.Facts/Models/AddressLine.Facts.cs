namespace Cavity.Models
{
    using Xunit;

    public sealed class AddressLineFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<AddressLine>()
                            .DerivesFrom<object>()
                            .IsAbstractBaseClass()
                            .IsNotDecorated()
                            .Implements<IAddressLine>()
                            .Result);
        }

        [Fact]
        public void prop_Original()
        {
            Assert.NotNull(new PropertyExpectations<AddressLine>(x => x.Original)
                               .TypeIs<string>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_Value()
        {
            Assert.NotNull(new PropertyExpectations<AddressLine>(x => x.Value)
#if NET40
                               .TypeIs<dynamic>()
#else
                               .TypeIs<object>()
#endif
                               .Result);
        }
    }
}