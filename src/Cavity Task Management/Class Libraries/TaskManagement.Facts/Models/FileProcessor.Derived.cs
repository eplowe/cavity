namespace Cavity.Models
{
    using System.IO;

    public sealed class DerivedFileProcessor : FileProcessor
    {
        public override void Process(FileInfo file,
                                     dynamic data)
        {
        }

        protected override void OnDispose()
        {
        }
    }
}