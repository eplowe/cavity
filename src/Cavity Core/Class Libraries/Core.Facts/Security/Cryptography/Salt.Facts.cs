namespace Cavity.Security.Cryptography
{
    using System;

    using Xunit;

    public sealed class SaltFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<Salt>()
                            .DerivesFrom<object>()
                            .IsConcreteClass()
                            .IsSealed()
                            .HasDefaultConstructor()
                            .Result);
        }

        [Fact]
        public void ctor()
        {
            Assert.NotNull(new Salt());
        }

        [Fact]
        public void ctor_int()
        {
            Assert.NotNull(new Salt(10));
        }

        [Fact]
        public void ctor_intZero()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Salt(0));
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new Salt("XbAH5ybjSkZt+Q=="));
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new Salt(string.Empty));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new Salt(null));
        }

        [Fact]
        public void opEquality_Salt_SaltFalse()
        {
            var obj = new Salt("XbAH5ybjSkZt+Q==");
            var comparand = new Salt();

            Assert.False(obj == comparand);
        }

        [Fact]
        public void opEquality_Salt_SaltSame()
        {
            var obj = new Salt("XbAH5ybjSkZt+Q==");
            var comparand = obj;

            Assert.True(obj == comparand);
        }

        [Fact]
        public void opEquality_Salt_SaltTrue()
        {
            const string value = "XbAH5ybjSkZt+Q==";

            var obj = new Salt(value);
            var comparand = new Salt(value);

            Assert.True(obj == comparand);
        }

        [Fact]
        public void opImplicit_Salt_string()
        {
            const string value = "XbAH5ybjSkZt+Q==";

            var expected = new Salt(value);
            Salt actual = value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_Salt_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => (Salt)string.Empty);
        }

        [Fact]
        public void opImplicit_string_Salt()
        {
            const string expected = "XbAH5ybjSkZt+Q==";
            string actual = new Salt(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_string_SaltNull()
        {
            Assert.Null(null as Salt);
        }

        [Fact]
        public void opInequality_Salt_SaltFalse()
        {
            const string value = "XbAH5ybjSkZt+Q==";

            var obj = new Salt(value);
            var comparand = new Salt(value);

            Assert.False(obj != comparand);
        }

        [Fact]
        public void opInequality_Salt_SaltSame()
        {
            var obj = new Salt("XbAH5ybjSkZt+Q==");
            var comparand = obj;

            Assert.False(obj != comparand);
        }

        [Fact]
        public void opInequality_Salt_SaltTrue()
        {
            var obj = new Salt("XbAH5ybjSkZt+Q==");
            var comparand = new Salt();

            Assert.True(obj != comparand);
        }

        [Fact]
        public void op_Equals_objectFalse()
        {
            var obj = new Salt();
            var comparand = new Salt("XbAH5ybjSkZt+Q==");

            Assert.False(obj.Equals(comparand));
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(new Salt().Equals(null));
        }

        [Fact]
        public void op_Equals_objectSame()
        {
            var obj = new Salt();
            var comparand = obj;

            Assert.True(obj.Equals(comparand));
        }

        [Fact]
        public void op_Equals_objectString()
        {
            const string value = "XbAH5ybjSkZt+Q==";
            var obj = new Salt(value);

            Assert.False(obj.Equals(value));
        }

        [Fact]
        public void op_Equals_objectTrue()
        {
            const string value = "XbAH5ybjSkZt+Q==";
            var obj = new Salt(value);
            var comparand = new Salt(value);

            Assert.True(obj.Equals(comparand));
        }

        [Fact]
        public void op_GetHashCode()
        {
            const string value = "XbAH5ybjSkZt+Q==";

            var expected = value.GetHashCode();
            var actual = new Salt(value).GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToBytes()
        {
            Assert.NotNull(new Salt("XbAH5ybjSkZt+Q==").ToBytes());
        }

        [Fact]
        public void op_ToString()
        {
            const string expected = "XbAH5ybjSkZt+Q==";
            var actual = new Salt(expected).ToString();

            Assert.Equal(expected, actual);
        }
    }
}