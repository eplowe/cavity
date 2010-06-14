namespace Cavity.Fluent
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    public interface ITestObject
    {
        bool Result { get; }

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Inference brings no benefit here.")]
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Implements", Justification = "This naming is intentional.")]
        ITestObject Implements<TInterface>();

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Inference brings no benefit here.")]
        ITestObject IsDecoratedWith<TAttribute>()
            where TAttribute : Attribute;

        ITestObject Serializable();

        ITestObject XmlRoot(string elementName);

        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "namespace", Justification = "This naming is intentional.")]
        ITestObject XmlRoot(string elementName, string @namespace);

        ITestObject IsNotDecorated();
    }
}