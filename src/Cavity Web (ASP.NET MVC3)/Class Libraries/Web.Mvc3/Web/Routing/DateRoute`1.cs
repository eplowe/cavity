namespace Cavity.Web.Routing
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    public class DateRoute<T> : RouteBase
        where T : Controller
    {
        public DateRoute(string extension, 
                         string action)
        {
            Extension = extension;
            Action = action;
        }

        private static string Controller
        {
            get
            {
                return typeof(T).Name.RemoveFromEnd("Controller", StringComparison.Ordinal);
            }
        }

        private string Action { get; set; }

        private string Extension { get; set; }

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            RouteData result = null;

            var route = GetRoute(httpContext);
            if (null != route)
            {
                result = route.GetRouteData(httpContext);
            }

            return result;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, 
                                                       RouteValueDictionary values)
        {
            if (null == requestContext)
            {
                throw new ArgumentNullException("requestContext");
            }

            VirtualPathData result = null;

            var route = GetRoute(requestContext.HttpContext);
            if (null != route)
            {
                result = route.GetVirtualPath(requestContext, values);
            }

            return result;
        }

        private Route GetRoute(HttpContextBase httpContext)
        {
            if (null == httpContext)
            {
                throw new ArgumentNullException("httpContext");
            }

            var path = httpContext.Request.Path;
            if (11 > path.Length)
            {
                return null;
            }

            if ('-' != path[5] ||
                '-' != path[8])
            {
                return null;
            }

            if (new[]
            {
                1, 2, 3, 4, 6, 7, 9, 10
            }.Any(index => !char.IsDigit(path[index])))
            {
                return null;
            }

            var date = path.Substring(1, 10).TryTo<DateTime?>();
            if (!date.HasValue)
            {
                return null;
            }

            if (!string.Equals(Extension, path.Substring(11), StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            return new Route(path.Substring(1), new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(new
                {
                    controller = Controller, 
                    action = Action, 
                    Date = date
                }), 
                Constraints = new RouteValueDictionary()
            };
        }
    }
}