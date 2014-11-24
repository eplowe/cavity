namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
#if !NET20
    using System.Linq;
#endif
#if NET20 || NET35
    using System.Web;
#else
    using System.Net;
#endif
    using System.Xml;
#if NET20
    using Cavity.Collections;
#endif
    using Cavity.Collections.Generic;
    using HtmlAgilityPack;

    public static class HtmlDocumentExtensionMethods
    {
#if NET20
        public static DataSet TabularData(HtmlDocument obj)
#else
        public static DataSet TabularData(this HtmlDocument obj)
#endif
        {
            if (null == obj)
            {
                throw new ArgumentNullException("obj");
            }

            var result = new DataSet
                             {
                                 Locale = CultureInfo.InvariantCulture
                             };

            var tables = obj.DocumentNode.SelectNodes("//table");
            if (null == tables)
            {
                return result;
            }

            foreach (var table in tables)
            {
                AddDataTable(result, table);
            }

            return result;
        }

        private static void AddDataTable(DataSet data,
                                         HtmlNode table)
        {
            var result = new DataTable
                             {
                                 Locale = CultureInfo.InvariantCulture
                             };

            var id = table.Attributes["id"];
            if (null != id)
            {
                var name = id.Value;
                if (null != data.Tables[name])
                {
                    return;
                }

                result.TableName = name;
            }
            else
            {
#if NET20
                var caption = IEnumerableExtensionMethods.FirstOrDefault(table.Descendants("caption"));
#else
                var caption = table.Descendants("caption").FirstOrDefault();
#endif
                if (null != caption)
                {
                    result.TableName = caption.InnerText;
                }
            }

            FillDataTable(result, table);

            data.Tables.Add(result);
        }

        private static void AddNormalDataRows(DataTable obj,
                                              IEnumerable<HtmlNode> rows)
        {
            foreach (var row in rows)
            {
                obj.Rows.Add(FillNormalDataRow(obj.NewRow(), row));
            }
        }

        private static void AddVerticalDataColumns(DataTable obj,
                                                   HtmlNode body)
        {
            var i = -1;
            var span = 1;
            foreach (var row in body.Descendants("tr"))
            {
                i++;
                if (span > 1)
                {
                    span--;
                    continue;
                }

#if NET20
                var heading = IEnumerableExtensionMethods.FirstOrDefault(row.Descendants("th"));
#else
                var heading = row.Descendants("th").FirstOrDefault();
#endif
                if (null == heading)
                {
                    obj.Columns.Add(XmlConvert.ToString(i), typeof(HtmlNode));
                    continue;
                }

                var attribute = heading.Attributes["rowspan"];
                span = null == attribute ? 1 : XmlConvert.ToInt32(attribute.Value);
#if NET20 || NET35
                var text = HttpUtility.HtmlDecode(heading.InnerText);
#else
                var text = WebUtility.HtmlDecode(heading.InnerText);
#endif
                if (1 == span)
                {
                    obj.Columns.Add(text, typeof(HtmlNode));
                    continue;
                }

                for (var s = 0; s < span; s++)
                {
#if NET20
                    obj.Columns.Add(StringExtensionMethods.FormatWith("{0} ({1})", text, s + 1), typeof(HtmlNode));
#else
                    obj.Columns.Add("{0} ({1})".FormatWith(text, s + 1), typeof(HtmlNode));
#endif
                }
            }
        }

        private static void AddVerticalDataRows(DataTable obj,
                                                HtmlNode body)
        {
#if NET20
            var rows = IEnumerableExtensionMethods.ToList(body.Descendants("tr"));
            var cells = 0;
            foreach (var row in rows)
            {
                cells = Math.Max(cells, IEnumerableExtensionMethods.Count(row.Descendants("td")));
            }
#else
            var rows = body.Descendants("tr").ToList();
            var cells = rows
                .Select(row => row.Descendants("td").Count())
                .Concat(new[] { 0 })
                .Max();
#endif

            for (var i = 0; i < cells; i++)
            {
                obj.Rows.Add(obj.NewRow());
            }

            var column = 0;
            foreach (var item in rows)
            {
                var current = column;
                var row = -1;
                foreach (var cell in item.Descendants("td"))
                {
                    row++;
                    for (var c = current; c < obj.Columns.Count; c++)
                    {
                        if (DBNull.Value == obj.Rows[row][current])
                        {
                            break;
                        }

                        row++;
                    }

                    var attribute = cell.Attributes["rowspan"];
                    var span = null == attribute ? 1 : XmlConvert.ToInt32(attribute.Value);
                    if (1 == span)
                    {
                        obj.Rows[row][current] = cell;
                        continue;
                    }

                    for (var s = 0; s < span; s++)
                    {
                        obj.Rows[row][current + s] = cell;
                    }
                }

                column++;
            }
        }

        private static void FillDataTable(DataTable obj,
                                          HtmlNode table)
        {
#if NET20
            var body = 0 != IEnumerableExtensionMethods.Count(table.Descendants("thead"))
                           ? IEnumerableExtensionMethods.First(table.Descendants("tbody"))
                           : table;
            var rows = IEnumerableExtensionMethods.ToList(body.Descendants("tr"));
#else
            var body = table.Descendants("thead").Any()
                           ? table.Descendants("tbody").First()
                           : table;
            var rows = body.Descendants("tr").ToList();
#endif
            if (HasVerticalColumns(table))
            {
                AddVerticalDataColumns(obj, body);
                AddVerticalDataRows(obj, body);
                return;
            }

            var columns = Columns(table);
            for (var i = 0; i < columns.Count; i++)
            {
#if NET20 || NET35
                var name = string.IsNullOrEmpty(columns[i])
#else
                var name = string.IsNullOrWhiteSpace(columns[i])
#endif
                               ? XmlConvert.ToString(i)
                               : columns[i];
                obj.Columns.Add(name, typeof(HtmlNode));
            }

            AddNormalDataRows(obj, rows);
        }

        private static DataRow FillNormalDataRow(DataRow obj,
                                                 HtmlNode row)
        {
#if NET20
            var cells = IEnumerableExtensionMethods.ToList(row.Descendants("td"));
#else
            var cells = row.Descendants("td").ToList();
#endif

            if (0 == cells.Count)
            {
                return obj;
            }

            var i = 0;
            foreach (var cell in cells)
            {
                var attribute = cell.Attributes["colspan"];
                var span = null == attribute ? 1 : XmlConvert.ToInt32(attribute.Value);
                for (var s = 0; s < span; s++)
                {
                    obj[i++] = cell;
                }
            }

            return obj;
        }

        private static bool HasVerticalColumns(HtmlNode table)
        {
#if NET20
            if (0 != IEnumerableExtensionMethods.Count(table.Descendants("thead")))
#else
            if (table.Descendants("thead").Any())
#endif
            {
                return false;
            }

#if NET20
            var row = IEnumerableExtensionMethods.FirstOrDefault(table.Descendants("tr"));
#else
            var row = table.Descendants("tr").FirstOrDefault();
#endif
            if (null == row)
            {
                return false;
            }

#if NET20
            return 0 != IEnumerableExtensionMethods.Count(row.Descendants("th"));
#else
            return row.Descendants("th").Any();
#endif
        }

        private static IList<string> Columns(HtmlNode table)
        {
#if NET20
            var head = IEnumerableExtensionMethods.FirstOrDefault(table.Descendants("thead"));
#else
            var head = table.Descendants("thead").FirstOrDefault();
#endif

            return null == head ? ColumnsRow(table) : ColumnsHead(head);
        }

        private static IList<string> ColumnsHead(HtmlNode head)
        {
#if NET20
            var rows = IEnumerableExtensionMethods.ToList(head.Descendants("tr"));
#else
            var rows = head.Descendants("tr").ToList();
#endif
            if (0 == rows.Count)
            {
                return new List<string>();
            }

            var matrix = new Matrix<string>();
            foreach (var cell in rows[0].Descendants("th"))
            {
                var colspan = cell.Attributes["colspan"];
                if (null == colspan)
                {
                    matrix.Width++;
                    continue;
                }

                for (var i = 0; i < XmlConvert.ToInt32(colspan.Value); i++)
                {
                    matrix.Width++;
                }
            }

            var carry = new List<int>();
            for (var i = 0; i < matrix.Width; i++)
            {
                carry.Add(0);
            }

            var y = 0;
            foreach (var row in rows)
            {
                var x = 0;
                matrix.Height++;
                foreach (var cell in row.Descendants("th"))
                {
                    while (0 != carry[x])
                    {
                        matrix[x, y] = matrix[x, y - 1];
                        carry[x]--;
                        x++;
                    }

                    var rowspan = cell.Attributes["rowspan"];
                    if (null != rowspan)
                    {
                        carry[x] = XmlConvert.ToInt32(rowspan.Value);
                    }

                    var colspan = cell.Attributes["colspan"];
                    var name = ColumnName(cell);
                    var index = 1;
                    for (var i = 0; i < (null == colspan ? 1 : XmlConvert.ToInt32(colspan.Value)); i++)
                    {
                        matrix[x++, y] = string.Format(CultureInfo.InvariantCulture, null == colspan ? "{0}" : "{0} ({1})", name, index++);
                    }
                }

                y++;
            }

#if NET20
            var list = new List<string>();
            foreach (var element in matrix.Row(matrix.Height - 1))
            {
                list.Add(element);
            }

            return list;
#else
            return matrix.Row(matrix.Height - 1).ToList();
#endif
        }

        private static IList<string> ColumnsRow(HtmlNode table)
        {
            var list = new List<string>();
#if NET20
            var row = IEnumerableExtensionMethods.FirstOrDefault(table.Descendants("tr"));
#else
            var row = table.Descendants("tr").FirstOrDefault();
#endif
            if (null == row)
            {
                return list;
            }

#if NET20
            var headers = IEnumerableExtensionMethods.ToList(row.Descendants("th"));
#else
            var headers = row.Descendants("th").ToList();
#endif
            if (0 == headers.Count)
            {
#if NET20
                headers = IEnumerableExtensionMethods.ToList(row.Descendants("td"));
#else
                headers = row.Descendants("td").ToList();
#endif
            }

#if NET20
            foreach (var header in headers)
            {
                list.Add(ColumnName(header));
            }
#else
            list.AddRange(headers.Select(ColumnName));
#endif
            return list;
        }

        private static string ColumnName(HtmlNode header)
        {
            var id = header.Attributes["id"];
#if NET20 || NET35
            return null == id ? HttpUtility.HtmlDecode(header.InnerText) : id.Value;
#else
            return null == id ? WebUtility.HtmlDecode(header.InnerText) : id.Value;
#endif
        }
    }
}