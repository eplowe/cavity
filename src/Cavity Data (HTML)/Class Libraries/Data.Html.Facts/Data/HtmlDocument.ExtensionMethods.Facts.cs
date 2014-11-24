namespace Cavity.Data
{
    using System;
    using System.Data;
    using System.IO;
    using HtmlAgilityPack;
    using Xunit;

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

        [Fact]
        public void op_TabularData_HtmlDocument_whenCaption()
        {
            var html = new HtmlDocument();
            html.Load(new FileInfo("tabular-caption.html").FullName);

            var table = html.TabularData().Tables["Caption"];

            Assert.Equal(1, table.Rows.Count);
            Assert.Equal("data", table.Rows[0].Field<HtmlNode>(0).InnerText);
        }

        [Fact]
        public void op_TabularData_HtmlDocument_whenCellColumnSpanning()
        {
            var html = new HtmlDocument();
            html.Load(new FileInfo("tabular-cell-colspan.html").FullName);

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

        [Fact]
        public void op_TabularData_HtmlDocument_whenDuplicateId()
        {
            var html = new HtmlDocument();
            html.Load(new FileInfo("tabular-duplicate.html").FullName);

            Assert.NotNull(html.TabularData().Tables["duplicate"]);
        }

        [Fact]
        public void op_TabularData_HtmlDocument_whenEmptyRow()
        {
            var html = new HtmlDocument();
            html.Load(new FileInfo("tabular-empty-row.html").FullName);

            var table = html.TabularData().Tables["empty-row"];

            Assert.Empty(table.Rows);
        }

        [Fact]
        public void op_TabularData_HtmlDocument_whenEmptyTable()
        {
            var html = new HtmlDocument();
            html.Load(new FileInfo("tabular-empty-table.html").FullName);

            var table = html.TabularData().Tables["empty-table"];

            Assert.Empty(table.Rows);
        }

        [Fact]
        public void op_TabularData_HtmlDocument_whenHeadingAmpersand()
        {
            var html = new HtmlDocument();
            html.Load(new FileInfo("tabular-heading-ampersand.html").FullName);

            var table = html.TabularData().Tables["heading-ampersand"];

            Assert.Equal(2, table.Rows.Count);
            Assert.Equal("1A", table.Rows[0].Field<HtmlNode>(0).InnerText);
            Assert.Equal("2A", table.Rows[1].Field<HtmlNode>("This & That").InnerText);
        }

        [Fact]
        public void op_TabularData_HtmlDocument_whenHeadingColumnId()
        {
            var html = new HtmlDocument();
            html.Load(new FileInfo("tabular-heading-id.html").FullName);

            var table = html.TabularData().Tables["heading-id"];

            Assert.Equal(2, table.Rows.Count);

            Assert.Equal("1A", table.Rows[0].Field<HtmlNode>(0).InnerText);
            Assert.Equal("2A", table.Rows[1].Field<HtmlNode>("A").InnerText);
        }

        [Fact]
        public void op_TabularData_HtmlDocument_whenHeadingColumnSpanning()
        {
            var html = new HtmlDocument();
            html.Load(new FileInfo("tabular-heading-colspan.html").FullName);

            var table = html.TabularData().Tables["heading-colspan"];

            Assert.Equal(2, table.Rows.Count);
            Assert.Equal("1A", table.Rows[0].Field<HtmlNode>(0).InnerText);
            Assert.Equal("1B", table.Rows[0].Field<HtmlNode>(1).InnerText);
            Assert.Equal("1C", table.Rows[0].Field<HtmlNode>(2).InnerText);
            Assert.Equal("1D", table.Rows[0].Field<HtmlNode>(3).InnerText);
            Assert.Equal("2A", table.Rows[1].Field<HtmlNode>("Column A").InnerText);
            Assert.Equal("2B", table.Rows[1].Field<HtmlNode>("Columns B, C (1)").InnerText);
            Assert.Equal("2C", table.Rows[1].Field<HtmlNode>("Columns B, C (2)").InnerText);
            Assert.Equal("2D", table.Rows[1].Field<HtmlNode>("Column D").InnerText);
        }

        [Fact]
        public void op_TabularData_HtmlDocument_whenHeadingColumnText()
        {
            var html = new HtmlDocument();
            html.Load(new FileInfo("tabular-heading-text.html").FullName);

            var table = html.TabularData().Tables["heading-text"];

            Assert.Equal(2, table.Rows.Count);

            Assert.Equal("1A", table.Rows[0].Field<HtmlNode>(0).InnerText);
            Assert.Equal("2A", table.Rows[1].Field<HtmlNode>("Column A").InnerText);
        }

        [Fact]
        public void op_TabularData_HtmlDocument_whenHeadingComplex()
        {
            var html = new HtmlDocument();
            html.Load(new FileInfo("tabular-heading-complex.html").FullName);

            var table = html.TabularData().Tables["heading-complex"];

            Assert.Equal(2, table.Rows.Count);
            Assert.Equal("Friday, January 01, 2010", table.Rows[0].Field<HtmlNode>(0).InnerText);
            Assert.Equal("26.6", table.Rows[0].Field<HtmlNode>(1).InnerText);
            Assert.Equal("31.8", table.Rows[0].Field<HtmlNode>(2).InnerText);
            Assert.Equal("35.8", table.Rows[0].Field<HtmlNode>(3).InnerText);
            Assert.Equal("Saturday, December 31, 2011", table.Rows[1].Field<HtmlNode>("date").InnerText);
            Assert.Equal("40.5", table.Rows[1].Field<HtmlNode>("minimum-temperature").InnerText);
            Assert.Equal("52.8", table.Rows[1].Field<HtmlNode>("mean-temperature").InnerText);
            Assert.Equal("55.4", table.Rows[1].Field<HtmlNode>("maximum-temperature").InnerText);
        }

        [Fact]
        public void op_TabularData_HtmlDocument_whenUnnamed()
        {
            var html = new HtmlDocument();
            html.Load(new FileInfo("tabular-unnamed.html").FullName);

            var table = html.TabularData().Tables[0];

            Assert.Equal(1, table.Rows.Count);

            Assert.Equal("_<em>_</em>_", table.Rows[0].Field<HtmlNode>(0).InnerHtml);
        }

        [Fact]
        public void op_TabularData_HtmlDocument_whenVertical()
        {
            var html = new HtmlDocument();
            html.Load(new FileInfo("tabular-vertical.html").FullName);

            var table = html.TabularData().Tables["vertical"];

            Assert.Equal(2, table.Rows.Count);
            Assert.Equal("1A", table.Rows[0].Field<HtmlNode>(0).InnerText);
            Assert.Equal("2A", table.Rows[1].Field<HtmlNode>(0).InnerText);
            Assert.Equal("2B", table.Rows[1].Field<HtmlNode>("Value B").InnerText);
        }

        [Fact]
        public void op_TabularData_HtmlDocument_whenVerticalCellRowSpanning()
        {
            var html = new HtmlDocument();
            html.Load(new FileInfo("tabular-vertical-cell-rowspan.html").FullName);

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

        [Fact]
        public void op_TabularData_HtmlDocument_whenVerticalHeadingRowSpanning()
        {
            var html = new HtmlDocument();
            html.Load(new FileInfo("tabular-vertical-heading-rowspan.html").FullName);

            var table = html.TabularData().Tables["vertical-heading-rowspan"];

            Assert.Equal(2, table.Rows.Count);
            Assert.Equal("1A", table.Rows[0].Field<HtmlNode>(0).InnerText);
            Assert.Equal("2A", table.Rows[1].Field<HtmlNode>(0).InnerText);
            Assert.Equal("1B", table.Rows[0].Field<HtmlNode>("Value B, C (1)").InnerText);
            Assert.Equal("2B", table.Rows[1].Field<HtmlNode>("Value B, C (1)").InnerText);
            Assert.Equal("1C", table.Rows[0].Field<HtmlNode>("Value B, C (2)").InnerText);
            Assert.Equal("2C", table.Rows[1].Field<HtmlNode>("Value B, C (2)").InnerText);
            Assert.Equal("1D", table.Rows[0].Field<HtmlNode>("Value D").InnerText);
            Assert.Equal("2D", table.Rows[1].Field<HtmlNode>("Value D").InnerText);
        }

        [Fact]
        public void op_TabularData_HtmlDocument_withoutTables()
        {
            var html = new HtmlDocument();
            html.Load(new FileInfo("tabular-empty.html").FullName);

            Assert.Empty(html.TabularData().Tables);
        }
    }
}