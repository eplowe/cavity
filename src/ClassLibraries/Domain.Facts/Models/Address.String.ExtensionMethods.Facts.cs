namespace Cavity.Models
{
    using System;
    using Xunit;

    public sealed class AddressStringExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(AddressStringExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_ExtractFlatNumber_string()
        {
            var expected = string.Empty;
            var actual = "High Street".ExtractFlatNumber();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ExtractFlatNumber_string123()
        {
            var expected = string.Empty;
            var actual = "123".ExtractFlatNumber();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ExtractFlatNumber_stringEmpty()
        {
            var expected = string.Empty;
            var actual = string.Empty.ExtractFlatNumber();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ExtractFlatNumber_stringFlat1()
        {
            const string expected = "Flat 1";
            var actual = expected.ExtractFlatNumber();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ExtractFlatNumber_stringFlat1TallBuildings()
        {
            const string expected = "Flat 1";
            var actual = "Flat 1 Tall Buildings".ExtractFlatNumber();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ExtractFlatNumber_stringFlat1aTallBuildings()
        {
            const string expected = "Flat 1a";
            var actual = "Flat 1a Tall Buildings".ExtractFlatNumber();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ExtractFlatNumber_stringFlataTallBuildings()
        {
            const string expected = "Flat a";
            var actual = "Flat a Tall Buildings".ExtractFlatNumber();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ExtractFlatNumber_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as string).ExtractFlatNumber());
        }

        [Fact]
        public void op_ExtractHouseNumber_string()
        {
            var expected = string.Empty;
            var actual = "High Street".ExtractHouseNumber();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ExtractHouseNumber_string123()
        {
            const string expected = "123";
            var actual = expected.ExtractHouseNumber();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ExtractHouseNumber_string123HighStreet()
        {
            const string expected = "123";
            var actual = "123 High Street".ExtractHouseNumber();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ExtractHouseNumber_string123a()
        {
            const string expected = "123a";
            var actual = expected.ExtractHouseNumber();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ExtractHouseNumber_string123aHighStreet()
        {
            const string expected = "123a";
            var actual = "123a High Street".ExtractHouseNumber();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ExtractHouseNumber_string123dash456HighStreet()
        {
            const string expected = "123…456";
            var actual = "123-456 High Street".ExtractHouseNumber();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ExtractHouseNumber_string123slash456HighStreet()
        {
            const string expected = "123…456";
            var actual = "123/456 High Street".ExtractHouseNumber();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ExtractHouseNumber_stringB12HighStreet()
        {
            const string expected = "B12";
            var actual = "B12 High Street".ExtractHouseNumber();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ExtractHouseNumber_stringB1HighStreet()
        {
            const string expected = "B1";
            var actual = "B1 High Street".ExtractHouseNumber();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ExtractHouseNumber_stringEmpty()
        {
            var expected = string.Empty;
            var actual = string.Empty.ExtractHouseNumber();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ExtractHouseNumber_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as string).ExtractHouseNumber());
        }
    }
}