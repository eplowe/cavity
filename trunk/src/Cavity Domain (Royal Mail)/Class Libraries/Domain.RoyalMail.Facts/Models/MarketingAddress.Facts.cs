namespace Cavity.Models
{
    using Cavity.Collections;
    using Xunit;
    using Xunit.Extensions;

    public sealed class MarketingAddressFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<MarketingAddress>()
                            .DerivesFrom<KeyStringDictionary>()
                            .IsConcreteClass()
                            .IsUnsealed()
                            .HasDefaultConstructor()
                            .Serializable()
                            .Result);
        }

        [Theory]
        [InlineData("", "", "", "", "", "", "", "")]
        [InlineData("Example", "", "", "", "", "", "", "Example")]
        [InlineData("", "Example", "", "", "", "", "", "Example")]
        [InlineData("", "", "Example", "", "", "", "", "Example")]
        [InlineData("", "", "", "Example", "", "", "", "Example")]
        [InlineData("", "", "", "", "Example", "", "", "Example")]
        [InlineData("", "", "", "", "", "Example", "", "Example")]
        [InlineData("", "", "", "", "", "", "AB1 2ZZ", "AB1 2ZZ")]
        [InlineData("Flat 1", "23 High Street", "ABERDEEN", "", "", "", "AB1 2ZZ", "Flat 1, 23 High Street, ABERDEEN, AB1 2ZZ")]
        [InlineData("Flat 1", "", "", "23 High Street", "", "ABERDEEN", "AB1 2ZZ", "Flat 1, 23 High Street, ABERDEEN, AB1 2ZZ")]
        [InlineData("Flat 1,1A", "", "", "23 High Street", "", "ABERDEEN", "AB1 2ZZ", "Flat 1,1A, 23 High Street, ABERDEEN, AB1 2ZZ")]
        public void op_ToString(string address1, string address2, string address3, string address4, string address5, string address6, string postcode, string expected)
        {
            var obj = new MarketingAddress
                {
                    Address1 = address1,
                    Address2 = address2,
                    Address3 = address3,
                    Address4 = address4,
                    Address5 = address5,
                    Address6 = address6,
                    Postcode = postcode
                };

            var actual = obj.FullAddress;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_FullAddress_whenEmpty()
        {
            Assert.Empty(new MarketingAddress().FullAddress);
        }

        [Fact]
        public void prop_Address1()
        {
            Assert.NotNull(new PropertyExpectations<MarketingAddress>(x => x.Address1)
                               .IsAutoProperty<string>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_Address2()
        {
            Assert.NotNull(new PropertyExpectations<MarketingAddress>(x => x.Address2)
                               .IsAutoProperty<string>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_Address3()
        {
            Assert.NotNull(new PropertyExpectations<MarketingAddress>(x => x.Address3)
                               .IsAutoProperty<string>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_Address4()
        {
            Assert.NotNull(new PropertyExpectations<MarketingAddress>(x => x.Address4)
                               .IsAutoProperty<string>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_Address5()
        {
            Assert.NotNull(new PropertyExpectations<MarketingAddress>(x => x.Address5)
                               .IsAutoProperty<string>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_Address6()
        {
            Assert.NotNull(new PropertyExpectations<MarketingAddress>(x => x.Address6)
                               .IsAutoProperty<string>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_FullAddress()
        {
            Assert.NotNull(new PropertyExpectations<MarketingAddress>(x => x.FullAddress)
                               .TypeIs<string>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_Postcode()
        {
            Assert.NotNull(new PropertyExpectations<MarketingAddress>(x => x.Postcode)
                               .IsAutoProperty<BritishPostcode>()
                               .IsNotDecorated()
                               .Result);
        }
    }
}