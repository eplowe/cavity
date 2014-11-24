namespace Cavity.Data
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using Microsoft.Office.Interop.Excel;

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This is not a collection.")]
    public class ExcelWorkbook : DataFile
    {
        public ExcelWorkbook(FileInfo info)
            : base(info)
        {
        }

        public override IEnumerator<IDataSheet> GetEnumerator()
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
                foreach (_Worksheet sheet in workbook.Sheets)
                {
                    yield return new ExcelWorksheet(Info, sheet);
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