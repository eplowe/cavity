namespace Cavity
{
    using System;

    using Xunit;

    public sealed class GenericExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(GenericExtensionMethods).IsStatic());
        }

        [Fact]
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
        public void op_EqualsOneOf_T_Ts()
        {
            Assert.True(123.EqualsOneOf(123, 456));
        }

        [Fact]
        public void op_EqualsOneOf_T_Ts_whenFalse()
        {
            Assert.False(1.EqualsOneOf(2, 3));
        }

        [Fact]
        public void op_EqualsOneOf_T_Ts_whenStringFalse()
        {
            Assert.False("abc".EqualsOneOf("xyz"));
        }

        [Fact]
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
            Assert.True(2.IsBoundedBy(1, 3));
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
    }
}