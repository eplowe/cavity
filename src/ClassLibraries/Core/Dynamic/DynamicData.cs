namespace Cavity.Dynamic
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;

    public sealed class DynamicData : DynamicObject
    {
        private readonly Dictionary<string, object> _data;

        public DynamicData()
        {
            _data = new Dictionary<string, object>();
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return _data.Keys;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (null == binder)
            {
                throw new ArgumentNullException("binder");
            }

            result = _data[binder.Name];
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (null == binder)
            {
                throw new ArgumentNullException("binder");
            }

            _data[binder.Name] = value;
            return true;
        }
    }
}