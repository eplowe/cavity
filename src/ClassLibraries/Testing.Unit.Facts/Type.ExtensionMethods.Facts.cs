namespace Cavity
{
    using System;
    using Cavity.Types;
    using Xunit;

    public class TypeExtensionMethodsFacts
    {
        [Fact]
        public void op_Implements_TypeNull_Type()
        {
            Assert.Throws<ArgumentNullException>(() => (null as Type).Implements(typeof(Interface1)));
        }

        [Fact]
        public void op_Implements_Type_TypeNull()
        {
            Assert.Throws<ArgumentNullException>(() => typeof(object).Implements(null as Type));
        }

        [Fact]
        public void op_Implements_Type_Type_whenFalse()
        {
            Assert.False(typeof(object).Implements(typeof(Interface1)));
        }

        [Fact]
        public void op_Implements_Type_Type_whenTrue()
        {
            Assert.True(typeof(InterfaceClass1).Implements(typeof(Interface1)));
        }
    }
}