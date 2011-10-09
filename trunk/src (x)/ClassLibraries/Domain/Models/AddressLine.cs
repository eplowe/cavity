namespace Cavity.Models
{
    using System;
#if NET40
    using Cavity.Dynamic;
#endif

    public abstract class AddressLine : IAddressLine
    {
        protected AddressLine()
        {
#if NET40
            Data = new DynamicData();
#endif
        }

#if NET40
        public dynamic Data { get; protected set; }
#else
        public object Data { get; protected set; }
#endif

        public virtual string ToString(IFormatAddress renderer)
        {
            throw new NotSupportedException();
        }
    }
}