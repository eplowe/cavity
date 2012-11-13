namespace Cavity.Text
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Xml;

    using Cavity;

    public static class Html
    {
        private static readonly IDictionary<string, string> _entities = LoadEntities();

        public static IDictionary<string, string> Entities
        {
            get
            {
                return _entities;
            }
        }

        public static string Decode(string value)
        {
            var result = new MutableString(WebUtility.HtmlDecode(value));
            foreach (var entity in _entities)
            {
                result.Replace(entity.Key, entity.Value);
            }

            return result;
        }

        private static IDictionary<string, string> LoadEntities()
        {
            var entities = new Dictionary<string, string>(StringComparer.Ordinal);
            for (var i = 32; i < 100; i++)
            {
                entities.Add("&#0{0};".FormatWith(i), XmlConvert.ToString((char)i));
            }

            for (var i = 32; i < 127; i++)
            {
                entities.Add("&#{0};".FormatWith(i), XmlConvert.ToString((char)i));
            }

            for (var i = 160; i < 256; i++)
            {
                entities.Add("&#{0};".FormatWith(i), XmlConvert.ToString((char)i));
            }

            entities.Add("&quot;", "\"");
            entities.Add("&amp;", "&");
            entities.Add("&minus;", "-");
            entities.Add("&lt;", "<");
            entities.Add("&gt;", ">");
            entities.Add("&nbsp;", " ");
            entities.Add("&iexcl;", "¡");
            entities.Add("&cent;", "¢");
            entities.Add("&pound;", "£");
            entities.Add("&curren;", "¤");
            entities.Add("&yen;", "¥");
            entities.Add("&brvbar;", "¦");
            entities.Add("&sect;", "§");
            entities.Add("&uml;", "¨");
            entities.Add("&copy;", "©");
            entities.Add("&ordf;", "ª");
            entities.Add("&laquo;", "«");
            entities.Add("&not;", "¬");
            entities.Add("&shy;", string.Empty);
            entities.Add("&reg;", "®");
            entities.Add("&macr;", "¯");
            entities.Add("&deg;", "°");
            entities.Add("&plusmn;", "±");
            entities.Add("&sup2;", "²");
            entities.Add("&sup3;", "³");
            entities.Add("&acute;", "´");
            entities.Add("&micro;", "µ");
            entities.Add("&para;", "¶");
            entities.Add("&middot;", "·");
            entities.Add("&cedil;", "¸");
            entities.Add("&sup1;", "¹");
            entities.Add("&ordm;", "º");
            entities.Add("&raquo;", "»");
            entities.Add("&frac14;", "¼");
            entities.Add("&frac12;", "½");
            entities.Add("&frac34;", "¾");
            entities.Add("&iquest;", "¿");
            entities.Add("&Agrave;", "À");
            entities.Add("&Aacute;", "Á");
            entities.Add("&Acirc;", "Â");
            entities.Add("&Atilde;", "Ã");
            entities.Add("&Auml;", "Ä");
            entities.Add("&Aring;", "Å");
            entities.Add("&AElig;", "Æ");
            entities.Add("&Ccedil;", "Ç");
            entities.Add("&Egrave;", "È");
            entities.Add("&Eacute;", "É");
            entities.Add("&Ecirc;", "Ê");
            entities.Add("&Euml;", "Ë");
            entities.Add("&Igrave;", "Ì");
            entities.Add("&Iacute;", "Í");
            entities.Add("&Icirc;", "Î");
            entities.Add("&Iuml;", "Ï");
            entities.Add("&ETH;", "Ð");
            entities.Add("&Ntilde;", "Ñ");
            entities.Add("&Ograve;", "Ò");
            entities.Add("&Oacute;", "Ó");
            entities.Add("&Ocirc;", "Ô");
            entities.Add("&Otilde;", "Õ");
            entities.Add("&Ouml;", "Ö");
            entities.Add("&times;", "×");
            entities.Add("&Oslash;", "Ø");
            entities.Add("&Ugrave;", "Ù");
            entities.Add("&Uacute;", "Ú");
            entities.Add("&Ucirc;", "Û");
            entities.Add("&Uuml;", "Ü");
            entities.Add("&Yacute;", "Ý");
            entities.Add("&THORN;", "Þ");
            entities.Add("&szlig;", "ß");
            entities.Add("&agrave;", "à");
            entities.Add("&aacute;", "á");
            entities.Add("&acirc;", "â");
            entities.Add("&atilde;", "ã");
            entities.Add("&auml;", "ä");
            entities.Add("&aring;", "å");
            entities.Add("&aelig;", "æ");
            entities.Add("&ccedil;", "ç");
            entities.Add("&egrave;", "è");
            entities.Add("&eacute;", "é");
            entities.Add("&ecirc;", "ê");
            entities.Add("&euml;", "ë");
            entities.Add("&igrave;", "ì");
            entities.Add("&iacute;", "í");
            entities.Add("&icirc;", "î");
            entities.Add("&iuml;", "ï");
            entities.Add("&eth;", "ð");
            entities.Add("&ntilde;", "ñ");
            entities.Add("&ograve;", "ò");
            entities.Add("&oacute;", "ó");
            entities.Add("&ocirc;", "ô");
            entities.Add("&otilde;", "õ");
            entities.Add("&ouml;", "ö");
            entities.Add("&divide;", "÷");
            entities.Add("&oslash;", "ø");
            entities.Add("&ugrave;", "ù");
            entities.Add("&uacute;", "ú");
            entities.Add("&ucirc;", "û");
            entities.Add("&uuml;", "ü");
            entities.Add("&yacute;", "v");
            entities.Add("&thorn;", "þ");
            entities.Add("&yuml;", "ÿ");
            entities.Add("&#338;", "Œ");
            entities.Add("&#339;", "œ");
            entities.Add("&#352;", "Š");
            entities.Add("&#353;", "š");
            entities.Add("&#376;", "Ÿ");
            entities.Add("&#402;", "ƒ");
            entities.Add("&#8211;", "–");
            entities.Add("&#8212;", "—");
            entities.Add("&#8216;", "‘");
            entities.Add("&#8217;", "’");
            entities.Add("&#8218;", "‚");
            entities.Add("&#8220;", "“");
            entities.Add("&#8221;", "”");
            entities.Add("&#8222;", "„");
            entities.Add("&#8224;", "†");
            entities.Add("&#8225;", "‡");
            entities.Add("&#8226;", "•");
            entities.Add("&#8230;", "…");
            entities.Add("&#8240;", "‰");
            entities.Add("&#8364;", "€");
            entities.Add("&euro;", "€");
            entities.Add("&#8482;", "™");

            return entities;
        }
    }
}