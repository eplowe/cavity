namespace Cavity.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Xml;

    using HtmlAgilityPack;

    public static class HtmlDocumentExtensionMethods
    {
        public static DataSet TabularData(this HtmlDocument obj)
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
                result.AddDataTable(table);
            }

            return result;
        }

        private static void AddDataTable(this DataSet data, 
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
                var caption = table.Descendants("caption").FirstOrDefault();
                if (null != caption)
                {
                    result.TableName = caption.InnerText;
                }
            }

            FillDataTable(result, table);

            data.Tables.Add(result);
        }

        private static void AddNormalDataColumns(this DataTable obj, 
                                                 IEnumerable<HtmlNode> rows)
        {
            var columns = rows
                .Select(row => row.Descendants("td").Count())
                .Concat(new[] { 0 })
                .Max();

            for (var i = 0; i < columns; i++)
            {
                obj.Columns.Add(XmlConvert.ToString(i), typeof(HtmlNode));
            }
        }

        private static void AddNormalDataRows(this DataTable obj, 
                                              IEnumerable<HtmlNode> rows)
        {
            foreach (var row in rows)
            {
                obj.Rows.Add(obj.NewRow().FillNormalDataRow(row));
            }
        }

        private static void AddVerticalDataColumns(this DataTable obj, 
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

                var heading = row.Descendants("th").FirstOrDefault();
                if (null == heading)
                {
                    obj.Columns.Add(XmlConvert.ToString(i), typeof(HtmlNode));
                    continue;
                }

                var attribute = heading.Attributes["rowspan"];
                span = null == attribute ? 1 : XmlConvert.ToInt32(attribute.Value);
                var text = WebUtility.HtmlDecode(heading.InnerText);
                if (1 == span)
                {
                    obj.Columns.Add(text, typeof(HtmlNode));
                    continue;
                }

                for (var s = 0; s < span; s++)
                {
                    obj.Columns.Add("{0} ({1})".FormatWith(text, s + 1), typeof(HtmlNode));
                }
            }
        }

        private static void AddVerticalDataRows(this DataTable obj, 
                                                HtmlNode body)
        {
            var rows = body.Descendants("tr").ToList();
            var cells = rows
                .Select(row => row.Descendants("td").Count())
                .Concat(new[] { 0 })
                .Max();

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
            var body = table.Descendants("thead").Any()
                           ? table.Descendants("tbody").First()
                           : table;
            var rows = body.Descendants("tr").ToList();

            if (table.HasVerticalColumns())
            {
                obj.AddVerticalDataColumns(body);
                obj.AddVerticalDataRows(body);
                return;
            }

            obj.AddNormalDataColumns(rows);
            obj.FillNormalDataColumns(table.Descendants("thead").FirstOrDefault());
            obj.AddNormalDataRows(rows);
        }

        private static void FillNormalDataColumns(this DataTable obj, 
                                                  HtmlNode head)
        {
            if (null == head)
            {
                return;
            }

            var rows = head.Descendants("tr").ToList();
            if (0 == rows.Count)
            {
                return;
            }

            var headings = rows.Last().Descendants("th").ToList();
            if (0 == headings.Count)
            {
                return;
            }

            var i = 0;
            foreach (var heading in headings)
            {
                var attribute = heading.Attributes["colspan"];
                var span = null == attribute ? 1 : XmlConvert.ToInt32(attribute.Value);
                var text = WebUtility.HtmlDecode(heading.InnerText);
                if (1 == span)
                {
                    obj.Columns[i++].ColumnName = text;
                    continue;
                }

                for (var s = 0; s < span; s++)
                {
                    obj.Columns[i++].ColumnName = "{0} ({1})".FormatWith(text, s + 1);
                }
            }
        }

        private static DataRow FillNormalDataRow(this DataRow obj, 
                                                 HtmlNode row)
        {
            var cells = row.Descendants("td").ToList();
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

        private static bool HasVerticalColumns(this HtmlNode table)
        {
            if (table.Descendants("thead").Any())
            {
                return false;
            }

            var row = table.Descendants("tr").FirstOrDefault();
            if (null == row)
            {
                return false;
            }

            return row.Descendants("th").Any();
        }
    }
}