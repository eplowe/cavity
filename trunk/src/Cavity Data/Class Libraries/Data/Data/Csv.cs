namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
#if !NET20
    using System.Linq;
#endif
    using System.Text;

    using Cavity.Collections;

    public static class Csv
    {
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

    }
}