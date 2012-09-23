namespace Cavity.Data
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This isn't a collection.")]
    public class ExcelFile : DataFile
    {
        public ExcelFile(FileInfo info)
            : base(info)
        {
        }

        public override IEnumerator<IDataSheet> GetEnumerator()
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
                    yield return new ExcelWorksheet(Info)
                                     {
                                         Title = row["TABLE_NAME"].ToString()
                                     };
#else
                    yield return new ExcelWorksheet(Info)
                                     {
                                         Title = row.Field<string>("TABLE_NAME")
                                     };
#endif
                }
            }
        }
    }
}