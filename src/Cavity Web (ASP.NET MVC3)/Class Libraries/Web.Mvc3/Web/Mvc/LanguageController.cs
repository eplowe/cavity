namespace Cavity.Web.Mvc
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Web.Mvc;
    using Cavity.Globalization;

    public abstract class LanguageController : Controller
    {
        public abstract IEnumerable<Language> Languages { get; }

        public ActionResult ContentNegotiation(CultureInfo language)
        {
            if (null == language)
            {
                throw new ArgumentNullException("language");
            }

            if (CultureInfo.InvariantCulture == language)
            {
                throw new ArgumentOutOfRangeException("language");
            }

            language.SetCurrentCulture();

            Accept accept = Request.Headers["Accept"];

            return accept.Negotiate(Request, GetType());
        }

        public ActionResult LanguageNegotiation()
        {
            AcceptLanguage accept = Request.Headers["Accept-Language"];

            return accept.Negotiate(Request, GetType());
        }

        [SuppressMessage("Microsoft.Design", "CA1061:DoNotHideBaseClassMethods", Justification = "The base method is not marked virtual.")]
        public ViewResult View(CultureInfo language, 
                               object model)
        {
            language.SetCurrentCulture();

            return View(model);
        }
    }
}