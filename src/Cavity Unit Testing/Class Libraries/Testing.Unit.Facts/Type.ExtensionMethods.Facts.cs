namespace Cavity
{
    using System;

    using Cavity.Types;

    using Xunit;

    public sealed class TypeExtensionMethodsFacts
    {
        [Fact]
        public void op_Implements_TypeNull_Type()
        {
            Assert.Throws<ArgumentNullException>(() => (null as Type).Implements(typeof(IInterface1)));
        }

        [Fact]
        public void op_Implements_Type_TypeNull()
        {
            Assert.Throws<ArgumentNullException>(() => typeof(object).Implements(null));
        }

        [Fact]
        public void op_Implements_Type_Type_whenFalse()
        {
            Assert.False(typeof(object).Implements(typeof(IInterface1)));
        }

        [Fact]
        public void op_Implements_Type_Type_whenTrue()
        {
            Assert.True(typeof(InterfacedClass1).Implements(typeof(IInterface1)));
        }

        [Fact]
        public void op_IsStatic_TypeNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as Type).IsStatic());
        }

        [Fact]
        public void op_IsStatic_Type_whenFalse()
        {
            Assert.False(typeof(object).IsStatic());
        }

        [Fact]
        public void op_IsStatic_Type_whenTrue()
        {
            Assert.True(typeof(StaticClass1).IsStatic());
        }
    }
}