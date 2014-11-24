namespace Cavity
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Xunit;
    using Xunit.Extensions;

    public sealed class GenericExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(GenericExtensionMethods).IsStatic());
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ts", Justification = "This is spelling is intended.")]
        public void op_EqualsNoneOf_TNull_Ts()
        {
            Assert.True((null as string).EqualsNoneOf("xyz"));
        }

        [Fact]
        public void op_EqualsNoneOf_T_TNull()
        {
            Assert.Throws<ArgumentNullException>(() => "abc".EqualsNoneOf(null));
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ts", Justification = "This is spelling is intended.")]
        public void op_EqualsNoneOf_T_Ts()
        {
            Assert.False(123.EqualsNoneOf(123, 456));
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ts", Justification = "This is spelling is intended.")]
        public void op_EqualsNoneOf_T_Ts_whenTrue()
        {
            Assert.True(1.EqualsNoneOf(2, 3));
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ts", Justification = "This is spelling is intended.")]
        public void op_EqualsOneOf_TNull_Ts()
        {
            Assert.False((null as string).EqualsOneOf("xyz"));
        }

        [Fact]
        public void op_EqualsOneOf_T_TNull()
        {
            Assert.Throws<ArgumentNullException>(() => "abc".EqualsOneOf(null));
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ts", Justification = "This is spelling is intended.")]
        public void op_EqualsOneOf_T_Ts()
        {
            Assert.True(123.EqualsOneOf(123, 456));
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ts", Justification = "This is spelling is intended.")]
        public void op_EqualsOneOf_T_Ts_whenFalse()
        {
            Assert.False(1.EqualsOneOf(2, 3));
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ts", Justification = "This is spelling is intended.")]
        public void op_EqualsOneOf_T_Ts_whenStringFalse()
        {
            Assert.False("abc".EqualsOneOf("xyz"));
        }

        [Fact]
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Ts", Justification = "This is spelling is intended.")]
        public void op_EqualsOneOf_T_Ts_whenStringTrue()
        {
            Assert.True("abc".EqualsOneOf("abc"));
        }

        [Fact]
        public void op_In_charA_charsABC()
        {
            Assert.True('A'.In('A', 'B', 'C'));
        }

        [Fact]
        public void op_In_charZ_charsABC()
        {
            Assert.False('Z'.In('A', 'B', 'C'));
        }

        [Fact]
        public void op_In_charZ_charsNull()
        {
            Assert.False('Z'.In());
        }

        [Fact]
        public void op_IsBoundedBy_TNull_T_T()
        {
            Assert.False((null as string).IsBoundedBy("a", "c"));
        }

        [Fact]
        public void op_IsBoundedBy_T_TNull_T()
        {
            Assert.True("b".IsBoundedBy(null, "c"));
        }

        [Fact]
        public void op_IsBoundedBy_T_T_T()
        {
            Assert.True(1.IsBoundedBy(1, 3));
            Assert.True(2.IsBoundedBy(1, 3));
            Assert.True(3.IsBoundedBy(1, 3));
        }

        [Fact]
        public void op_IsBoundedBy_T_T_TNull()
        {
            Assert.Throws<ArgumentNullException>(() => "b".IsBoundedBy("a", null));
        }

        [Fact]
        public void op_IsBoundedBy_T_T_T_whenFalse()
        {
            Assert.False(3.IsBoundedBy(1, 2));
        }

        [Fact]
        public void op_IsBoundedBy_T_T_T_whenLower()
        {
            Assert.True(1.IsBoundedBy(1, 2));
        }

        [Fact]
        public void op_IsBoundedBy_T_T_T_whenLowerGreaterThanUpper()
        {
            Assert.Throws<ArgumentException>(() => 2.IsBoundedBy(3, 1));
        }

        [Fact]
        public void op_IsBoundedBy_T_T_T_whenSameBounds()
        {
            Assert.Throws<ArgumentException>(() => 1.IsBoundedBy(1, 1));
        }

        [Fact]
        public void op_IsBoundedBy_T_T_T_whenString()
        {
            Assert.True("b".IsBoundedBy("a", "c"));
        }

        [Fact]
        public void op_IsBoundedBy_T_T_T_whenUpper()
        {
            Assert.True(2.IsBoundedBy(1, 2));
        }

        [Fact]
        public void op_IsGreaterThan_T_T()
        {
            Assert.True(123.IsGreaterThan(0));
            Assert.False(123.IsGreaterThan(123));
            Assert.False(123.IsGreaterThan(456));
        }

        [Fact]
        public void op_IsLessThan_T_T()
        {
            Assert.False(123.IsLessThan(0));
            Assert.False(123.IsLessThan(123));
            Assert.True(123.IsLessThan(456));
        }

        [Fact]
        public void op_IsNotBoundedBy_T_T_T_whenFalse()
        {
            Assert.True(3.IsNotBoundedBy(1, 2));
        }

        [Fact]
        public void op_IsNotBoundedBy_T_T_T_whenLower()
        {
            Assert.False(1.IsNotBoundedBy(1, 2));
        }

        [Fact]
        public void op_IsNot_T_T()
        {
            Assert.False(123.IsNot(123));
            Assert.True(123.IsNot(456));
        }

        [Theory]
        [InlineData(false, null, null)]
        [InlineData(true, null, "example")]
        [InlineData(true, "example", null)]
        [InlineData(false, "example", "example")]
        [InlineData(true, "example", "EXAMPLE")]
        public void op_IsNot_T_T_whenString(bool expected,
                                            string value,
                                            string comparand)
        {
            var actual = value.IsNot(comparand);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_Is_T_T()
        {
            Assert.True(123.Is(123));
            Assert.False(123.Is(456));
        }

        [Theory]
        [InlineData(true, null, null)]
        [InlineData(false, null, "example")]
        [InlineData(false, "example", null)]
        [InlineData(true, "example", "example")]
        [InlineData(false, "example", "EXAMPLE")]
        public void op_Is_T_T_whenString(bool expected,
                                         string value,
                                         string comparand)
        {
            var actual = value.Is(comparand);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void op_NotIn_charA_charsABC()
        {
            Assert.False('A'.NotIn('A', 'B', 'C'));
        }

        [Fact]
        public void op_NotIn_charZ_charsABC()
        {
            Assert.True('Z'.NotIn('A', 'B', 'C'));
        }

        [Fact]
        public void op_NotIn_charZ_charsNull()
        {
            Assert.True('Z'.NotIn());
        }
    }
}