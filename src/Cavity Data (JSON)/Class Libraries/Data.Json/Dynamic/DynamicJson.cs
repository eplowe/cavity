namespace Cavity.Dynamic
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;

    public class DynamicJson : DynamicObject
    {
        public DynamicJson()
        {
            Data = new Dictionary<string, object>();
        }

        protected Dictionary<string, object> Data { get; private set; }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return Data.Keys;
        }

        ////public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
        ////{
        ////    return base.TryInvoke(binder, args, out result);
        ////}
        public void SetMember(string name, 
                              object value)
        {
            Data[name] = value;
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