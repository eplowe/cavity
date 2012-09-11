namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This is not a collection.")]
    public class CsvDataFile : DataFile
    {
        public CsvDataFile(FileInfo info)
            : base(info)
        {
        }

        public override IEnumerator<IDataSheet> GetEnumerator()
        {
            var title = 0 == Info.Extension.Length
                            ? Info.Name
#if NET20
                            : StringExtensionMethods.RemoveFromEnd(Info.Name, Info.Extension, StringComparison.OrdinalIgnoreCase);
#else
                            : Info.Name.RemoveFromEnd(Info.Extension, StringComparison.OrdinalIgnoreCase);
#endif

            yield return new CsvDataSheet(Info)
                             {
                                 Title = title
                             };
        }
    }
}