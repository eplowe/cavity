namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
#if !NET20
    using System.Linq;
#endif
#if NET40
    using System.Net;
#else
    using System.Web;
#endif
    using System.Xml;

#if NET20
    using Cavity.Collections;
#endif

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

        private static void AddNormalDataColumns(DataTable obj, 
                                                 IEnumerable<HtmlNode> rows)
        {
#if NET20
            var columns = 0;
            foreach (var row in rows)
            {
                columns = Math.Max(columns, IEnumerableExtensionMethods.Count(row.Descendants("td")));
            }
#else
            var columns = rows
                .Select(row => row.Descendants("td").Count())
                .Concat(new[] { 0 })
                .Max();
#endif

            for (var i = 0; i < columns; i++)
            {
                obj.Columns.Add(XmlConvert.ToString(i), typeof(HtmlNode));
            }
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
#if NET40
                var text = WebUtility.HtmlDecode(heading.InnerText);
#else
                var text = HttpUtility.HtmlDecode(heading.InnerText);
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

            AddNormalDataColumns(obj, rows);
#if NET20
            FillNormalDataColumns(obj, IEnumerableExtensionMethods.FirstOrDefault(table.Descendants("thead")));
#else
            FillNormalDataColumns(obj, table.Descendants("thead").FirstOrDefault());
#endif
            AddNormalDataRows(obj, rows);
        }

        private static void FillNormalDataColumns(DataTable obj, 
                                                  HtmlNode head)
        {
            if (null == head)
            {
                return;
            }

#if NET20
            var rows = IEnumerableExtensionMethods.ToList(head.Descendants("tr"));
#else
            var rows = head.Descendants("tr").ToList();
#endif
            if (0 == rows.Count)
            {
                return;
            }

#if NET20
            var headings = IEnumerableExtensionMethods.ToList(IEnumerableExtensionMethods.Last(rows).Descendants("th"));
#else
            var headings = rows.Last().Descendants("th").ToList();
#endif
            if (0 == headings.Count)
            {
                return;
            }

            var i = 0;
            foreach (var heading in headings)
            {
                var attribute = heading.Attributes["colspan"];
                var span = null == attribute ? 1 : XmlConvert.ToInt32(attribute.Value);
#if NET40
                var text = WebUtility.HtmlDecode(heading.InnerText);
#else
                var text = HttpUtility.HtmlDecode(heading.InnerText);
#endif
                if (1 == span)
                {
                    obj.Columns[i++].ColumnName = text;
                    continue;
                }

                for (var s = 0; s < span; s++)
                {
#if NET20
                    obj.Columns[i++].ColumnName = StringExtensionMethods.FormatWith("{0} ({1})", text, s + 1);
#else
                    obj.Columns[i++].ColumnName = "{0} ({1})".FormatWith(text, s + 1);
#endif
                }
            }
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
    }
}