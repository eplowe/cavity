namespace Cavity.Views.NotFound
{
    using RazorGenerator.Testing;
    using Xunit;

    public sealed class HtmlRepresentationFacts
    {
        [Fact]
        public void op_RenderAsHtml()
        {
            string expected = AlphaDecimal.Random();

            var view = new HtmlRepresentation();
            view.ViewBag.Message = expected;
            var html = view.RenderAsHtml();

            var actual = html.DocumentNode.SelectSingleNode("//p[@id='message']").InnerText;

            Assert.Equal(expected, actual);
        }
    }
}