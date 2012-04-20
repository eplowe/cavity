namespace Cavity.Data
{
    using System;
    using System.Data;

    using HtmlAgilityPack;

    using Xunit;
    using Xunit.Extensions;

    public sealed class HtmlDocumentExtensionMethodsFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(HtmlDocumentExtensionMethods).IsStatic());
        }

        [Fact]
        public void op_TabularData_HtmlDocumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => (null as HtmlDocument).TabularData());
        }

        [Theory]
        [HtmlFile("tabular.html")]
        public void op_TabularData_HtmlDocument_whenCaption(HtmlDocument html)
        {
            var table = html.TabularData().Tables["Caption"];

            Assert.Equal(1, table.Rows.Count);
            Assert.Equal("data", table.Rows[0].Field<HtmlNode>(0).InnerText);
        }

        [Theory]
        [HtmlFile("tabular.html")]
        public void op_TabularData_HtmlDocument_whenCellColumnSpanning(HtmlDocument html)
        {
            var table = html.TabularData().Tables["cell-colspan"];

            Assert.Equal(2, table.Rows.Count);
            Assert.Equal("1A", table.Rows[0].Field<HtmlNode>(0).InnerText);
            Assert.Equal("1B", table.Rows[0].Field<HtmlNode>(1).InnerText);
            Assert.Equal("1C", table.Rows[0].Field<HtmlNode>(2).InnerText);
            Assert.Equal("1D", table.Rows[0].Field<HtmlNode>(3).InnerText);
            Assert.Equal("2A", table.Rows[1].Field<HtmlNode>("Column A").InnerText);
            Assert.Equal("2B + 2C", table.Rows[1].Field<HtmlNode>("Column B").InnerText);
            Assert.Equal("2B + 2C", table.Rows[1].Field<HtmlNode>("Column C").InnerText);
            Assert.Equal("2D", table.Rows[1].Field<HtmlNode>("Column D").InnerText);
        }

        [Theory]
        [HtmlFile("tabular.html")]
        public void op_TabularData_HtmlDocument_whenDuplicateId(DataSet data)
        {
            Assert.NotNull(data.Tables["duplicate"]);
        }

        [Theory]
        [HtmlFile("tabular.html")]
        public void op_TabularData_HtmlDocument_whenEmptyRow(HtmlDocument html)
        {
            var table = html.TabularData().Tables["empty-row"];

            Assert.Empty(table.Rows);
        }

        [Theory]
        [HtmlFile("tabular.html")]
        public void op_TabularData_HtmlDocument_whenEmptyTable(HtmlDocument html)
        {
            var table = html.TabularData().Tables["empty-table"];

            Assert.Empty(table.Rows);
        }

        [Theory]
        [HtmlFile("tabular.html")]
        public void op_TabularData_HtmlDocument_whenHeadingColumnSpanning(HtmlDocument html)
        {
            var table = html.TabularData().Tables["heading-colspan"];

            Assert.Equal(2, table.Rows.Count);
            Assert.Equal("1A", table.Rows[0].Field<HtmlNode>(0).InnerText);
            Assert.Equal("1B", table.Rows[0].Field<HtmlNode>(1).InnerText);
            Assert.Equal("1C", table.Rows[0].Field<HtmlNode>(2).InnerText);
            Assert.Equal("1D", table.Rows[0].Field<HtmlNode>(3).InnerText);
            Assert.Equal("2A", table.Rows[1].Field<HtmlNode>("Column A").InnerText);
            Assert.Equal("2B", table.Rows[1].Field<HtmlNode>("Columns B & C (1)").InnerText);
            Assert.Equal("2C", table.Rows[1].Field<HtmlNode>("Columns B & C (2)").InnerText);
            Assert.Equal("2D", table.Rows[1].Field<HtmlNode>("Column D").InnerText);
        }

        [Theory]
        [HtmlFile("tabular.html")]
        public void op_TabularData_HtmlDocument_whenId(HtmlDocument html)
        {
            var table = html.TabularData().Tables["example"];

            Assert.Equal(2, table.Rows.Count);

            Assert.Equal("1B", table.Rows[0].Field<HtmlNode>(1).InnerText);
            Assert.Equal("2A", table.Rows[1].Field<HtmlNode>("Column A").InnerText);
        }

        [Theory]
        [HtmlFile("tabular.html")]
        public void op_TabularData_HtmlDocument_whenVertical(HtmlDocument html)
        {
            var table = html.TabularData().Tables["vertical"];

            Assert.Equal(2, table.Rows.Count);
            Assert.Equal("1A", table.Rows[0].Field<HtmlNode>(0).InnerText);
            Assert.Equal("2A", table.Rows[1].Field<HtmlNode>(0).InnerText);
            Assert.Equal("2B", table.Rows[1].Field<HtmlNode>("Value B").InnerText);
        }

        [Theory]
        [HtmlFile("tabular.html")]
        public void op_TabularData_HtmlDocument_whenVerticalCellRowSpanning(HtmlDocument html)
        {
            var table = html.TabularData().Tables["vertical-cell-rowspan"];

            Assert.Equal(2, table.Rows.Count);
            Assert.Equal("1A", table.Rows[0].Field<HtmlNode>(0).InnerText);
            Assert.Equal("2A", table.Rows[1].Field<HtmlNode>(0).InnerText);
            Assert.Equal("1B + 1C", table.Rows[0].Field<HtmlNode>("Value B").InnerText);
            Assert.Equal("2B", table.Rows[1].Field<HtmlNode>("Value B").InnerText);
            Assert.Equal("1B + 1C", table.Rows[0].Field<HtmlNode>("Value C").InnerText);
            Assert.Equal("2C", table.Rows[1].Field<HtmlNode>("Value C").InnerText);
            Assert.Equal("1D", table.Rows[0].Field<HtmlNode>("Value D").InnerText);
            Assert.Equal("2D", table.Rows[1].Field<HtmlNode>("Value D").InnerText);
        }

        [Theory]
        [HtmlFile("tabular.html")]
        public void op_TabularData_HtmlDocument_whenVerticalHeadingRowSpanning(HtmlDocument html)
        {
            var table = html.TabularData().Tables["vertical-heading-rowspan"];

            Assert.Equal(2, table.Rows.Count);
            Assert.Equal("1A", table.Rows[0].Field<HtmlNode>(0).InnerText);
            Assert.Equal("2A", table.Rows[1].Field<HtmlNode>(0).InnerText);
            Assert.Equal("1B", table.Rows[0].Field<HtmlNode>("Value B & C (1)").InnerText);
            Assert.Equal("2B", table.Rows[1].Field<HtmlNode>("Value B & C (1)").InnerText);
            Assert.Equal("1C", table.Rows[0].Field<HtmlNode>("Value B & C (2)").InnerText);
            Assert.Equal("2C", table.Rows[1].Field<HtmlNode>("Value B & C (2)").InnerText);
            Assert.Equal("1D", table.Rows[0].Field<HtmlNode>("Value D").InnerText);
            Assert.Equal("2D", table.Rows[1].Field<HtmlNode>("Value D").InnerText);
        }

        [Theory]
        [HtmlFile("tabular.html")]
        public void op_TabularData_HtmlDocument_withoutId(HtmlDocument html)
        {
            var table = html.TabularData().Tables[1];

            Assert.Equal(1, table.Rows.Count);

            Assert.Equal("_<em>_</em>_", table.Rows[0].Field<HtmlNode>(0).InnerHtml);
        }

        [Theory]
        [HtmlFile("example.html")]
        public void op_TabularData_HtmlDocument_withoutTables(HtmlDocument html)
        {
            Assert.Empty(html.TabularData().Tables);
        }
    }
}