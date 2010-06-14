namespace Cavity.Tests
{
    using System;
    using System.Reflection;
    using Cavity.Fluent;

    public abstract class MemberTest : ITestExpectation
    {
        private MemberInfo _member;

        protected MemberTest(MemberInfo member)
        {
            this.Member = member;
        }

        public MemberInfo Member
        {
            get
            {
                return this._member;
            }

            set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                this._member = value;
            }
        }

        public abstract bool Check();
    }
}