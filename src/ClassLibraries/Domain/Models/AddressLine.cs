namespace Cavity.Models
{
#if NET40
    using Cavity.Dynamic;
#endif

    public abstract class AddressLine : IAddressLine
    {
        protected AddressLine()
        {
#if NET40
            Value = new DynamicData();
#endif
        }

        public string Original { get; set; }

#if NET40
        public dynamic Value { get; set; }
#else
        public object Value { get; set; }
#endif
    }
}