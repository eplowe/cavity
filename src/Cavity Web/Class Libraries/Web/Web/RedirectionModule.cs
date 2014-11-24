namespace Cavity.Web
{
    using System;
    using System.Net;
    using System.Web;
    using Cavity.Configuration;

    public sealed class RedirectionModule : IHttpModule
    {
        public void OnBeginRequest(object sender,
                                   EventArgs e)
        {
            if (null == sender)
            {
                throw new ArgumentNullException("sender");
            }

            var application = (HttpApplication)sender;

            var request = application.Request;
            var location = Config.Section<RedirectionConfigurationSection>("cavity.redirection").Redirect(request.Url);
            if (null == location)
            {
                return;
            }

            var response = application.Response;
            response.StatusCode = (int)HttpStatusCode.Found;
            response.AppendHeader("Location", location);
            response.Cache.SetCacheability(HttpCacheability.Public);
            response.Cache.SetExpires(DateTime.UtcNow.AddHours(1));
            response.End();
        }

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            if (null == context)
            {
                throw new ArgumentNullException("context");
            }

            context.BeginRequest += OnBeginRequest;
        }
    }
}