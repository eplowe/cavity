namespace Cavity.Tests
{
    using System;
    using Cavity.Types;
    using Xunit;

    public sealed class AttributeUsageTestFacts
    {
        [Fact]
        public void ctor_MemberInfoNull_AttributeTargets_bool()
        {
            Assert.Throws<ArgumentNullException>(() => new AttributeUsageTest(null, AttributeTargets.Class, true, true));
        }

        [Fact]
        public void ctor_MemberInfo_AttributeTargets_bool()
        {
            Assert.NotNull(new AttributeUsageTest(typeof(object), AttributeTargets.Class, true, true));
        }

        [Fact]
        public void is_AttributePropertyTest()
        {
            Assert.IsAssignableFrom<MemberTestBase>(new AttributeUsageTest(typeof(object), AttributeTargets.Class, true, true));
        }

        [Fact]
        public void op_Check()
        {
            Assert.True(new AttributeUsageTest(typeof(Attribute1Attribute), AttributeTargets.All, false, true).Check());
        }

        [Fact]
        public void op_Check_whenUndecorated()
        {
            Assert.Throws<UnitTestException>(() => new AttributeUsageTest(typeof(Class1), AttributeTargets.Class, true, true).Check());
        }

        [Fact]
        public void op_Check_whenUnexpectedAllowMultiple()
        {
            Assert.Throws<UnitTestException>(() => new AttributeUsageTest(typeof(Attribute1Attribute), AttributeTargets.All, true, false).Check());
        }

        [Fact]
        public void op_Check_whenUnexpectedValidOn()
        {
            Assert.Throws<UnitTestException>(() => new AttributeUsageTest(typeof(Attribute1Attribute), AttributeTargets.Class, false, false).Check());
        }

        [Fact]
        public void prop_AllowMultiple()
        {
            var obj = new AttributeUsageTest(typeof(object), AttributeTargets.Class, true, false);

            Assert.True(obj.AllowMultiple);
        }

        [Fact]
        public void prop_Inherited()
        {
            var obj = new AttributeUsageTest(typeof(object), AttributeTargets.Class, false, true);

            Assert.True(obj.Inherited);
        }

        [Fact]
        public void prop_ValidOn()
        {
            const AttributeTargets expected = AttributeTargets.Class;

            var obj = new AttributeUsageTest(typeof(object), expected, true, true);

            var actual = obj.ValidOn;

            Assert.Equal(expected, actual);
        }
    }
}