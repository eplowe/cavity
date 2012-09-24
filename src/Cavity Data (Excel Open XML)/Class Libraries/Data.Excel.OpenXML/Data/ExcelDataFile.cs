namespace Cavity.Data
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    using ClosedXML.Excel;

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This is not a collection.")]
    public class ExcelDataFile : DataFile
    {
        public ExcelDataFile(FileInfo info)
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

            using (var workbook = new XLWorkbook(Info.FullName))
            {
                foreach (var sheet in workbook.Worksheets)
                {
                    yield return new ExcelDataSheet(Info, sheet);
                }
            }
        }
    }
}