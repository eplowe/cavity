namespace Cavity.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This is not a collection.")]
    public abstract class DataFile : IEnumerable<IDataSheet>
    {
        protected DataFile(FileInfo info)
        {
            if (null == info)
            {
                throw new ArgumentNullException("info");
            }

            if (!info.Exists)
            {
                throw new FileNotFoundException(info.FullName);
            }

            Info = info;
        }

        public FileInfo Info { get; private set; }

        public string Title
        {
            get
            {
                return 0 == Info.Extension.Length
                           ? Info.Name
#if NET20
                           : StringExtensionMethods.RemoveFromEnd(Info.Name, Info.Extension, StringComparison.OrdinalIgnoreCase);
#else
                           : Info.Name.RemoveFromEnd(Info.Extension, StringComparison.OrdinalIgnoreCase);
#endif
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public abstract IEnumerator<IDataSheet> GetEnumerator();
    }
}