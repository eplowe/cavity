namespace Cavity.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This isn't a collection.")]
    public sealed class ExcelFile : IEnumerable<ExcelWorksheet>
    {
        public ExcelFile(FileInfo info)
        {
            if (null == info)
            {
                throw new ArgumentNullException("info");
            }

            Info = info;
        }

        public FileInfo Info { get; private set; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<ExcelWorksheet> GetEnumerator()
        {
            Info.Refresh();
            if (!Info.Exists)
            {
                throw new FileNotFoundException(Info.FullName);
            }

            using (var connection = new OleDbConnection(Excel.ConnectionString(Info)))
            {
                connection.Open();
                var data = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (null == data)
                {
                    yield break;
                }

                foreach (DataRow row in data.Rows)
                {
#if NET20
                    yield return new ExcelWorksheet(Info, row["TABLE_NAME"].ToString());
#else
                    yield return new ExcelWorksheet(Info, row.Field<string>("TABLE_NAME"));
#endif
                }
            }
        }
    }
}