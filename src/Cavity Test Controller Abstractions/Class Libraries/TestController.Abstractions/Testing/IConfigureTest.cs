namespace Cavity.Testing
{
    using System.Collections.Generic;

    public interface IConfigureTest
    {
        IEnumerable<string> Configurations { get; }
    }
}