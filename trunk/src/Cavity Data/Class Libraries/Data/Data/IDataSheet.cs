namespace Cavity.Data
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    using Cavity.Collections;

    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "DataSheet", Justification = "This is the correct casing.")]
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This is not a collection.")]
    public interface IDataSheet : IEnumerable<KeyStringDictionary>
    {
        string Title { get; set; }
    }
}