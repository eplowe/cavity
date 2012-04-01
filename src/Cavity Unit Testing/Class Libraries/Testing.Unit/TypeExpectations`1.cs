namespace Cavity
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    using Cavity.Fluent;
    using Cavity.Properties;
    using Cavity.Tests;

    /// <summary>
    /// Provides functionality to assert expectations about a type.
    /// </summary>
    /// <typeparam name="T">The type under test.</typeparam>
    /// <remarks>
    /// This is an internal DSL which employs method chaining to build a set of expectations.
    /// When <see cref="P:Cavity.Fluent.ITestType.Result"/> is invoked, all the expectations are verified;
    /// if any expectations are not met, a <see cref="T:Cavity.UnitTestException"/> is thrown.
    /// </remarks>
    /// <seealso href="http://code.google.com/p/cavity/wiki/TypeExpectations">Guide to asserting expectations about types.</seealso>
    public sealed class TypeExpectations<T> : ITestClassStyle, 
                                              ITestClassSealed, 
                                              ITestClassConstruction, 
                                              ITestType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Cavity.TypeExpectations`1"/> class.
        /// </summary>
        public TypeExpectations()
        {
            Items = new Collection<ITestExpectation>();
        }

        /// <summary>
        /// Gets a value indicating whether all the expectations have been met.
        /// </summary>
        /// <value>
        /// Returns <see langword="true" /> if all the expectations were met.
        /// </value>
        /// <exception cref="T:Cavity.UnitTestException">Thrown when an expectation is not met.</exception>
        bool ITestType.Result
        {
            get
            {
                return 0 == Items.Count(x => !x.Check());
            }
        }

        private Collection<ITestExpectation> Items { get; set; }

        /// <summary>
        /// Adds an expectation that the type derives from a specified base type.
        /// </summary>
        /// <typeparam name="TBase">The expected type of the base type.</typeparam>
        /// <returns>The current instance.</returns>
        /// <seealso href="http://msdn.microsoft.com/library/hfw7t1ce">base (C# Reference)</seealso>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Inference brings no benefit here.")]
        public ITestClassStyle DerivesFrom<TBase>()
        {
            (this as ITestType).Add(new BaseClassTest<T>(typeof(TBase)));
            return this;
        }

        /// <summary>
        /// Adds an expectation that the type is an interface.
        /// </summary>
        /// <returns>The current instance.</returns>
        /// <seealso href="http://msdn.microsoft.com/library/87d83y5b">interface (C# Reference)</seealso>
        public ITestType IsInterface()
        {
            (this as ITestType).Add(new InterfaceTest<T>());
            return this;
        }

        /// <summary>
        /// Adds an expectation that the type is a value type.
        /// </summary>
        /// <returns>The current instance.</returns>
        /// <seealso href="http://msdn.microsoft.com/library/s1ax56ch">Value Types (C# Reference)</seealso>
        public ITestType IsValueType()
        {
            (this as ITestType).Add(new ValueTypeTest<T>());
            return this;
        }

        /// <summary>
        /// Adds an expectation that the type has a default constructor.
        /// </summary>
        /// <returns>The current instance.</returns>
        /// <seealso href="http://msdn.microsoft.com/library/ace5hbzh">Constructors (C# Programming Guide)</seealso>
        ITestType ITestClassConstruction.HasDefaultConstructor()
        {
            (this as ITestType).Add(new DefaultConstructorTest<T>());
            return this;
        }

        /// <summary>
        /// Adds an expectation that the type does not have a default constructor.
        /// </summary>
        /// <returns>The current instance.</returns>
        /// <seealso href="http://msdn.microsoft.com/library/ace5hbzh">Constructors (C# Programming Guide)</seealso>
        ITestType ITestClassConstruction.NoDefaultConstructor()
        {
            return this;
        }

        /// <summary>
        /// Adds an expectation that the type is sealed.
        /// </summary>
        /// <returns>The current instance.</returns>
        /// <seealso href="http://msdn.microsoft.com/library/88c54tsw">sealed (C# Reference)</seealso>
        ITestClassConstruction ITestClassSealed.IsSealed()
        {
            (this as ITestType).Add(new SealedClassTest<T>(true));
            return this;
        }

        /// <summary>
        /// Adds an expectation that the type is not sealed.
        /// </summary>
        /// <returns>The current instance.</returns>
        /// <seealso href="http://msdn.microsoft.com/library/88c54tsw">sealed (C# Reference)</seealso>
        ITestClassConstruction ITestClassSealed.IsUnsealed()
        {
            (this as ITestType).Add(new SealedClassTest<T>(false));
            return this;
        }

        /// <summary>
        /// Adds an expectation that the type is an abstract base type.
        /// </summary>
        /// <returns>The current instance.</returns>
        /// <seealso href="http://msdn.microsoft.com/library/sf985hc5">abstract (C# Reference)</seealso>
        ITestType ITestClassStyle.IsAbstractBaseClass()
        {
            (this as ITestType).Add(new AbstractBaseClassTest<T>());
            return this;
        }

        /// <summary>
        /// Adds an expectation that the type is a concrete type.
        /// </summary>
        /// <returns>The current instance.</returns>
        /// <seealso href="http://msdn.microsoft.com/library/0b0thckt">class (C# Reference)</seealso>
        ITestClassSealed ITestClassStyle.IsConcreteClass()
        {
            (this as ITestType).Add(new ConcreteClassTest<T>());
            return this;
        }

        /// <summary>
        /// Adds the specified <paramref name="expectation"/>.
        /// </summary>
        /// <param name="expectation">A test expectation.</param>
        /// <returns>The current instance.</returns>
        /// <exception cref="T:System.ArgumentNullException">
        /// Thrown when the specified <paramref name="expectation"/> is <see langword="null"/>.
        /// </exception>
        ITestType ITestType.Add(ITestExpectation expectation)
        {
            if (null == expectation)
            {
                throw new ArgumentNullException("expectation");
            }

            Items.Add(expectation);
            return this;
        }

        /// <summary>
        /// Adds an expectation that the type is decorated with the  <see cref="T:System.AttributeUsageAttribute"/>.
        /// </summary>
        /// <param name="validOn">The expected value of the <see cref="P:System.AttributeUsageAttribute.ValidOn"/> property.</param>
        /// <returns>The current instance.</returns>
        /// <seealso href="http://msdn.microsoft.com/library/system.attributeusageattribute">AttributeUsageAttribute Class</seealso>
        ITestType ITestType.AttributeUsage(AttributeTargets validOn)
        {
            return (this as ITestType).AttributeUsage(validOn, false, true);
        }

        /// <summary>
        /// Adds an expectation that the type is decorated with the  <see cref="T:System.AttributeUsageAttribute"/>.
        /// </summary>
        /// <param name="validOn">The expected value of the <see cref="P:System.AttributeUsageAttribute.ValidOn"/> property.</param>
        /// <param name="allowMultiple">The expected value of the <see cref="P:System.AttributeUsageAttribute.AllowMultiple"/> property.</param>
        /// <param name="inherited">The expected value of the <see cref="P:System.AttributeUsageAttribute.Inherited"/> property.</param>
        /// <returns>The current instance.</returns>
        /// <seealso href="http://msdn.microsoft.com/library/system.attributeusageattribute">AttributeUsageAttribute Class</seealso>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Inference brings no benefit here.")]
        ITestType ITestType.AttributeUsage(AttributeTargets validOn, 
                                           bool allowMultiple, 
                                           bool inherited)
        {
            (this as ITestType).Add(new AttributeUsageTest(typeof(T), validOn, allowMultiple, inherited));
            return this;
        }

        /// <summary>
        /// Adds an expectation that the type implements the specified interface.
        /// </summary>
        /// <typeparam name="TInterface">The expected type of the interface.</typeparam>
        /// <returns>The current instance.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// Thrown when the specified type is not an interface.
        /// </exception>
        /// <seealso href="http://msdn.microsoft.com/library/ms173156">Interfaces (C# Programming Guide)</seealso>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Inference brings no benefit here.")]
        ITestType ITestType.Implements<TInterface>()
        {
            if (!typeof(T).IsInterface && typeof(TInterface) == typeof(IXmlSerializable))
            {
                throw new NotSupportedException(Resources.Implements_IXmlSerializable);
            }

            if (!typeof(TInterface).IsInterface)
            {
                throw new ArgumentOutOfRangeException(Resources.TypeExpectationsException_InterfaceMessage);
            }

            (this as ITestType).Add(new ImplementationTest<T>(typeof(TInterface)));
            return this;
        }

        /// <summary>
        /// Adds an expectation that the type is decorated with the specified attribute.
        /// </summary>
        /// <typeparam name="TAttribute">The expected type of the attribute.</typeparam>
        /// <returns>The current instance.</returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// Thrown when the specified attribute type has a more specific expectation method.
        /// </exception>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Inference brings no benefit here.")]
        ITestType ITestType.IsDecoratedWith<TAttribute>()
        {
            if (typeof(AttributeUsageAttribute).IsAssignableFrom(typeof(TAttribute)))
            {
                throw new NotSupportedException(Resources.TypeExpectations_IsDecoratedWithAttributeUsage);
            }

            if (typeof(SerializableAttribute).IsAssignableFrom(typeof(TAttribute)))
            {
                throw new NotSupportedException(Resources.TypeExpectations_IsDecoratedWithSerializable);
            }

            if (typeof(XmlRootAttribute).IsAssignableFrom(typeof(TAttribute)))
            {
                throw new NotSupportedException(Resources.TypeExpectations_IsDecoratedWithXmlRoot);
            }

            (this as ITestType).Add(new AttributeMemberTest(typeof(T), typeof(TAttribute)));
            return this;
        }

        /// <summary>
        /// Adds an expectation that the type is not decorated with any attributes.
        /// </summary>
        /// <returns>The current instance.</returns>
        ITestType ITestType.IsNotDecorated()
        {
            (this as ITestType).Add(new AttributeMemberTest(typeof(T), null));
            return this;
        }

        /// <summary>
        /// Adds an expectation that the type is decorated with the <see cref="T:System.SerializableAttribute"/>
        /// and implements <see cref="T:System.Runtime.Serialization.ISerializable"/>.
        /// </summary>
        /// <returns>The current instance.</returns>
        ITestType ITestType.Serializable()
        {
            (this as ITestType).Add(new AttributeMemberTest(typeof(T), typeof(SerializableAttribute)));
            (this as ITestType).Add(new ImplementationTest<T>(typeof(ISerializable)));
            return this;
        }

        /// <summary>
        /// Adds an expectation that the type is decorated with the
        /// <see cref="T:System.Xml.Serialization.XmlRootAttribute"/>
        /// with the specified <paramref name="elementName"/>.
        /// </summary>
        /// <param name="elementName">The expected <see cref="P:System.Xml.Serialization.XmlRootAttribute.ElementName"/> value.</param>
        /// <returns>The current instance.</returns>
        ITestType ITestType.XmlRoot(string elementName)
        {
            (this as ITestType).Add(new XmlRootTest<T>(elementName));
            return this;
        }

        /// <summary>
        /// Adds an expectation that the type is decorated with the
        /// <see cref="T:System.Xml.Serialization.XmlRootAttribute"/>
        /// with the specified <paramref name="elementName"/> and <paramref name="namespace"/>.
        /// </summary>
        /// <param name="elementName">The expected <see cref="P:System.Xml.Serialization.XmlRootAttribute.ElementName"/> value.</param>
        /// <param name="namespace">The expected <see cref="P:System.Xml.Serialization.XmlRootAttribute.Namespace"/> value.</param>
        /// <returns>The current instance.</returns>
        ITestType ITestType.XmlRoot(string elementName, 
                                    string @namespace)
        {
            (this as ITestType).Add(new XmlRootTest<T>(elementName, @namespace));
            return this;
        }

        /// <summary>
        /// Adds an expectation that the type implements <see cref="T:System.Xml.Serialization.IXmlSerializable"/>.
        /// </summary>
        /// <returns>The current instance.</returns>
        ITestType ITestType.XmlSerializable()
        {
            (this as ITestType).Add(new XmlSerializableTest(typeof(T)));
            (this as ITestType).Add(new ImplementationTest<T>(typeof(IXmlSerializable)));
            return this;
        }
    }
}