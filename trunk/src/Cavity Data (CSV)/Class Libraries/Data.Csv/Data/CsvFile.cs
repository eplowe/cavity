namespace Cavity.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
#if !NET20
    using System.Linq;
#endif
    using System.Text;

    using Cavity.Collections;
    using Cavity.Data.Transformation;
    using Cavity.IO;

    [Obsolete("CsvFile is now deprecated. Use CsvDataFile or CsvDataSheet instead.")]
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

        public static string Line(KeyStringDictionary data, 
                                  string columns)
        {
            if (null == data)
            {
                throw new ArgumentNullException("data");
            }

            if (null == columns)
            {
                throw new ArgumentNullException("columns");
            }

            columns = columns.Normalize().Trim();
            if (0 == columns.Length)
            {
                throw new ArgumentOutOfRangeException("columns");
            }

            return Line(data, columns.Split(','));
        }

        public static string Line(KeyStringDictionary data, 
                                  IList<string> columns)
        {
            if (null == data)
            {
                throw new ArgumentNullException("data");
            }

            if (null == columns)
            {
                throw new ArgumentNullException("columns");
            }

            if (0 == columns.Count)
            {
                throw new ArgumentOutOfRangeException("columns");
            }

#if NET20
            var values = new List<string>();
            foreach (var column in columns)
            {
                values.Add(data[column.Normalize().Trim()]);
            }
#else
            var values = columns
                .Select(column => data[column.Normalize().Trim()])
                .ToList();
#endif

            return Line(values);
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
                buffer.Append(CsvStringExtensionMethods.FormatCommaSeparatedValue(item));
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

        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "I want type safety here.")]
        public static void Save(FileInfo file,
                                DataTable table)
        {
            if (null == file)
            {
                throw new ArgumentNullException("file");
            }

            if (null == table)
            {
                throw new ArgumentNullException("table");
            }

            var exists = file.Exists;
            using (var stream = file.Open(exists ? FileMode.Append : FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
            {
                using (var writer = new StreamWriter(stream))
                {
                    var buffer = new StringBuilder();
                    var index = 0;
                    if (!exists)
                    {
                        foreach (DataColumn column in table.Columns)
                        {
                            if (0 != index)
                            {
                                buffer.Append(',');
                            }

                            index++;
#if NET20
                            buffer.Append(CsvStringExtensionMethods.FormatCommaSeparatedValue(column.ColumnName));
#else
                            buffer.Append(column.ColumnName.FormatCommaSeparatedValue());
#endif
                        }
                    }

                    writer.WriteLine(buffer.ToString());
#if NET20 || NET35
                    buffer = new StringBuilder();
#else
                    buffer.Clear();
#endif
                    index = 0;
                    foreach (DataRow row in table.Rows)
                    {
                        foreach (var item in row.ItemArray)
                        {
                            if (0 != index)
                            {
                                buffer.Append(',');
                            }

                            index++;
#if NET20
                            buffer.Append(CsvStringExtensionMethods.FormatCommaSeparatedValue(item.ToString()));
#else
                            buffer.Append(item.ToString().FormatCommaSeparatedValue());
#endif
                        }

                        writer.WriteLine(buffer.ToString());
                    }
                }
            }
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

        public IEnumerable<T> As<T>()
            where T : KeyStringDictionary, new()
        {
            var enumerator = GetEnumerator<T>();
            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
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
            if (null != Info.Directory &&
                !Info.Directory.Exists)
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
            if (0 == count &&
                Info.Exists)
            {
                Info.Delete();
            }
        }

        public virtual IEnumerable<DataTable> Shard(IIdentifyShard identifier)
        {
            if (null == identifier)
            {
                throw new ArgumentNullException("identifier");
            }

            DataTable table = null;
            string current = null;
            foreach (var entry in this)
            {
                var id = identifier.IdentifyShard(entry);
                if (null == table)
                {
                    table = NewDataTable(id, entry);
                }

                if (id != current)
                {
                    current = id;
                    if (0 != table.Rows.Count)
                    {
                        yield return table;
                    }

                    table = NewDataTable(id, entry);
                }

                var row = NewDataRow(table, entry);

                table.Rows.Add(row);
            }

            if (null == table)
            {
                yield break;
            }

            if (0 != table.Rows.Count)
            {
                yield return table;
            }
        }

        public DataTable ToDataTable()
        {
            DataTable table = null;
            foreach (var entry in this)
            {
                if (null == table)
                {
                    table = NewDataTable(Info.Name, entry);
                }

                var row = NewDataRow(table, entry);

                table.Rows.Add(row);
            }

            return table;
        }

        public IEnumerable<KeyStringDictionary> Transform(ITransformCsv transformer)
        {
            if (null == transformer)
            {
                throw new ArgumentNullException("transformer");
            }

            return transformer.TransformEntries(this);
        }

        public IEnumerable<T> Transform<T>(ITransformCsv<T> transformer)
        {
            if (null == transformer)
            {
                throw new ArgumentNullException("transformer");
            }

            return transformer.TransformEntries(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<KeyStringDictionary> GetEnumerator()
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

        private static DataTable NewDataTable(string name,
                                              Dictionary<string, string> entry)
        {
            var table = new DataTable(name)
            {
                Locale = CultureInfo.InvariantCulture
            };
            foreach (var key in entry.Keys)
            {
                table.Columns.Add(key);
            }

            return table;
        }

        private static DataRow NewDataRow(DataTable table,
                                          Dictionary<string, string> entry)
        {
            var row = table.NewRow();
            foreach (var key in entry.Keys)
            {
                row[key] = entry[key];
            }

            return row;
        }
    }
}