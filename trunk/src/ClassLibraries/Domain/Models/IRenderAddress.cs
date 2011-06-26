namespace Cavity.Models
{
    using System.Collections.Generic;

    public interface IRenderAddress
    {
        string ToString(IEnumerable<IAddressLine> address);
    }
}