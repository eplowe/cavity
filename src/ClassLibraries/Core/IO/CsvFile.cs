namespace Cavity.IO
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
#if !NET20
    using System.Linq;
#endif
    using System.Text;
    using Cavity.Collections;
    using Cavity.Data;

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This isn't a collection.")]
    public sealed class CsvFile : IEnumerable<KeyStringDictionary>
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

        public FileInfo Info { get; private set; }

        public static string Header(KeyStringDictionary data)
        {
            if (null == data)
            {
                throw new ArgumentNullException("data");
            }

            if (0 == data.Count)
            {
                throw new ArgumentOutOfRangeException("data");
            }

            var buffer = new StringBuilder();
            foreach (var item in data)
            {
                if (0 != buffer.Length)
                {
                    buffer.Append(',');
                }

#if NET20
                buffer.Append(DataStringExtensionMethods.FormatCommaSeparatedValue(item.Key));
#else
                buffer.Append(item.Key.FormatCommaSeparatedValue());
#endif
            }

            return buffer.ToString();
        }

        public static string Line(KeyStringDictionary data)
        {
            if (null == data)
            {
                throw new ArgumentNullException("data");
            }

            if (0 == data.Count)
            {
                throw new ArgumentOutOfRangeException("data");
            }

            var buffer = new StringBuilder();
            foreach (var item in data)
            {
                if (0 != buffer.Length)
                {
                    buffer.Append(',');
                }

#if NET20
                buffer.Append(DataStringExtensionMethods.FormatCommaSeparatedValue(item.Value));
#else
                buffer.Append(item.Value.FormatCommaSeparatedValue());
#endif
            }

            return buffer.ToString();
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This design is intentional.")]
        public static void Save(FileMode mode, IEnumerable<KeyValuePair<FileInfo, KeyStringDictionary>> data)
        {
            if (null == data)
            {
                throw new ArgumentNullException("data");
            }

#if NET20
            KeyStringDictionary line = null;
            foreach (var item in data)
            {
                line = item.Value;
                break;
            }
#else
            var line = data.First().Value;
#endif
            using (var writers = new StreamWriterDictionary(Header(line))
            {
                Access = FileAccess.Write,
                Mode = mode,
                Share = FileShare.Read
            })
            {
                foreach (var item in data)
                {
                    writers.Item(item.Key).WriteLine(Line(item.Value));
                }
            }
        }

        public void Save(FileMode mode, IEnumerable<KeyStringDictionary> data)
        {
            if (null == data)
            {
                throw new ArgumentNullException("data");
            }

            Info.Refresh();
#if NET20
            if (0 == IEnumerableExtensionMethods.Count(data))
#else
            if (0 == data.Count())
#endif
            {
                if (Info.Exists)
                {
                    Info.Delete();
                }

                return;
            }

            if (!Info.Directory.Exists)
            {
                Info.Directory.Create();
            }

#if NET20
            KeyStringDictionary line = null;
            foreach (var item in data)
            {
                line = item;
                break;
            }
#else
            var line = data.First();
#endif
            using (var stream = Info.Open(mode, FileAccess.Write, FileShare.Read))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(Header(line));
                    foreach (var item in data)
                    {
                        writer.WriteLine(Line(item));
                    }
                }
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