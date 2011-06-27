namespace Cavity.Models
{
    using System.Collections.Generic;

    public interface IFormatAddress
    {
        string ToString(IEnumerable<IAddressLine> address);
    }
}