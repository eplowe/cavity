namespace Cavity
{
    public sealed class ComparableObjectDerived : ComparableObject
    {
        public ComparableObjectDerived()
            : this(null)
        {
        }

        public ComparableObjectDerived(string value)
        {
            Value = value;
        }

        public string Value
        {
            get;
            set;
        }

        public override string ToString()
        {
            return base.ToString() + Value;
        }
    }
}