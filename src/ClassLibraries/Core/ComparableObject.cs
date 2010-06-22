namespace Cavity
{
    using System;

    public abstract class ComparableObject : IComparable
    {
        protected ComparableObject()
        {
        }

        public static implicit operator string(ComparableObject value)
        {
            return object.ReferenceEquals(null, value) ? null as string : value.ToString();
        }

        public static bool operator ==(ComparableObject operand1, ComparableObject operand2)
        {
            if (object.ReferenceEquals(null, operand1))
            {
                return object.ReferenceEquals(null, operand2);
            }
            else
            {
                return operand1.Equals(operand2);
            }
        }

        public static bool operator !=(ComparableObject operand1, ComparableObject operand2)
        {
            if (object.ReferenceEquals(null, operand1))
            {
                return !object.ReferenceEquals(null, operand2);
            }
            else
            {
                return !operand1.Equals(operand2);
            }
        }

        public static bool operator <(ComparableObject operand1, ComparableObject operand2)
        {
            return ComparableObject.Compare(operand1, operand2) < 0;
        }

        public static bool operator >(ComparableObject operand1, ComparableObject operand2)
        {
            return ComparableObject.Compare(operand1, operand2) > 0;
        }

        public static int Compare(ComparableObject comparand1, ComparableObject comparand2)
        {
            return object.ReferenceEquals(comparand1, comparand2)
                ? 0
                : string.Compare(
                    object.ReferenceEquals(null, comparand1) ? null as string : comparand1.ToString(),
                    object.ReferenceEquals(null, comparand2) ? null as string : comparand2.ToString(),
                    StringComparison.OrdinalIgnoreCase);
        }

        public virtual int CompareTo(object obj)
        {
            int comparison = 1;

            if (!object.ReferenceEquals(null, obj))
            {
                ComparableObject value = obj as ComparableObject;

                if (object.ReferenceEquals(null, value))
                {
                    throw new ArgumentOutOfRangeException("obj");
                }

                comparison = ComparableObject.Compare(this, value);
            }

            return comparison;
        }

        public override bool Equals(object obj)
        {
            bool result = false;

            if (!object.ReferenceEquals(null, obj))
            {
                if (object.ReferenceEquals(this, obj))
                {
                    result = true;
                }
                else
                {
                    ComparableObject cast = obj as ComparableObject;

                    if (!object.ReferenceEquals(null, cast))
                    {
                        result = 0 == ComparableObject.Compare(this, cast);
                    }
                }
            }

            return result;
        }

        public override int GetHashCode()
        {
            string value = this.ToString();

            return null == value ? 0 : value.GetHashCode();
        }
    }
}