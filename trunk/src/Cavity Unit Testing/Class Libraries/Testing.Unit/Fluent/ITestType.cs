namespace Cavity.Fluent
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    public interface ITestType
    {
        bool Result { get; }

        ITestType Add(ITestExpectation expectation);

        ITestType AttributeUsage(AttributeTargets validOn);

        ITestType AttributeUsage(AttributeTargets validOn, 
                                 bool allowMultiple, 
                                 bool inherited);

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Inference brings no benefit here.")]
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Implements", Justification = "This naming is intentional.")]
        ITestType Implements<TInterface>();

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Inference brings no benefit here.")]
        ITestType IsDecoratedWith<TAttribute>()
            where TAttribute : Attribute;

        ITestType IsNotDecorated();

        ITestType Serializable();

        ITestType XmlRoot(string elementName);

        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "namespace", Justification = "This naming is intentional.")]
        ITestType XmlRoot(string elementName, 
                          string @namespace);

        ITestType XmlSerializable();
    }
}