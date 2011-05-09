namespace Cavity.IO
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
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

                buffer.Append(item.Key.FormatCommaSeparatedValue());
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

                buffer.Append(item.Value.FormatCommaSeparatedValue());
            }

            return buffer.ToString();
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This design is intentional.")]
        public static void Save(IEnumerable<KeyValuePair<FileInfo, KeyStringDictionary>> data)
        {
            if (null == data)
            {
                throw new ArgumentNullException("data");
            }

            using (var writers = new StreamWriterDictionary(Header(data.First().Value))
            {
                Access = FileAccess.Write,
                Mode = FileMode.Create,
                Share = FileShare.Read
            })
            {
                foreach (var item in data)
                {
                    writers.Item(item.Key).WriteLine(Line(item.Value));
                }
            }
        }

        public void Save(IEnumerable<KeyStringDictionary> data)
        {
            if (null == data)
            {
                throw new ArgumentNullException("data");
            }

            Info.Refresh();
            if (0 == data.Count())
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

            using (var stream = Info.Open(FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(Header(data.First()));
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