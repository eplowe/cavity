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

        public abstract IEnumerator<IDataSheet> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}