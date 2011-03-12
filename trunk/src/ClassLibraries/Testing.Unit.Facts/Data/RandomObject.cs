namespace Cavity.Data
{
    public sealed class RandomObject : ComparableObject
    {
        public RandomObject()
        {
            Value = AlphaDecimal.Random();
        }

        private AlphaDecimal Value { get; set; }

        public override string ToString()
        {
            return Value;
        }
    }
}