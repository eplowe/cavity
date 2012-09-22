namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
#if !NET20
    using System.Linq;
#endif
    using System.Xml;

    using Microsoft.Office.Interop.Excel;

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This is not a collection.")]
    public sealed class ExcelWorksheet : DataSheet
    {
        public ExcelWorksheet(string path)
            : this(new FileInfo(path))
        {
        }

        public ExcelWorksheet(FileInfo info)
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

        protected override IEnumerator<T> GetEnumerator<T>()
        {
            Info.Refresh();
            if (!Info.Exists)
            {
                yield break;
            }

            Application instance = null;
            try
            {
                instance = new Application();
                var workbook = instance.Workbooks.Open(Info.FullName, 0, true, 5, string.Empty, string.Empty, true, XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
#if NET20
                _Worksheet worksheet = null;
                foreach (_Worksheet sheet in workbook.Sheets)
                {
                    if (!string.Equals(sheet.Name, Title, StringComparison.Ordinal))
                    {
                        continue;
                    }

                    worksheet = sheet;
                    break;
                }
#else
                var worksheet = workbook
                    .Sheets
                    .Cast<_Worksheet>()
                    .FirstOrDefault(sheet => string.Equals(sheet.Name, Title, StringComparison.Ordinal));
#endif

                if (null == worksheet)
                {
                    yield break;
                }

                var range = worksheet.UsedRange;
                var columns = new List<string>();
                for (var i = 1; i < range.Columns.Count + 1; i++)
                {
                    columns.Add(Cell(range, 1, i));
                }

                for (var i = 2; i < range.Rows.Count + 1; i++)
                {
                    var entry = Activator.CreateInstance<T>();
                    for (var j = 0; j < columns.Count; j++)
                    {
                        entry.Add(columns[j], Cell(range, i, j + 1));
                    }

                    yield return entry;
                }
            }
            finally
            {
                if (null != instance)
                {
                    instance.Quit();
                }
            }
        }

        private static string Cell(Range range, int row, int column)
        {
            var cell = (Range)range.Cells[row, column];
            if (cell.Value is bool)
            {
                return XmlConvert.ToString((bool)cell.Value);
            }
            
            if (cell.Value is DateTime)
            {
                var value = XmlConvert.ToString((DateTime)cell.Value, XmlDateTimeSerializationMode.Utc);
                if (value.EndsWith("T00:00:00Z", StringComparison.Ordinal))
                {
#if NET20
                    value = StringExtensionMethods.RemoveFromEnd(value, "T00:00:00Z", StringComparison.Ordinal);
#else
                    value = value.RemoveFromEnd("T00:00:00Z", StringComparison.Ordinal);
#endif
                }

                return value;
            }

            return (string)cell.Text;
        }
    }
}