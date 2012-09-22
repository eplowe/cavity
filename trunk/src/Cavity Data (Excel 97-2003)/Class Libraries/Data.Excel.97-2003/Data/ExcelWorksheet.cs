namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
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
                _Worksheet worksheet = null;
                foreach (_Worksheet sheet in workbook.Sheets)
                {
                    if (string.Equals(sheet.Name, Title, StringComparison.Ordinal))
                    {
                        worksheet = sheet;
                        break;
                    }
                }

                if (null == worksheet)
                {
                    yield break;
                }

                var range = worksheet.UsedRange;
                var columns = new List<string>();
                for (var i = 1; i < range.Columns.Count + 1; i++)
                {
                    var cell = (Range)range.Cells[1, i];
                    columns.Add((string)cell.Text);
                    
                    //// columns.Add((string)range.Cells[1, i].Value);
                }

                for (var i = 2; i < range.Rows.Count + 1; i++)
                {
                    var entry = Activator.CreateInstance<T>();
                    for (var j = 0; j < columns.Count; j++)
                    {
                        var cell = (Range)range.Cells[i, j + 1];
                        if (cell.Value is bool)
                        {
                            entry.Add(columns[j], XmlConvert.ToString((bool)cell.Value));
                        }
                        else if (cell.Value is DateTime)
                        {
                            var value = XmlConvert.ToString((DateTime)cell.Value, XmlDateTimeSerializationMode.Utc);
                            if (value.EndsWith("T00:00:00Z", StringComparison.Ordinal))
                            {
                                value = value.RemoveFromEnd("T00:00:00Z", StringComparison.Ordinal);
                            }

                            entry.Add(columns[j], value);
                        }
                        else
                        {
                            entry.Add(columns[j], (string)cell.Text);
                        }

                        ////var value = range.Cells[i, j + 1].Value ?? string.Empty;
                        ////entry.Add(columns[j], value.ToString());
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
    }
}