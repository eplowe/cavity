namespace Cavity
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;

    public abstract class ValueObject<T> : IEquatable<T>
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
            foreach (PropertyInfo property in this.Properties)
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
            foreach (PropertyInfo property in this.Properties)
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