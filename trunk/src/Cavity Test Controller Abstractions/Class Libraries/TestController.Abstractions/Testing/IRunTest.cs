namespace Cavity.Testing
{
    using System.ComponentModel.Composition;
    using System.IO;

    [InheritedExport]
    public interface IRunTest
    {
        string Description { get; }

        string Title { get; }

        bool Run(DirectoryInfo directory,
                 TextWriter log);
    }
}