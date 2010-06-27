namespace Cavity
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;

    public abstract class ValueObject<T> : IComparable, IEquatable<T>
        where T : ValueObject<T>
    {
        protected ValueObject()
        {
            this.Properties = new List<PropertyInfo>();
        }

        private List<PropertyInfo> Properties
        {
            get;
            set;
        }

        public static implicit operator string(ValueObject<T> value)
        {
            return object.ReferenceEquals(null, value) ? null as string : value.ToString();
        }

        public static bool operator ==(ValueObject<T> operand1, ValueObject<T> operand2)
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

        public static bool operator !=(ValueObject<T> operand1, ValueObject<T> operand2)
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

        public static bool operator <(ValueObject<T> operand1, ValueObject<T> operand2)
        {
            return ValueObject<T>.Compare(operand1, operand2) < 0;
        }

        public static bool operator >(ValueObject<T> operand1, ValueObject<T> operand2)
        {
            return ValueObject<T>.Compare(operand1, operand2) > 0;
        }

        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes", Justification = "Inference is not required here.")]
        public static int Compare(ValueObject<T> comparand1, ValueObject<T> comparand2)
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
            int result = 1;

            if (!object.ReferenceEquals(null, obj))
            {
                ValueObject<T> value = obj as ValueObject<T>;

                if (object.ReferenceEquals(null, value))
                {
                    throw new ArgumentOutOfRangeException("obj");
                }

                result = ValueObject<T>.Compare(this, value);
            }

            return result;
        }

        public virtual bool Equals(T other)
        {
            bool result = false;

            if (!object.ReferenceEquals(null, other))
            {
                if (object.ReferenceEquals(this, other))
                {
                    result = true;
                }
                else
                {
                    if (this.Properties.Count == other.Properties.Count)
                    {
                        result = true;
                        for (int i = 0; i < this.Properties.Count; i++)
                        {
                            PropertyInfo property = this.Properties[i];
                            object left = property.GetValue(this, null);
                            object right = property.GetValue(other, null);
                            if (object.ReferenceEquals(null, left))
                            {
                                result = object.ReferenceEquals(null, right);
                            }
                            else
                            {
                                result = left.Equals(right);
                            }

                            if (!result)
                            {
                                break;
                            }
                        }
                    }
                }
            }

            return result;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as T);
        }

        public override int GetHashCode()
        {
            int result = base.GetHashCode();

            foreach (var property in this.Properties)
            {
                object value = property.GetValue(this, null);
                if (null != value)
                {
                    result = result ^ value.GetHashCode();
                }
            }

            return result;
        }

        public override string ToString()
        {
            StringBuilder buffer = new StringBuilder();

            foreach (var property in this.Properties)
            {
                object value = property.GetValue(this, null);
                if (null != value)
                {
                    if (0 != buffer.Length)
                    {
                        buffer.Append(Environment.NewLine);
                    }

                    buffer.Append(value.ToString());
                }
            }

            return buffer.ToString();
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This design is intentional.")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "A delegate type is required.")]
        protected void RegisterProperty(Expression<Func<T, object>> expression)
        {
            if (null == expression)
            {
                throw new ArgumentNullException("expression");
            }

            MemberExpression member = null;
            if (ExpressionType.Convert == expression.Body.NodeType)
            {
                UnaryExpression body = (UnaryExpression)expression.Body;
                member = body.Operand as MemberExpression;
            }
            else
            {
                member = expression.Body as MemberExpression;
            }

            this.RegisterProperty(member);
        }

        private void RegisterProperty(MemberExpression member)
        {
            this.RegisterProperty(member.Member as PropertyInfo);
        }

        private void RegisterProperty(PropertyInfo property)
        {
            this.Properties.Add(property);
        }
    }
}