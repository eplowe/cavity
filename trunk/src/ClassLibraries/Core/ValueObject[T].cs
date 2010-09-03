namespace Cavity
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;

    public abstract class ValueObject<T> : IComparable, IEquatable<T>
        where T : ValueObject<T>
    {
        protected ValueObject()
        {
            Properties = new List<PropertyInfo>();
        }

        private List<PropertyInfo> Properties { get; set; }

        public static bool operator ==(ValueObject<T> operand1, ValueObject<T> operand2)
        {
            return ReferenceEquals(null, operand1)
                       ? ReferenceEquals(null, operand2)
                       : operand1.Equals(operand2);
        }

        public static bool operator >(ValueObject<T> operand1, ValueObject<T> operand2)
        {
            return Compare(operand1, operand2) > 0;
        }

        public static implicit operator string(ValueObject<T> value)
        {
            return ReferenceEquals(null, value)
                       ? null
                       : value.ToString();
        }

        public static bool operator !=(ValueObject<T> operand1, ValueObject<T> operand2)
        {
            if (ReferenceEquals(null, operand1))
            {
                return !ReferenceEquals(null, operand2);
            }
            else
            {
                return !operand1.Equals(operand2);
            }
        }

        public static bool operator <(ValueObject<T> operand1, ValueObject<T> operand2)
        {
            return Compare(operand1, operand2) < 0;
        }

        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Inference is not required here.")]
        public static int Compare(ValueObject<T> comparand1, ValueObject<T> comparand2)
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
            return Equals(obj as T);
        }

        public override int GetHashCode()
        {
            var result = base.GetHashCode();

            return Properties
                .Select(property => property.GetValue(this, null))
                .Where(value => null != value)
                .Aggregate(result, (current, value) => current ^ value.GetHashCode());
        }

        public override string ToString()
        {
            var buffer = new StringBuilder();

            foreach (var value in Properties
                .Select(property => property.GetValue(this, null))
                .Where(value => null != value))
            {
                if (0 != buffer.Length)
                {
                    buffer.Append(Environment.NewLine);
                }

                buffer.Append(value.ToString());
            }

            return buffer.ToString();
        }

        public virtual int CompareTo(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return 1;
            }

            var value = obj as ValueObject<T>;

            if (ReferenceEquals(null, value))
            {
                throw new ArgumentOutOfRangeException("obj");
            }

            return Compare(this, value);
        }

        public virtual bool Equals(T other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (Properties.Count !=
                other.Properties.Count)
            {
                return false;
            }

            return !(from property in Properties
                     let left = property.GetValue(this, null)
                     let right = property.GetValue(other, null)
                     where ReferenceEquals(null, left) ? !ReferenceEquals(null, right) : !left.Equals(right)
                     select left).Any();
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This design is intentional.")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "A delegate type is required.")]
        protected void RegisterProperty(Expression<Func<T, object>> expression)
        {
            if (null == expression)
            {
                throw new ArgumentNullException("expression");
            }

            MemberExpression member;
            if (ExpressionType.Convert ==
                expression.Body.NodeType)
            {
                var body = (UnaryExpression)expression.Body;
                member = (MemberExpression)body.Operand;
            }
            else
            {
                member = (MemberExpression)expression.Body;
            }

            RegisterProperty(member);
        }

        private void RegisterProperty(MemberExpression member)
        {
            RegisterProperty((PropertyInfo)member.Member);
        }

        private void RegisterProperty(PropertyInfo property)
        {
            Properties.Add(property);
        }
    }
}