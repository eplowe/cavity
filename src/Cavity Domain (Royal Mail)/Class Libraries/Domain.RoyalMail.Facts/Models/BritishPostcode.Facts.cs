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
            Assert.Null((BritishPostcode)null);
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
        public void op_FromString_string_whenPeriod()
        {
            const string original = ".AB10 1AA.";
            var obj = BritishPostcode.FromString(original);

            Assert.Equal("AB", obj.Area);
            Assert.Equal("AB10", obj.District);
            Assert.Equal("AB10 1", obj.Sector);
            Assert.Equal("AB10 1AA", obj.Unit);
        }

        [Fact]
        public void op_FromString_string_whenLondonWC()
        {
            const string original = "WC1A 2HR";
            var obj = BritishPostcode.FromString(original);

            Assert.Equal("WC", obj.Area);
            Assert.Equal("WC1A", obj.District);
            Assert.Equal("WC1A 2", obj.Sector);
            Assert.Equal("WC1A 2HR", obj.Unit);
        }

        [Fact]
        public void op_FromString_string_whenOnlyDistrict()
        {
            const string original = "GU21";
            var obj = BritishPostcode.FromString(original);

            Assert.Equal("GU", obj.Area);
            Assert.Equal("GU21", obj.District);
            Assert.Null(obj.Sector);
            Assert.Null(obj.Unit);
        }

        [Fact]
        public void op_FromString_string_whenThreeParts()
        {
            const string original = "GU21 4XG BB";
            var obj = BritishPostcode.FromString(original);

            Assert.Null(obj.Area);
            Assert.Null(obj.District);
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
            Assert.True(new PropertyExpectations<BritishPostcode>(p => p.Area)
                            .TypeIs<string>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_District()
        {
            Assert.True(new PropertyExpectations<BritishPostcode>(p => p.District)
                            .TypeIs<string>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Sector()
        {
            Assert.True(new PropertyExpectations<BritishPostcode>(p => p.Sector)
                            .TypeIs<string>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_Unit()
        {
            Assert.True(new PropertyExpectations<BritishPostcode>(p => p.Unit)
                            .TypeIs<string>()
                            .IsNotDecorated()
                            .Result);
        }
    }
}