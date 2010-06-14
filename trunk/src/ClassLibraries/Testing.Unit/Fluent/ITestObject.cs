namespace Cavity.Fluent
{
    using System.Diagnostics.CodeAnalysis;

    public interface ITestObject
    {
        bool Result { get; }

        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "Inference brings no benefit here.")]
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Implements", Justification = "This naming is intentional.")]
        ITestObject Implements<TInterface>();
    }
}