namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    using Cavity.Collections;

    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "DataSheet", Justification = "This is the correct casing.")]
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This is not a collection.")]
    public class CsvDataSheet : DataSheet
    {
        public CsvDataSheet(FileInfo info)
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

        public override IEnumerable<T> As<T>()
        {
            var enumerator = GetEnumerator<T>();
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
        }

        public override IEnumerator<KeyStringDictionary> GetEnumerator()
        {
            return GetEnumerator<KeyStringDictionary>();
        }

        protected IEnumerator<T> GetEnumerator<T>()
            where T : KeyStringDictionary, new()
        {
            Info.Refresh();
            if (!Info.Exists)
            {
                yield break;
            }

            using (var stream = Info.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                using (var reader = new CsvStreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        var entry = reader.ReadEntry<T>();
                        if (null == entry)
                        {
                            break;
                        }

                        yield return (T)entry;
                    }
                }
            }
        }
    }
}