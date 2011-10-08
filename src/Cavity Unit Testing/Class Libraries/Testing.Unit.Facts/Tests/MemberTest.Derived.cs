namespace Cavity.Tests
{
    using System;
    using System.Reflection;

    public sealed class DerivedMemberTest : MemberTestBase
    {
        public DerivedMemberTest(MemberInfo property)
            : base(property)
        {
        }

        public override bool Check()
        {
            throw new NotImplementedException();
        }
    }
}