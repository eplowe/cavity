namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Xml;
    using Cavity.Collections;
    using ClosedXML.Excel;

    [SuppressMessage("Microsoft.Naming", "CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId = "DataSheet", Justification = "This casing is correct.")]
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This is not a collection.")]
    public class ExcelDataSheet : DataSheet
    {
        public ExcelDataSheet(string path)
            : this(new FileInfo(path))
        {
        }

        public ExcelDataSheet(FileInfo info)
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

        internal ExcelDataSheet(FileInfo info,
                                IXLWorksheet worksheet)
            : this(info)
        {
            if (null == worksheet)
            {
                throw new ArgumentNullException("worksheet");
            }

            Worksheet = worksheet;
            Title = worksheet.Name;
        }

        public FileInfo Info { get; private set; }

        private IXLWorksheet Worksheet { get; set; }

        protected override IEnumerator<T> GetEnumerator<T>()
        {
            Info.Refresh();
            if (!Info.Exists)
            {
                yield break;
            }

            if (null == Worksheet)
            {
                using (var workbook = new XLWorkbook(Info.FullName))
                {
                    var worksheet = workbook
                        .Worksheets
                        .FirstOrDefault(sheet => string.Equals(sheet.Name, Title, StringComparison.Ordinal));

                    if (null == worksheet)
                    {
                        yield break;
                    }

                    foreach (var entry in Entries<T>(worksheet))
                    {
                        yield return entry;
                    }
                }
            }
            else
            {
                foreach (var entry in Entries<T>(Worksheet))
                {
                    yield return entry;
                }
            }
        }

        private static string Cell(IXLRange range,
                                   int row,
                                   int column)
        {
            var cell = range.Cell(row, column);
            var value = cell.Value;
            if (value is bool)
            {
                return XmlConvert.ToString((bool)cell.Value);
            }

            if (cell.Value is DateTime)
            {
                var dateTime = XmlConvert.ToString((DateTime)cell.Value, XmlDateTimeSerializationMode.Utc);
                if (dateTime.EndsWith("T00:00:00Z", StringComparison.Ordinal))
                {
                    dateTime = dateTime.RemoveFromEnd("T00:00:00Z", StringComparison.Ordinal);
                }

                return dateTime;
            }

            return cell.GetFormattedString();
        }

        private static IEnumerable<T> Entries<T>(IXLWorksheet worksheet)
            where T : KeyStringDictionary, new()
        {
            var range = worksheet.RangeUsed();
            if (null == range)
            {
                yield break;
            }

            var columns = new List<string>();
            for (var i = 1; i < range.ColumnCount() + 1; i++)
            {
                columns.Add(Cell(range, 1, i));
            }

            for (var i = 2; i < range.RowCount() + 1; i++)
            {
                var entry = Activator.CreateInstance<T>();
                for (var j = 0; j < columns.Count; j++)
                {
                    entry.Add(columns[j], Cell(range, i, j + 1));
                }

                yield return entry;
            }
        }
    }
}