namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.Data.SqlClient;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
#if !NET20
    using System.Linq;

#endif

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This isn't a collection.")]
    public class ExcelWorksheet : DataSheet
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

        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "Input is quote encoded.")]
        protected override IEnumerator<T> GetEnumerator<T>()
        {
            Info.Refresh();
            if (!Info.Exists)
            {
                yield break;
            }

            using (var connection = new OleDbConnection(Excel.ConnectionString(Info)))
            {
                connection.Open();
#if NET20
                var sql = StringExtensionMethods.FormatWith("SELECT * FROM {0}", new SqlCommandBuilder().QuoteIdentifier(Title));
#else
                var sql = "SELECT * FROM {0}".FormatWith(new SqlCommandBuilder().QuoteIdentifier(Title));
#endif
                using (var command = new OleDbCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (null == reader)
                        {
                            yield break;
                        }

                        var schema = reader.GetSchemaTable();
                        if (null == schema)
                        {
                            yield break;
                        }

#if NET20
                        var columns = new List<string>();
                        foreach (DataRow row in schema.Rows)
                        {
                            columns.Add(row["ColumnName"].ToString());
                        }
#else
                        var columns = (from DataRow row in schema.Rows
                                       select row.Field<string>("ColumnName")).ToList();
#endif

                        while (reader.Read())
                        {
                            var entry = Activator.CreateInstance<T>();

                            foreach (var column in columns)
                            {
                                entry[column] = reader[column].ToString();
                            }

                            yield return entry;
                        }
                    }
                }
            }
        }
    }
}