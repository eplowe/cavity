namespace Cavity
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using System.Xml.Serialization;
    using Cavity.Fluent;
    using Cavity.Properties;
    using Cavity.Tests;

    public class PropertyExpectations
    {
        public PropertyExpectations(PropertyInfo property)
        {
            this.Property = property;
            this.Items = new Collection<ITestExpectation>();
        }

        public PropertyInfo Property
        {
            get;
            set;
        }

        public bool Result
        {
            get
            {
                return 0 == this.Items.Where(x => !x.Check()).Count();
            }
        }

        private Collection<ITestExpectation> Items
        {
            get;
            set;
        }

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Inference brings no benefit here.")]
        public PropertyExpectations TypeIs<T>()
        {
            this.Items.Add(new PropertyGetterTest<T>(this.Property));
            return this;
        }

        public PropertyExpectations DefaultValueIsNull()
        {
            this.Items.Add(new PropertyGetterTest(this.Property, null));
            return this;
        }

        public PropertyExpectations DefaultValueIs(object value)
        {
            this.Items.Add(new PropertyGetterTest(this.Property, value));
            return this;
        }

        public PropertyExpectations DefaultValueIsNotNull()
        {
            this.Items.Add(new PropertyDefaultIsNotNullTest(this.Property));
            return this;
        }

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Inference brings no benefit here.")]
        public PropertyExpectations Set<T>()
        {
            return this.Set(default(T));
        }

        public PropertyExpectations Set(object value)
        {
            this.Items.Add(new PropertySetterTest(this.Property, value));
            return this;
        }

        public PropertyExpectations ArgumentNullException()
        {
            this.Exception(null, typeof(ArgumentNullException));
            return this;
        }

        public PropertyExpectations ArgumentOutOfRangeException(object value)
        {
            this.Exception(value, typeof(ArgumentOutOfRangeException));
            return this;
        }

        public PropertyExpectations FormatException(object value)
        {
            this.Exception(value, typeof(FormatException));
            return this;
        }

        public PropertyExpectations Exception(object value, Type expectedException)
        {
            if (null == expectedException)
            {
                throw new ArgumentNullException("expectedException");
            }
            else if (!expectedException.IsSubclassOf(typeof(Exception)))
            {
                throw new ArgumentOutOfRangeException("expectedException");
            }

            this.Items.Add(new PropertySetterTest(this.Property, value) { ExpectedException = expectedException });
            return this;
        }

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Inference brings no benefit here.")]
        public PropertyExpectations IsAutoProperty<T>()
        {
            return this.IsAutoProperty<T>(default(T));
        }

        public PropertyExpectations IsAutoProperty<T>(T defaultValue)
        {
            this.DefaultValueIs(defaultValue);
            this.Set(default(T));
            this.Set(defaultValue);
            if (typeof(string).Equals(typeof(T)))
            {
                this.Set(string.Empty);
            }

            return this;
        }

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Inference brings no benefit here.")]
        public PropertyExpectations IsDecoratedWith<TAttribute>()
            where TAttribute : Attribute
        {
            if (typeof(XmlArrayAttribute).IsAssignableFrom(typeof(TAttribute)))
            {
                throw new TestException(Resources.PropertyExpectations_IsDecoratedWithXmlArray);
            }
            else if (typeof(XmlAttributeAttribute).IsAssignableFrom(typeof(TAttribute)))
            {
                throw new TestException(Resources.PropertyExpectations_IsDecoratedWithXmlAttribute);
            }
            else if (typeof(XmlElementAttribute).IsAssignableFrom(typeof(TAttribute)))
            {
                throw new TestException(Resources.PropertyExpectations_IsDecoratedWithXmlElement);
            }
            else if (typeof(XmlIgnoreAttribute).IsAssignableFrom(typeof(TAttribute)))
            {
                throw new TestException(Resources.PropertyExpectations_IsDecoratedWithXmlIgnore);
            }
            else if (typeof(XmlTextAttribute).IsAssignableFrom(typeof(TAttribute)))
            {
                throw new TestException(Resources.PropertyExpectations_IsDecoratedWithXmlText);
            }
            else if (typeof(XmlNamespaceDeclarationsAttribute).IsAssignableFrom(typeof(TAttribute)))
            {
                throw new TestException(Resources.PropertyExpectations_IsDecoratedWithXmlNamespaceDeclarations);
            }

            this.Items.Add(new DecorationTest(this.Property, typeof(TAttribute)));
            return this;
        }

        public PropertyExpectations XmlArray(string name, string items)
        {
            this.Items.Add(new XmlArrayDecorationTest(this.Property) { Name = name, Items = items });
            return this;
        }

        public PropertyExpectations XmlAttribute(string name)
        {
            this.Items.Add(new XmlAttributeDecorationTest(this.Property) { Name = name });
            return this;
        }

        public PropertyExpectations XmlAttribute(string name, string @namespace)
        {
            this.Items.Add(new XmlAttributeDecorationTest(this.Property) { Name = name, Namespace = @namespace });
            return this;
        }

        public PropertyExpectations XmlElement(string name)
        {
            this.Items.Add(new XmlElementDecorationTest(this.Property) { Name = name });
            return this;
        }

        public PropertyExpectations XmlElement(string name, string @namespace)
        {
            this.Items.Add(new XmlElementDecorationTest(this.Property) { Name = name, Namespace = @namespace });
            return this;
        }

        public PropertyExpectations XmlIgnore()
        {
            this.Items.Add(new XmlIgnoreDecorationTest(this.Property));
            return this;
        }

        public PropertyExpectations XmlNamespaceDeclarations()
        {
            this.Items.Add(new XmlNamespaceDeclarationsDecorationTest(this.Property));
            return this;
        }

        public PropertyExpectations XmlText()
        {
            this.Items.Add(new XmlTextDecorationTest(this.Property));
            return this;
        }

        public PropertyExpectations IsNotDecorated()
        {
            this.Items.Add(new DecorationTest(this.Property, null as Type));
            return this;
        }
    }
}