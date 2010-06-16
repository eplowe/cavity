namespace Cavity.Tests
{
    using System;
    using System.Reflection;
    using Cavity.Types;
    using Xunit;

    public sealed class AttributeMemberTestFacts
    {
        [Fact]
        public void is_AttributePropertyTest()
        {
            Assert.IsAssignableFrom<MemberTestBase>(new AttributeMemberTest(typeof(object), typeof(Attribute1)));
        }

        [Fact]
        public void ctor_MemberInfo_Type()
        {
            Assert.NotNull(new AttributeMemberTest(typeof(object), typeof(Attribute1)));
        }

        [Fact]
        public void ctor_MemberInfoNull_Type()
        {
            Assert.Throws<ArgumentNullException>(() => new AttributeMemberTest(null as MethodInfo, typeof(Attribute1)));
        }

        [Fact]
        public void ctor_MemberInfo_TypeNull()
        {
            Assert.NotNull(new AttributeMemberTest(typeof(object), null as Type));
        }

        [Fact]
        public void prop_Attribute()
        {
            var obj = new AttributeMemberTest(typeof(object), null as Type);

            Type expected = typeof(Attribute1);
            obj.Attribute = expected;
            Type actual = obj.Attribute;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void op_Check_whenFalse()
        {
            Assert.Throws<TestException>(() => new AttributeMemberTest(typeof(object), typeof(Attribute1)).Check());
        }

        [Fact]
        public void op_Check_whenUnexpectedAttribute()
        {
            Assert.Throws<TestException>(() => new AttributeMemberTest(typeof(AttributedClass1), null as Type).Check());
        }

        [Fact]
        public void op_Check_whenTrue()
        {
            Assert.True(new AttributeMemberTest(typeof(AttributedClass1), typeof(Attribute1)).Check());
        }
    }
}