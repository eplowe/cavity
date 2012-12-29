namespace Cavity.Models
{
    using System;

    using Xunit;
    using Xunit.Extensions;

    public sealed class BritishPostcodeFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<BritishPostcode>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .NoDefaultConstructor()
                            .IsNotDecorated()
                            .Implements<IComparable>()
                            .Implements<IComparable<BritishPostcode>>()
                            .Implements<IEquatable<BritishPostcode>>()
                            .Result);
        }

        [Theory]
        [InlineData(true, null, null)]
        [InlineData(true, "", "")]
        [InlineData(true, "GU21 4XG", "GU21 4XG")]
        [InlineData(false, null, "")]
        [InlineData(false, null, "GU21 4XG")]
        [InlineData(false, "", null)]
        [InlineData(false, "GU21 4XG", null)]
        [InlineData(false, "GU21 4XG", "G")]
        [InlineData(false, "GU21 4XG", "GU")]
        [InlineData(false, "GU21 4XG", "GU2")]
        [InlineData(false, "GU21 4XG", "GU21")]
        [InlineData(false, "GU21 4XG", "GU21 4")]
        [InlineData(false, "GU21 4XG", "GU21 4X")]
        [InlineData(false, "GU21 4XG", "GU21 4XH")]
        public void opEquality_BritishPostcode_BritishPostcode(bool expected,
                                                               string comparand1,
                                                               string comparand2)
        {
            BritishPostcode postcode1 = comparand1;
            BritishPostcode postcode2 = comparand2;

            var actual = postcode1 == postcode2;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(false, null, null)]
        [InlineData(false, "", "")]
        [InlineData(false, null, "")]
        [InlineData(false, null, "GU21 4XG")]
        [InlineData(true, "", null)]
        [InlineData(true, "GU21 4XG", null)]
        [InlineData(true, "GU21 4XG", "G")]
        [InlineData(true, "GU21 4XG", "GU")]
        [InlineData(true, "GU21 4XG", "GU2")]
        [InlineData(true, "GU21 4XG", "GU21")]
        [InlineData(true, "GU21 4XG", "GU21 4")]
        [InlineData(true, "GU21 4XG", "GU21 4X")]
        [InlineData(true, "GU21 4XG", "GU21 4XF")]
        [InlineData(false, "GU21 4XG", "GU21 4XG")]
        [InlineData(false, "GU21 4XG", "GU21 4XH")]
        public void opGreaterThan_BritishPostcode_BritishPostcode(bool expected,
                                                                  string comparand1,
                                                                  string comparand2)
        {
            BritishPostcode postcode1 = comparand1;
            BritishPostcode postcode2 = comparand2;

            var actual = postcode1 > postcode2;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_BritishPostcode_string()
        {
            BritishPostcode obj = "GU21 4XG";

            Assert.Equal("GU", obj.Area);
            Assert.Equal("GU21", obj.District);
            Assert.Equal("GU21 4", obj.Sector);
            Assert.Equal("GU21 4XG", obj.Unit);

            Assert.Equal("GU21", obj.OutCode);
            Assert.Equal("4XG", obj.InCode);
        }

        [Fact]
        public void opImplicit_BritishPostcode_stringEmpty()
        {
            BritishPostcode obj = string.Empty;

            Assert.Null(obj.Area);
            Assert.Null(obj.District);
            Assert.Null(obj.Sector);
            Assert.Null(obj.Unit);

            Assert.Null(obj.OutCode);
            Assert.Null(obj.InCode);
        }

        [Fact]
        public void opImplicit_BritishPostcode_stringNull()
        {
            Assert.Null((BritishPostcode)null);
        }

        [Fact]
        public void opImplicit_string_BritishPostcode()
        {
            const string expected = "GU21 4XG";
            string actual = BritishPostcode.FromString(expected);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(false, null, null)]
        [InlineData(false, "", "")]
        [InlineData(false, "GU21 4XG", "GU21 4XG")]
        [InlineData(true, null, "")]
        [InlineData(true, null, "GU21 4XG")]
        [InlineData(true, "", null)]
        [InlineData(true, "GU21 4XG", null)]
        [InlineData(true, "GU21 4XG", "G")]
        [InlineData(true, "GU21 4XG", "GU")]
        [InlineData(true, "GU21 4XG", "GU2")]
        [InlineData(true, "GU21 4XG", "GU21")]
        [InlineData(true, "GU21 4XG", "GU21 4")]
        [InlineData(true, "GU21 4XG", "GU21 4X")]
        [InlineData(true, "GU21 4XG", "GU21 4XH")]
        public void opInequality_BritishPostcode_BritishPostcode(bool expected,
                                                                 string comparand1,
                                                                 string comparand2)
        {
            BritishPostcode postcode1 = comparand1;
            BritishPostcode postcode2 = comparand2;

            var actual = postcode1 != postcode2;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(false, null, null)]
        [InlineData(false, "", "")]
        [InlineData(true, null, "")]
        [InlineData(true, null, "GU21 4XG")]
        [InlineData(false, "", null)]
        [InlineData(false, "GU21 4XG", null)]
        [InlineData(false, "GU21 4XG", "G")]
        [InlineData(false, "GU21 4XG", "GU")]
        [InlineData(false, "GU21 4XG", "GU2")]
        [InlineData(false, "GU21 4XG", "GU21")]
        [InlineData(false, "GU21 4XG", "GU21 4")]
        [InlineData(false, "GU21 4XG", "GU21 4X")]
        [InlineData(false, "GU21 4XG", "GU21 4XF")]
        [InlineData(false, "GU21 4XG", "GU21 4XG")]
        [InlineData(true, "GU21 4XG", "GU21 4XH")]
        public void opLessThan_BritishPostcode_BritishPostcode(bool expected,
                                                               string comparand1,
                                                               string comparand2)
        {
            BritishPostcode postcode1 = comparand1;
            BritishPostcode postcode2 = comparand2;

            var actual = postcode1 < postcode2;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(0, "GU21 4XG", "GU21 4XG")]
        [InlineData(-1, "GU21 4XG", "GU21 4XX")]
        [InlineData(1, "GU21 4XX", "GU21 4XG")]
        [InlineData(1, "GU21 4XX", null)]
        public void op_CompareTo_BritishPostcode(int expected,
                                                 string value,
                                                 string comparand)
        {
            BritishPostcode postcode1 = value;
            BritishPostcode postcode2 = comparand;

            Assert.Equal(expected, postcode1.CompareTo(postcode2));
            Assert.Equal(expected, postcode1.CompareTo((object)postcode2));
        }

        [Fact]
        public void op_CompareTo_object_whenString()
        {
            BritishPostcode obj = "GU21 4XG";

            Assert.Throws<ArgumentOutOfRangeException>(() => obj.CompareTo((object)"GU21 4XG"));
        }

        [Theory]
        [InlineData(0, null, null)]
        [InlineData(0, "GU", "GU")]
        [InlineData(0, "GU21", "GU21")]
        [InlineData(0, "GU21 4", "GU21 4")]
        [InlineData(0, "GU21 4XG", "GU21 4XG")]
        [InlineData(0, "WC", "WC")]
        [InlineData(0, "WC1", "WC1")]
        [InlineData(0, "WC1A", "WC1A")]
        [InlineData(0, "WC1A 2", "WC1A 2")]
        [InlineData(0, "WC1A 2HR", "WC1A 2HR")]
        [InlineData(-1, null, "GU21 4XG")]
        [InlineData(-1, "", "GU21 4XG")]
        [InlineData(-1, "GU21 4XG", "WC1A 2HR")]
        [InlineData(-1, "GU21 4XG", "WC")]
        [InlineData(-1, "GU21 4XG", "GU40 4XG")]
        [InlineData(-1, "GU", "GU21")]
        [InlineData(-1, "GU0", "GU21")]
        [InlineData(-1, "WC1 2HR", "WC1A 2HR")]
        [InlineData(-1, "WC1A 2HR", "WC1X 2HR")]
        [InlineData(-1, "GU21", "GU21 4XG")]
        [InlineData(-1, "GU21 4XG", "GU21 8XG")]
        [InlineData(-1, "GU21 4XG", "GU21 4XX")]
        [InlineData(1, "WC1A 2HR", null)]
        [InlineData(1, "WC1A 2HR", "")]
        [InlineData(1, "WC1A 2HR", "GU21 4XG")]
        [InlineData(1, "WC1A 2HR", "GU")]
        [InlineData(1, "GU40 4XG", "GU21 4XG")]
        [InlineData(1, "GU21", "GU")]
        [InlineData(1, "GU21", "GU0")]
        [InlineData(1, "WC1A 2HR", "WC1 2HR")]
        [InlineData(1, "WC1X 2HR", "WC1A 2HR")]
        [InlineData(1, "GU21 4XG", "GU21")]
        [InlineData(1, "GU21 8XG", "GU21 4XG")]
        [InlineData(1, "GU21 4XX", "GU21 4XG")]
        public void op_Compare_BritishPostcode_BritishPostcode(int expected,
                                                               string comparand1,
                                                               string comparand2)
        {
            BritishPostcode postcode1 = comparand1;
            BritishPostcode postcode2 = comparand2;

            var actual = BritishPostcode.Compare(postcode1, postcode2);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Compare_BritishPostcode_BritishPostcode_whenSame()
        {
            BritishPostcode obj = "GU21 4XG";

            const int expected = 0;
            var actual = BritishPostcode.Compare(obj, obj);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(true, "GU21 4XG", "GU21 4XG")]
        [InlineData(false, "GU21 4XG", "WC1A 2HR")]
        [InlineData(false, "GU21 4XG", null)]
        public void op_Equals_BritishPostcode(bool expected,
                                              string value,
                                              string comparand)
        {
            BritishPostcode obj = value;
            BritishPostcode other = comparand;

            Assert.Equal(expected, obj.Equals(other));
            Assert.Equal(expected, obj.Equals((object)other));
        }

        [Fact]
        public void op_Equals_BritishPostcode_whenSame()
        {
            BritishPostcode obj = "GU21 4XG";

            // ReSharper disable EqualExpressionComparison
            Assert.True(obj.Equals(obj));
            Assert.True(obj.Equals((object)obj));
            // ReSharper restore EqualExpressionComparison
        }

        [Fact]
        public void op_Equals_object_whenString()
        {
            BritishPostcode obj = "GU21 4XG";

            // ReSharper disable SuspiciousTypeConversion.Global
            Assert.False(obj.Equals((object)"GU21 4XG"));
            // ReSharper restore SuspiciousTypeConversion.Global
        }

        [Fact]
        public void op_FromString_string()
        {
            var obj = BritishPostcode.FromString("GU21 4XG");

            Assert.Equal("GU", obj.Area);
            Assert.Equal("GU21", obj.District);
            Assert.Equal("GU21 4", obj.Sector);
            Assert.Equal("GU21 4XG", obj.Unit);

            Assert.Equal("GU21", obj.OutCode);
            Assert.Equal("4XG", obj.InCode);
        }

        [Fact]
        public void op_FromString_stringEmpty()
        {
            var obj = BritishPostcode.FromString(string.Empty);

            Assert.Null(obj.Area);
            Assert.Null(obj.District);
            Assert.Null(obj.Sector);
            Assert.Null(obj.Unit);

            Assert.Null(obj.OutCode);
            Assert.Null(obj.InCode);
        }

        [Fact]
        public void op_FromString_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => BritishPostcode.FromString(null));
        }

        [Fact]
        public void op_FromString_string_WhenInvalidArea()
        {
            const string original = "0X 4XG";
            var obj = BritishPostcode.FromString(original);

            Assert.Null(obj.Area);
            Assert.Null(obj.District);
            Assert.Null(obj.Sector);
            Assert.Null(obj.Unit);

            Assert.Null(obj.OutCode);
            Assert.Null(obj.InCode);
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

            Assert.Equal("WC1A", obj.OutCode);
            Assert.Equal("2HR", obj.InCode);
        }

        [Fact]
        public void op_FromString_string_whenOnlyArea()
        {
            const string original = "GU";
            var obj = BritishPostcode.FromString(original);

            Assert.Equal("GU", obj.Area);
            Assert.Null(obj.District);
            Assert.Null(obj.Sector);
            Assert.Null(obj.Unit);

            Assert.Null(obj.OutCode);
            Assert.Null(obj.InCode);
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

            Assert.Equal("GU21", obj.OutCode);
            Assert.Null(obj.InCode);
        }

        [Fact]
        public void op_FromString_string_whenOnlySector()
        {
            const string original = "GU21 4";
            var obj = BritishPostcode.FromString(original);

            Assert.Equal("GU", obj.Area);
            Assert.Equal("GU21", obj.District);
            Assert.Equal("GU21 4", obj.Sector);
            Assert.Null(obj.Unit);

            Assert.Equal("GU21", obj.OutCode);
            Assert.Null(obj.InCode);
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
        public void op_FromString_string_whenPlaceName()
        {
            const string original = "ABERDEEN";
            var obj = BritishPostcode.FromString(original);

            Assert.Null(obj.Area);
            Assert.Null(obj.District);
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

        [Theory]
        [InlineData("")]
        [InlineData("WC1A 2HR")]
        public void op_GetHashCode(string value)
        {
            var expected = value.GetHashCode();
            var actual = BritishPostcode.FromString(value).GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData("G")]
        [InlineData("GU")]
        [InlineData("GU2")]
        [InlineData("GU21")]
        [InlineData("GU21 4")]
        [InlineData("GU21 4X")]
        [InlineData("GU21 4XG")]
        [InlineData("W")]
        [InlineData("WC")]
        [InlineData("WC1")]
        [InlineData("WC1A")]
        [InlineData("WC1A 2")]
        [InlineData("WC1A 2H")]
        [InlineData("WC1A 2HR")]
        public void op_ToString(string expected)
        {
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
        public void prop_InCode()
        {
            Assert.True(new PropertyExpectations<BritishPostcode>(p => p.InCode)
                            .TypeIs<string>()
                            .IsNotDecorated()
                            .Result);
        }

        [Fact]
        public void prop_OutCode()
        {
            Assert.True(new PropertyExpectations<BritishPostcode>(p => p.OutCode)
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