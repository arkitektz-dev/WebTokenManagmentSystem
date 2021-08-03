using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace WebAppQueueManagmentSystem.Cors
{
    public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
    {
        private string[] _domains;

        public AllowCrossSiteJsonAttribute(params string[] domains)
        {
            _domains = domains;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var context = filterContext.RequestContext.HttpContext;

            if (context.Request.UrlReferrer != null)
            {
                var host = context.Request.UrlReferrer.ToString();

                for (int i = 0; i < _domains.Length; i++)
                {
                    if (host.Contains(_domains[i]))
                    {
                        context.Response.AddHeader("Access-Control-Allow-Origin", "*");
                        break;
                    }
                }
            }
            else
            {
                context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            }

            context.Response.AddHeader("Access-Control-Allow-Methods", "GET, HEAD, OPTIONS, POST, PUT");
            context.Response.AddHeader("Access-Control-Allow-Headers",
                "Access-Control-Allow-Headers, " +
                "Origin,Accept, X-Requested-With, " +
                "Content-Type, Access-Control-Request-Method, " +
                "Access-Control-Request-Headers");

            base.OnActionExecuting(filterContext);
        }
    }
}