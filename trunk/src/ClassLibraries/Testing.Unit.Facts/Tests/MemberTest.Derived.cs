namespace Cavity.Tests
{
    using System;
    using System.Reflection;

    public class DerivedMemberTest : MemberTest
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