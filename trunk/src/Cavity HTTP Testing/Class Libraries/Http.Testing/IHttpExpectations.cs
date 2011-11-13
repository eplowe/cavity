namespace Cavity
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This isn't fundamentally a collection.")]
    public interface IHttpExpectations : ICollection<HttpExpectation>
    {
        bool Result { get; }
    }
}