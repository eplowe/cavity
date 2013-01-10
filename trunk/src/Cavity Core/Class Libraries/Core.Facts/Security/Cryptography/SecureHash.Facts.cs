namespace Cavity.Security.Cryptography
{
    using System;
    using System.ComponentModel;

    using Xunit;

    public sealed class SecureHashFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(new TypeExpectations<SecureHash>().DerivesFrom<object>()
                                                          .IsConcreteClass()
                                                          .IsSealed()
                                                          .NoDefaultConstructor()
                                                          .IsDecoratedWith<ImmutableObjectAttribute>()
                                                          .Result);
        }

        [Fact]
        public void ctor_string()
        {
            Assert.NotNull(new SecureHash("XbAH5ybjSkZt+Q=="));
        }

        [Fact]
        public void ctor_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new SecureHash(string.Empty));
        }

        [Fact]
        public void ctor_stringNull()
        {
            Assert.Throws<ArgumentNullException>(() => new SecureHash(null));
        }

        [Fact]
        public void opEquality_SecureHashNull_SecureHash()
        {
            Assert.False(null == SecureHash.Encrypt("plaintext", new Salt()));
        }

        [Fact]
        public void opEquality_SecureHash_SecureHashFalse()
        {
            var obj = new SecureHash("XbAH5ybjSkZt+Q==");
            SecureHash comparand = SecureHash.Encrypt("plaintext", new Salt());

            Assert.False(obj == comparand);
        }

        [Fact]
        public void opEquality_SecureHash_SecureHashNull()
        {
            Assert.False(SecureHash.Encrypt("plaintext", new Salt()) == null);
        }

        [Fact]
        public void opEquality_SecureHash_SecureHashSame()
        {
            var obj = new SecureHash("XbAH5ybjSkZt+Q==");
            SecureHash comparand = obj;

            Assert.True(obj == comparand);
        }

        [Fact]
        public void opEquality_SecureHash_SecureHashTrue()
        {
            const string value = "XbAH5ybjSkZt+Q==";

            var obj = new SecureHash(value);
            var comparand = new SecureHash(value);

            Assert.True(obj == comparand);
        }

        [Fact]
        public void opImplicit_SecureHash_string()
        {
            const string value = "XbAH5ybjSkZt+Q==";

            var expected = new SecureHash(value);
            SecureHash actual = value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opImplicit_SecureHash_stringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => (SecureHash)string.Empty);
        }

        [Fact]
        public void opImplicit_string_SecureHash()
        {
            const string expected = "XbAH5ybjSkZt+Q==";
            string actual = new SecureHash(expected);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void opInequality_SecureHashNull_SecureHash()
        {
            Assert.True(SecureHash.Encrypt("plaintext", new Salt()).IsNotNull());
        }

        [Fact]
        public void opInequality_SecureHash_SecureHashFalse()
        {
            const string value = "XbAH5ybjSkZt+Q==";

            var obj = new SecureHash(value);
            var comparand = new SecureHash(value);

            Assert.False(obj != comparand);
        }

        [Fact]
        public void opInequality_SecureHash_SecureHashNull()
        {
            Assert.True(SecureHash.Encrypt("plaintext", new Salt()).IsNotNull());
        }

        [Fact]
        public void opInequality_SecureHash_SecureHashSame()
        {
            var obj = new SecureHash("XbAH5ybjSkZt+Q==");
            var comparand = obj;

            Assert.False(obj != comparand);
        }

        [Fact]
        public void opInequality_SecureHash_SecureHashTrue()
        {
            var obj = new SecureHash("XbAH5ybjSkZt+Q==");
            SecureHash comparand = SecureHash.Encrypt("plaintext", new Salt());

            Assert.True(obj != comparand);
        }

        [Fact]
        public void op_Encrypt_stringEmpty_Salt()
        {
            Salt salt = "XbAH5ybjSkZt+Q==";

            var expected = new SecureHash("2uI+DajQR+GeYNTccNcDjoE5UtXbF6Jr+kVtE4vcNn4=");
            var actual = SecureHash.Encrypt(string.Empty, salt);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Encrypt_stringNull_Salt()
        {
            Assert.Throws<ArgumentNullException>(() => SecureHash.Encrypt(null, new Salt()));
        }

        [Fact]
        public void op_Encrypt_string_Salt()
        {
            Salt salt = "XbAH5ybjSkZt+Q==";

            var expected = new SecureHash("4bF4xJX9VVDr+xFMuOWS/9wJLAc7/ByDjW2PgPp5YLc=");
            var actual = SecureHash.Encrypt("plaintext", salt);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Encrypt_string_SaltNull()
        {
            Assert.Throws<ArgumentNullException>(() => SecureHash.Encrypt("plaintext", null));
        }

        [Fact]
        public void op_Equals_objectFalse()
        {
            var obj = SecureHash.Encrypt("plaintext", new Salt());
            var comparand = new SecureHash("XbAH5ybjSkZt+Q==");

            Assert.False(obj.Equals(comparand));
        }

        [Fact]
        public void op_Equals_objectNull()
        {
            Assert.False(SecureHash.Encrypt("plaintext", new Salt()).Equals(null));
        }

        [Fact]
        public void op_Equals_objectSame()
        {
            var obj = SecureHash.Encrypt("plaintext", new Salt());
            var comparand = obj;

            Assert.True(obj.Equals(comparand));
        }

        [Fact]
        public void op_Equals_objectString()
        {
            const string value = "XbAH5ybjSkZt+Q==";
            var obj = new SecureHash(value);

            Assert.False(obj.Equals(value));
        }

        [Fact]
        public void op_Equals_objectTrue()
        {
            const string value = "XbAH5ybjSkZt+Q==";
            var obj = new SecureHash(value);
            var comparand = new SecureHash(value);

            Assert.True(obj.Equals(comparand));
        }

        [Fact]
        public void op_GetHashCode()
        {
            const string value = "XbAH5ybjSkZt+Q==";

            var expected = value.GetHashCode();
            var actual = new SecureHash(value).GetHashCode();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_ToString()
        {
            const string expected = "XbAH5ybjSkZt+Q==";
            var actual = new SecureHash(expected).ToString();

            Assert.Equal(expected, actual);
        }
    }
}