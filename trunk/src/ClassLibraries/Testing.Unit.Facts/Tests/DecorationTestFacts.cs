namespace Cavity.Tests
{
    using System;
    using System.Reflection;
    using Cavity.Fluent;
    using Cavity.Types;
    using Xunit;

    public class DecorationTestFacts
    {
        [Fact]
        public void is_ITestExpectation()
        {
            Assert.IsAssignableFrom<ITestExpectation>(new DecorationTest(typeof(object), typeof(Attribute1)));
        }

        [Fact]
        public void ctor_MemberInfo_Type()
        {
            Assert.NotNull(new DecorationTest(typeof(object), typeof(Attribute1)));
        }

        [Fact]
        public void ctor_MemberInfoNull_Type()
        {
            Assert.Throws<ArgumentNullException>(() => new DecorationTest(null as MethodInfo, typeof(Attribute1)));
        }

        [Fact]
        public void ctor_MemberInfo_TypeNull()
        {
            Assert.NotNull(new DecorationTest(typeof(object), null as Type));
        }

        [Fact]
        public void prop_Attribute()
        {
            var obj = new DecorationTest(typeof(object), null as Type);

            Type expected = typeof(Attribute1);
            obj.Attribute = expected;
            Type actual = obj.Attribute;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void prop_MemberInfo()
        {
            var obj = new DecorationTest(typeof(int), typeof(Attribute1));

            MemberInfo expected = typeof(object);
            obj.MemberInfo = expected;
            MemberInfo actual = obj.MemberInfo;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void op_Check_whenFalse()
        {
            Assert.Throws<TestException>(() => new DecorationTest(typeof(object), typeof(Attribute1)).Check());
        }

        [Fact]
        public void op_Check_whenTrue()
        {
            Assert.True(new DecorationTest(typeof(AttributedClass1), typeof(Attribute1)).Check());
        }
    }
}