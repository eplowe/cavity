namespace Cavity.Tests
{
    using System;
    using System.Reflection;

    using Cavity.Types;

    using Xunit;

    public sealed class MemberTestBaseFacts
    {
        [Fact]
        public void ctor_MemberInfo()
        {
            Assert.NotNull(new DerivedMemberTest(typeof(PropertiedClass1).GetProperty("AutoBoolean")));
        }

        [Fact]
        public void ctor_MemberInfoNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DerivedMemberTest(null));
        }

        [Fact]
        public void op_Check()
        {
            Assert.Throws<NotImplementedException>(() => new DerivedMemberTest(typeof(PropertiedClass1).GetProperty("AutoBoolean")).Check());
        }

        [Fact]
        public void prop_Member_get()
        {
            var expected = typeof(PropertiedClass1).GetProperty("AutoBoolean");

            var obj = new DerivedMemberTest(expected);

            var actual = obj.Member;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void prop_Member_set()
        {
            var expected = typeof(PropertiedClass1).GetProperty("AutoBoolean");

            var obj = new DerivedMemberTest(typeof(PropertiedClass1).GetProperty("AutoString"))
                          {
                              Member = expected
                          };

            var actual = obj.Member;

            Assert.Same(expected, actual);
        }

        [Fact]
        public void prop_Member_setNull()
        {
            MemberInfo member = typeof(PropertiedClass1).GetProperty("AutoBoolean");

            Assert.Throws<ArgumentNullException>(() => new DerivedMemberTest(member).Member = null as MemberInfo);
        }
    }
}