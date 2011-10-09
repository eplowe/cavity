namespace Cavity.Dynamic
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;

    public class DynamicData : DynamicObject
    {
        public DynamicData()
        {
            Data = new Dictionary<string, object>();
        }

        protected Dictionary<string, object> Data { get; private set; }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return Data.Keys;
        }

        public override bool TryGetMember(GetMemberBinder binder,
                                          out object result)
        {
            if (null == binder)
            {
                throw new ArgumentNullException("binder");
            }

            result = Data.ContainsKey(binder.Name) ? Data[binder.Name] : null;

            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder,
                                          object value)
        {
            if (null == binder)
            {
                throw new ArgumentNullException("binder");
            }

            Data[binder.Name] = value;
            return true;
        }
    }
}