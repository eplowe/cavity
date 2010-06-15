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

    /// <summary>
    /// Provides functionality to assert expectations about a property.
    /// </summary>
    /// <remarks>
    /// This is an internal DSL which employs method chaining to build a set of expectations.
    /// When <see cref="P:Cavity.PropertyExpectations.Result"/> is invoked, all the expectations are verified;
    /// if any expectations are not met, a <see cref="T:Cavity.TestExpectation"/> is thrown.
    /// </remarks>
    /// <seealso href="http://code.google.com/p/cavity/wiki/PropertyExpectations">Guide to asserting expectations about properties.</seealso>
    public class PropertyExpectations
    {
        /// <summary>
        /// Initializes a new instance of <see cref="T:Cavity.PropertyExpectations"/> class
        /// with the specified <paramref name="property"/>.
        /// </summary>
        /// <param name="property">The property under test.</param>
        public PropertyExpectations(PropertyInfo property)
        {
            this.Property = property;
            this.Items = new Collection<ITestExpectation>();
        }

        /// <summary>
        /// Returns <see langword="true"/> if all the expectations have been met.
        /// </summary>
        /// <exception cref="T:Cavity.TestException">Thrown when an expectation is not met.</exception>
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

        private PropertyInfo Property
        {
            get;
            set;
        }

        /// <summary>
        /// Adds an expectation that the property is of the specified type.
        /// </summary>
        /// <typeparam name="T">The expected type of the property.</typeparam>
        /// <returns>The current instance.</returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Inference brings no benefit here.")]
        public PropertyExpectations TypeIs<T>()
        {
            this.Items.Add(new PropertyGetterTest<T>(this.Property));
            return this;
        }

        /// <summary>
        /// Adds an expectation that the property default value is <see langword="null"/>.
        /// </summary>
        /// <returns>The current instance.</returns>
        public PropertyExpectations DefaultValueIsNull()
        {
            this.Items.Add(new PropertyGetterTest(this.Property, null));
            return this;
        }

        /// <summary>
        /// Adds an expectation that the property default value is equal to the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The expected property value.</param>
        /// <returns>The current instance.</returns>
        public PropertyExpectations DefaultValueIs(object value)
        {
            this.Items.Add(new PropertyGetterTest(this.Property, value));
            return this;
        }

        /// <summary>
        /// Adds an expectation that the property default value is not <see langword="null"/>.
        /// </summary>
        /// <returns>The current instance.</returns>
        public PropertyExpectations DefaultValueIsNotNull()
        {
            this.Items.Add(new PropertyDefaultIsNotNullTest(this.Property));
            return this;
        }

        /// <summary>
        /// Adds an expectation that the property can be set to the default value of the specified type.
        /// </summary>
        /// <typeparam name="T">The type whose default value will be set.</typeparam>
        /// <returns>The current instance.</returns>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Inference brings no benefit here.")]
        public PropertyExpectations Set<T>()
        {
            return this.Set(default(T));
        }

        /// <summary>
        /// Adds an expectation that the property can be set to the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to be set.</param>
        /// <returns>The current instance.</returns>
        public PropertyExpectations Set(object value)
        {
            this.Items.Add(new PropertySetterTest(this.Property, value));
            return this;
        }

        /// <summary>
        /// Adds an expectation that an <see cref="T:System.ArgumentNullException"/>
        /// will be thrown when the property is set to <see langword="null"/>.
        /// </summary>
        /// <returns>The current instance.</returns>
        public PropertyExpectations ArgumentNullException()
        {
            this.Exception(null, typeof(ArgumentNullException));
            return this;
        }

        /// <summary>
        /// Adds an expectation that an <see cref="T:System.ArgumentOutOfRangeException"/>
        /// will be thrown when the property is set to the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to be set.</param>
        /// <returns>The current instance.</returns>
        public PropertyExpectations ArgumentOutOfRangeException(object value)
        {
            this.Exception(value, typeof(ArgumentOutOfRangeException));
            return this;
        }

        /// <summary>
        /// Adds an expectation that a <see cref="T:System.FormatException"/>
        /// will be thrown when the property is set to the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to be set.</param>
        /// <returns>The current instance.</returns>
        public PropertyExpectations FormatException(object value)
        {
            this.Exception(value, typeof(FormatException));
            return this;
        }

        /// <summary>
        /// Adds an expectation that an exception of the specified <paramref name="expectedException"/> type
        /// will be thrown when the property is set to the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value to be set.</param>
        /// <param name="expectedException">The type of the expected exception.</param>
        /// <returns>The current instance.</returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// Thrown when the specified <paramref name="expectedException"/> type is <see langword="null"/>.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// Thrown when the specified <paramref name="expectedException"/> type
        /// does not derive from <see cref="T:System.Exception"/>.
        /// </exception>
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

        /// <summary>
        /// Adds an expectation that the property is auto-implemented of the specified type.
        /// </summary>
        /// <typeparam name="T">The expected type of the property.</typeparam>
        /// <returns>The current instance.</returns>
        /// <seealso href="http://msdn.microsoft.com/library/bb384054">
        /// Auto-Implemented Properties (C# Programming Guide)
        /// </seealso>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Inference brings no benefit here.")]
        public PropertyExpectations IsAutoProperty<T>()
        {
            return this.IsAutoProperty<T>(default(T));
        }

        /// <summary>
        /// Adds an expectation that the property is auto-implemented of the specified type
        /// with the specified default value.
        /// </summary>
        /// <typeparam name="T">The expected type of the property.</typeparam>
        /// <param name="defaultValue">The expected default value.</param>
        /// <returns>The current instance.</returns>
        /// <seealso href="http://msdn.microsoft.com/library/bb384054">
        /// Auto-Implemented Properties (C# Programming Guide)
        /// </seealso>
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

        /// <summary>
        /// Adds an expectation that the property is decorated with an attribute of the specified type.
        /// </summary>
        /// <typeparam name="TAttribute">The type of expected attribute.</typeparam>
        /// <returns>The current instance.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// Thrown when the specified attribute type has a more specific expectation method.
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Inference brings no benefit here.")]
        public PropertyExpectations IsDecoratedWith<TAttribute>()
            where TAttribute : Attribute
        {
            if (typeof(XmlArrayAttribute).IsAssignableFrom(typeof(TAttribute)))
            {
                throw new ArgumentOutOfRangeException(Resources.PropertyExpectations_IsDecoratedWithXmlArray);
            }
            else if (typeof(XmlAttributeAttribute).IsAssignableFrom(typeof(TAttribute)))
            {
                throw new ArgumentOutOfRangeException(Resources.PropertyExpectations_IsDecoratedWithXmlAttribute);
            }
            else if (typeof(XmlElementAttribute).IsAssignableFrom(typeof(TAttribute)))
            {
                throw new ArgumentOutOfRangeException(Resources.PropertyExpectations_IsDecoratedWithXmlElement);
            }
            else if (typeof(XmlIgnoreAttribute).IsAssignableFrom(typeof(TAttribute)))
            {
                throw new ArgumentOutOfRangeException(Resources.PropertyExpectations_IsDecoratedWithXmlIgnore);
            }
            else if (typeof(XmlTextAttribute).IsAssignableFrom(typeof(TAttribute)))
            {
                throw new ArgumentOutOfRangeException(Resources.PropertyExpectations_IsDecoratedWithXmlText);
            }
            else if (typeof(XmlNamespaceDeclarationsAttribute).IsAssignableFrom(typeof(TAttribute)))
            {
                throw new ArgumentOutOfRangeException(Resources.PropertyExpectations_IsDecoratedWithXmlNamespaceDeclarations);
            }

            this.Items.Add(new AttributeMemberTest(this.Property, typeof(TAttribute)));
            return this;
        }

        /// <summary>
        /// Adds an expectation that the property is not decorated with any attributes.
        /// </summary>
        /// <returns>The current instance.</returns>
        public PropertyExpectations IsNotDecorated()
        {
            this.Items.Add(new AttributeMemberTest(this.Property, null as Type));
            return this;
        }

        /// <summary>
        /// Adds an expectation that the property is decorated with the
        /// <see cref="T:System.Xml.Serialization.XmlArrayAttribute"/>
        /// with the specified <paramref name="arrayElementName"/>
        /// and <paramref name="arrayItemElementName"/>.
        /// </summary>
        /// <param name="arrayElementName">
        /// The expected <see cref="P:System.Xml.Serialization.XmlArrayAttribute.ElementName"/> value.
        /// </param>
        /// <param name="arrayItemElementName">
        /// The expected <see cref="P:System.Xml.Serialization.XmlArrayItemAttribute.ElementName"/> value.
        /// </param>
        /// <returns>The current instance.</returns>
        public PropertyExpectations XmlArray(string arrayElementName, string arrayItemElementName)
        {
            this.Items.Add(new XmlArrayTest(this.Property) { ArrayElementName = arrayElementName, ArrayItemElementName = arrayItemElementName });
            return this;
        }

        /// <summary>
        /// Adds an expectation that the property is decorated with the
        /// <see cref="T:System.Xml.Serialization.XmlAttributeAttribute"/>
        /// with the specified <paramref name="attributeName"/>.
        /// </summary>
        /// <param name="attributeName">
        /// The expected <see cref="P:System.Xml.Serialization.XmlAttributeAttribute.AttributeName"/> value.
        /// </param>
        /// <returns>The current instance.</returns>
        public PropertyExpectations XmlAttribute(string attributeName)
        {
            this.Items.Add(new XmlAttributeTest(this.Property) { AttributeName = attributeName });
            return this;
        }

        /// <summary>
        /// Adds an expectation that the property is decorated with the
        /// <see cref="T:System.Xml.Serialization.XmlAttributeAttribute"/>
        /// with the specified <paramref name="attributeName"/> and <paramref name="namespace"/>.
        /// </summary>
        /// <param name="attributeName">
        /// The expected <see cref="P:System.Xml.Serialization.XmlAttributeAttribute.AttributeName"/> value.
        /// </param>
        /// <param name="namespace">
        /// The expected <see cref="P:System.Xml.Serialization.XmlAttributeAttribute.Namespace"/> value.
        /// </param>
        /// <returns>The current instance.</returns>
        public PropertyExpectations XmlAttribute(string attributeName, string @namespace)
        {
            this.Items.Add(new XmlAttributeTest(this.Property) { AttributeName = attributeName, Namespace = @namespace });
            return this;
        }

        /// <summary>
        /// Adds an expectation that the property is decorated with the
        /// <see cref="T:System.Xml.Serialization.XmlElementAttribute"/>
        /// with the specified <paramref name="elementName"/>.
        /// </summary>
        /// <param name="elementName">
        /// The expected <see cref="P:System.Xml.Serialization.XmlElementAttribute.ElementName"/> value.
        /// </param>
        /// <returns>The current instance.</returns>
        public PropertyExpectations XmlElement(string elementName)
        {
            this.Items.Add(new XmlElementTest(this.Property) { ElementName = elementName });
            return this;
        }

        /// <summary>
        /// Adds an expectation that the property is decorated with the
        /// <see cref="T:System.Xml.Serialization.XmlElementAttribute"/>
        /// with the specified <paramref name="elementName"/> and <paramref name="namespace"/>.
        /// </summary>
        /// <param name="elementName">
        /// The expected <see cref="P:System.Xml.Serialization.XmlElementAttribute.ElementName"/> value.
        /// </param>
        /// <param name="namespace">
        /// The expected <see cref="P:System.Xml.Serialization.XmlElementAttribute.Namespace"/> value.
        /// </param>
        /// <returns>The current instance.</returns>
        public PropertyExpectations XmlElement(string elementName, string @namespace)
        {
            this.Items.Add(new XmlElementTest(this.Property) { ElementName = elementName, Namespace = @namespace });
            return this;
        }

        /// <summary>
        /// Adds an expectation that the property is decorated with the
        /// <see cref="T:System.Xml.Serialization.XmlIgnoreAttribute"/>.
        /// </summary>
        /// <returns>The current instance.</returns>
        public PropertyExpectations XmlIgnore()
        {
            this.Items.Add(new XmlIgnoreTest(this.Property));
            return this;
        }

        /// <summary>
        /// Adds an expectation that the property is decorated with the
        /// <see cref="T:System.Xml.Serialization.XmlNamespaceDeclarationsAttribute"/>.
        /// </summary>
        /// <returns>The current instance.</returns>
        public PropertyExpectations XmlNamespaceDeclarations()
        {
            this.Items.Add(new XmlNamespaceDeclarationsTest(this.Property));
            return this;
        }

        /// <summary>
        /// Adds an expectation that the property is decorated with the
        /// <see cref="T:System.Xml.Serialization.XmlTextAttribute"/>.
        /// </summary>
        /// <returns>The current instance.</returns>
        public PropertyExpectations XmlText()
        {
            this.Items.Add(new XmlTextTest(this.Property));
            return this;
        }
    }
}