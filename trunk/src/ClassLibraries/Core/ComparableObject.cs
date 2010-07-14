namespace Cavity
{
    using System;

    public abstract class ComparableObject : IComparable
    {
        public static implicit operator string(ComparableObject value)
        {
            return ReferenceEquals(null, value)
                ? null
                : value.ToString();
        }

        public static bool operator ==(ComparableObject operand1, ComparableObject operand2)
        {
            return ReferenceEquals(null, operand1)
                ? ReferenceEquals(null, operand2)
                : operand1.Equals(operand2);
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

        public static bool operator >(ComparableObject operand1, ComparableObject operand2)
        {
            return Compare(operand1, operand2) > 0;
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

        public virtual int CompareTo(object obj)
        {
            var result = 1;

            if (!ReferenceEquals(null, obj))
            {
                var value = obj as ComparableObject;
                if (ReferenceEquals(null, value))
                {
                    throw new ArgumentOutOfRangeException("obj");
                }

                result = Compare(this, value);
            }

            return result;
        }

        public override bool Equals(object obj)
        {
            var result = false;

            if (!ReferenceEquals(null, obj))
            {
                if (ReferenceEquals(this, obj))
                {
                    result = true;
                }
                else
                {
                    var cast = obj as ComparableObject;

                    if (!ReferenceEquals(null, cast))
                    {
                        result = 0 == Compare(this, cast);
                    }
                }
            }

            return result;
        }

        public override int GetHashCode()
        {
            var value = ToString();

            return null == value
                ? 0
                : value.GetHashCode();
        }

        public override string ToString()
        {
            return null;
        }
    }
}