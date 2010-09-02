namespace Cavity.Models
{
    using System;
    using Xunit;

    public sealed class BritishPostcodeFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<BritishPostcode>()
                            .DerivesFrom<ComparableObject>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void opImplicit_BritishPostcode_string()
        {
            BritishPostcode obj = "GU21 4XG";

            Assert.Equal("GU", obj.Area);
            Assert.Equal("GU21", obj.District);
            Assert.Equal("GU21 4", obj.Sector);
            Assert.Equal("GU21 4XG", obj.Unit);
        }

        [Fact]
        public void opImplicit_BritishPostcode_stringEmpty()
        {
            BritishPostcode obj = string.Empty;

            Assert.Null(obj.Unit);
        }

        [Fact]
        public void opImplicit_BritishPostcode_stringNull()
        {
            BritishPostcode obj = null as string;

            Assert.Null(obj);
        }

        [Fact]
        public void op_FromString_string()
        {
            var obj = BritishPostcode.FromString("GU21 4XG");

            Assert.Equal("GU", obj.Area);
            Assert.Equal("GU21", obj.District);
            Assert.Equal("GU21 4", obj.Sector);
            Assert.Equal("GU21 4XG", obj.Unit);
        }

        [Fact]
        public void op_FromString_stringEmpty()
        {
            Assert.Null(BritishPostcode.FromString(string.Empty).Unit);
        }

        [Fact]
        public void op_FromString_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => BritishPostcode.FromString(null));
        }

        [Fact]
        public void op_FromString_string_whenLondonWC()
        {
            var obj = BritishPostcode.FromString("WC1A 2HR");

            Assert.Equal("WC", obj.Area);
            Assert.Equal("WC1A", obj.District);
            Assert.Equal("WC1A 2", obj.Sector);
            Assert.Equal("WC1A 2HR", obj.Unit);
        }

        [Fact]
        public void op_FromString_string_whenOnlyDistrict()
        {
            var obj = BritishPostcode.FromString("GU21");

            Assert.Equal("GU", obj.Area);
            Assert.Equal("GU21", obj.District);
            Assert.Null(obj.Sector);
            Assert.Null(obj.Unit);
        }

        [Fact]
        public void op_ToString()
        {
            const string expected = "GU21 4XG";
            var actual = BritishPostcode.FromString(expected).ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Area()
        {
            Assert.NotNull(new PropertyExpectations<BritishPostcode>("Area")
                               .TypeIs<string>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_AreaName()
        {
            Assert.NotNull(new PropertyExpectations<BritishPostcode>("AreaName")
                               .TypeIs<string>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_AreaName_get()
        {
            Assert.Equal("Guildford", BritishPostcode.FromString("GU21").AreaName);
        }

        [Fact]
        public void prop_AreaName_getZZ()
        {
            Assert.Null(BritishPostcode.FromString("ZZ99").AreaName);
        }

        [Fact]
        public void prop_District()
        {
            Assert.NotNull(new PropertyExpectations<BritishPostcode>("District")
                               .TypeIs<string>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_Sector()
        {
            Assert.NotNull(new PropertyExpectations<BritishPostcode>("Sector")
                               .TypeIs<string>()
                               .IsNotDecorated()
                               .Result);
        }

        [Fact]
        public void prop_Unit()
        {
            Assert.NotNull(new PropertyExpectations<BritishPostcode>("Unit")
                               .TypeIs<string>()
                               .IsNotDecorated()
                               .Result);
        }
    }
}