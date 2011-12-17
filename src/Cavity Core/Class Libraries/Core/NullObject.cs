namespace Cavity
{
    public sealed class NullObject
    {
        private static readonly NullObject _value = new NullObject();

        private NullObject()
        {
        }

        public static NullObject Value
        {
            get
            {
                return _value;
            }
        }
    }
}