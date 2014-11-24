namespace Cavity.Text
{
    using Xunit;

    public sealed class HtmlFacts
    {
        [Fact]
        public void a_definition()
        {
            Assert.True(typeof(Html).IsStatic());
        }

        [Fact]
        public void op_Decode_string()
        {
            const string expected = "\"&−<> ¡¢£¤¥¦§¨©ª«¬­®¯°±²³´µ¶·¸¹º»¼½¾¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþÿŒœŠšŸƒ–—‘’‚“”„†‡•…‰€€™";
            var actual = Html.Decode("&quot;&amp;&minus;&lt;&gt;&nbsp;&iexcl;&cent;&pound;&curren;&yen;&brvbar;&sect;&uml;&copy;&ordf;&laquo;&not;&shy;&reg;&macr;&deg;&plusmn;&sup2;&sup3;&acute;&micro;&para;&middot;&cedil;&sup1;&ordm;&raquo;&frac14;&frac12;&frac34;&iquest;&Agrave;&Aacute;&Acirc;&Atilde;&Auml;&Aring;&AElig;&Ccedil;&Egrave;&Eacute;&Ecirc;&Euml;&Igrave;&Iacute;&Icirc;&Iuml;&ETH;&Ntilde;&Ograve;&Oacute;&Ocirc;&Otilde;&Ouml;&times;&Oslash;&Ugrave;&Uacute;&Ucirc;&Uuml;&Yacute;&THORN;&szlig;&agrave;&aacute;&acirc;&atilde;&auml;&aring;&aelig;&ccedil;&egrave;&eacute;&ecirc;&euml;&igrave;&iacute;&icirc;&iuml;&eth;&ntilde;&ograve;&oacute;&ocirc;&otilde;&ouml;&divide;&oslash;&ugrave;&uacute;&ucirc;&uuml;&yacute;&thorn;&yuml;&#338;&#339;&#352;&#353;&#376;&#402;&#8211;&#8212;&#8216;&#8217;&#8218;&#8220;&#8221;&#8222;&#8224;&#8225;&#8226;&#8230;&#8240;&#8364;&euro;&#8482;");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void prop_Entities_get()
        {
            const int expected = 382;
            var actual = Html.Entities.Count;

            Assert.Equal(expected, actual);
        }
    }
}