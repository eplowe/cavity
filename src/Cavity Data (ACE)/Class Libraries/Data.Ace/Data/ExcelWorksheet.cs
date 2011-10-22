namespace Cavity.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.Data.SqlClient;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using Cavity.Collections;

    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "This isn't a collection.")]
    public sealed class ExcelWorksheet : IEnumerable<KeyStringDictionary>
    {
        public ExcelWorksheet(FileInfo info, string name)
        {
            if (null == info)
            {
                throw new ArgumentNullException("info");
            }

            if (null == name)
            {
                throw new ArgumentNullException("name");
            }

            if (0 == name.Length)
            {
                throw new ArgumentOutOfRangeException("name");
            }

            Info = info;
            Name = name;
        }

        public FileInfo Info { get; private set; }

        public string Name { get; private set; }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities", Justification = "Can't parameterise the table name, so have quoted it.")]
        public IEnumerator<KeyStringDictionary> GetEnumerator()
        {
            Info.Refresh();
            if (!Info.Exists)
            {
                throw new FileNotFoundException(Info.FullName);
            }

            using (var connection = new OleDbConnection(Excel.ConnectionString(Info)))
            {
                connection.Open();
#if NET20
                var sql = StringExtensionMethods.FormatWith("SELECT * FROM {0}", new SqlCommandBuilder().QuoteIdentifier(Name));
#else
                var sql = "SELECT * FROM {0}".FormatWith(new SqlCommandBuilder().QuoteIdentifier(Name));
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

                        var columns = new List<string>();
                        foreach (DataRow row in schema.Rows)
                        {
#if NET20
                            columns.Add(row["ColumnName"].ToString());
#else
                            columns.Add(row.Field<string>("ColumnName"));
#endif
                        }

                        while (reader.Read())
                        {
                            var entry = new KeyStringDictionary();
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