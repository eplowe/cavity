namespace Cavity
{
    public sealed class ComparableObjectDerived : ComparableObject
    {
        public ComparableObjectDerived()
            : this(null as string)
        {
        }

        public ComparableObjectDerived(string value)
        {
            this.Value = value;
        }

        public string Value
        {
            get;
            set;
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}