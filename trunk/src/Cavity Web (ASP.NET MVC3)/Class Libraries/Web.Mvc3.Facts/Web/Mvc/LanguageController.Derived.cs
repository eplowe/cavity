namespace Cavity.Web.Mvc
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Web.Mvc;

    using Cavity.Globalization;

    public sealed class DerivedLanguageController : LanguageController
    {
        public override IEnumerable<Language> Languages
        {
            get
            {
                yield return "en";
            }
        }

        [ContentNegotiation(".html", "*/*, text/*, text/html")]
        public ActionResult HtmlRepresentation(CultureInfo language)
        {
            return null;
        }
    }
}