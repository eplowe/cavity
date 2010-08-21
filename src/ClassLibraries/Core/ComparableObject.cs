namespace Cavity
{
    using System;

    public abstract class ComparableObject : IComparable
    {
        public static bool operator ==(ComparableObject operand1, ComparableObject operand2)
        {
            return ReferenceEquals(null, operand1)
                       ? ReferenceEquals(null, operand2)
                       : operand1.Equals(operand2);
        }

        public static bool operator >(ComparableObject operand1, ComparableObject operand2)
        {
            return Compare(operand1, operand2) > 0;
        }

        public static implicit operator string(ComparableObject value)
        {
            return ReferenceEquals(null, value)
                       ? null
                       : value.ToString();
        }

        public static bool operator !=(ComparableObject operand1, ComparableObject operand2)
        {
            return ReferenceEquals(null, operand1)
                       ? !ReferenceEquals(null, operand2)
                       : !operand1.Equals(operand2);
        }

        public static bool operator <(ComparableObject operand1, ComparableObject operand2)
        {
            return Compare(operand1, operand2) < 0;
        }

        public static int Compare(ComparableObject comparand1, ComparableObject comparand2)
        {
            return ReferenceEquals(comparand1, comparand2)
                       ? 0
                       : string.Compare(
                           ReferenceEquals(null, comparand1) ? null : comparand1.ToString(),
                           ReferenceEquals(null, comparand2) ? null : comparand2.ToString(),
                           StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var cast = obj as ComparableObject;

            if (ReferenceEquals(null, cast))
            {
                return false;
            }

            return 0 == Compare(this, cast);
        }

        public override int GetHashCode()
        {
            var value = ToString();

            return ReferenceEquals(null, value)
                       ? 0
                       : value.GetHashCode();
        }

        public override string ToString()
        {
            return string.Empty;
        }

        public virtual int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return 1;
            }

            var value = obj as ComparableObject;

            if (ReferenceEquals(null, value))
            {
                throw new ArgumentOutOfRangeException("obj");
            }

            return Compare(this, value);
        }
    }
}