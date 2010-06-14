namespace Cavity.Tests
{
    using System;
    using System.Reflection;
    using Cavity.Fluent;

    public abstract class AttributePropertyTest : ITestExpectation
    {
        private MemberInfo _info;

        protected AttributePropertyTest(MemberInfo info)
        {
            this.MemberInfo = info;
        }

        public MemberInfo MemberInfo
        {
            get
            {
                return this._info;
            }

            set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("value");
                }

                this._info = value;
            }
        }

        public abstract bool Check();
    }
}