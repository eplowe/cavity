namespace Cavity.IO
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Text;
    using Cavity.Collections;
    using Cavity.Data;

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This isn't a collection.")]
    public class CsvFile : IEnumerable<KeyStringDictionary>
    {
        public CsvFile(string path)
            : this(new FileInfo(path))
        {
        }

        public CsvFile(FileInfo info)
        {
            if (null == info)
            {
                throw new ArgumentNullException("info");
            }

            Info = info;
        }

        public FileInfo Info { get; protected set; }

        public static string Header(KeyStringDictionary data)
        {
            if (null == data)
            {
                throw new ArgumentNullException("data");
            }

            return Line(data.Keys);
        }

        public static string Line(KeyStringDictionary data)
        {
            if (null == data)
            {
                throw new ArgumentNullException("data");
            }

            return Line(data.Values);
        }

        public static string Line(IEnumerable<string> data)
        {
            if (null == data)
            {
                throw new ArgumentNullException("data");
            }

            var buffer = new StringBuilder();
            var i = 0;
            foreach (var item in data)
            {
                if (0 != i)
                {
                    buffer.Append(',');
                }

#if NET20
                buffer.Append(DataStringExtensionMethods.FormatCommaSeparatedValue(item));
#else
                buffer.Append(item.FormatCommaSeparatedValue());
#endif
                i++;
            }

            if (0 == i)
            {
                throw new ArgumentOutOfRangeException("data");
            }

            return buffer.ToString();
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This design is intentional.")]
        public static void Save(FileMode mode,
                                IEnumerable<KeyValuePair<FileInfo, KeyStringDictionary>> data)
        {
            if (null == data)
            {
                throw new ArgumentNullException("data");
            }

            var count = 0;
            using (var writers = new StreamWriterDictionary
            {
                Access = FileAccess.Write,
                Mode = mode,
                Share = FileShare.Read
            })
            {
                foreach (var item in data)
                {
                    if (0 == count)
                    {
                        writers.FirstLine = Header(item.Value);
                    }

                    writers.Item(item.Key).WriteLine(Line(item.Value));
                    count++;
                }
            }
        }

        public virtual void Save(FileMode mode,
                                 IEnumerable<KeyStringDictionary> data)
        {
            if (null == data)
            {
                throw new ArgumentNullException("data");
            }

            Info.Refresh();
            if (null != Info.Directory && !Info.Directory.Exists)
            {
                Info.Directory.Create();
            }

            var count = 0;
            using (var stream = Info.Open(mode, FileAccess.Write, FileShare.Read))
            {
                using (var writer = new StreamWriter(stream))
                {
                    foreach (var line in data)
                    {
                        if (0 == count)
                        {
                            writer.WriteLine(Header(line));
                        }

                        writer.WriteLine(Line(line));
                        count++;
                    }
                }
            }

            Info.Refresh();
            if (0 == count && Info.Exists)
            {
                Info.Delete();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyStringDictionary> GetEnumerator()
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
                        var entry = reader.ReadEntry();
                        if (null == entry)
                        {
                            break;
                        }

                        yield return entry;
                    }
                }
            }
        }
    }
}