using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Linq;

namespace KO.Framework.Web
{
    public class CheckSession : IActionFilter
    {
        private readonly List<string> ignoreRoutes = new() {
            "Account/Login",
            "Account/GetAllTables",
            "Home/ObtenerVersion",
            "Home/JSGlobales",
        };

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string routeFrom = filterContext.ActionDescriptor.RouteValues["controller"] + "/" +
                               filterContext.ActionDescriptor.RouteValues["action"];

            if (ignoreRoutes.Any(ir => ir == routeFrom))
            {
                return;
            }

            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var request = filterContext.HttpContext.Request;
                bool isAjax = request.Headers["X-Requested-With"].Any();

                if (isAjax)
                {
                    var host = request.Host.ToUriComponent();
                    var pathBase = request.PathBase.ToUriComponent();

                    filterContext.HttpContext.Response.ContentType = "application/json; charset=utf-8";
                    filterContext.Result = new JsonResult(new
                    {
                        result = -1,
                        redirect = "https://" + $"{request.Scheme}://{host}{pathBase}" + "/Account/Login",
                        error = "[Sesion vencida]",
                        errorUi = "[Sesion vencida]"
                    });
                }
                else
                {
                    RouteValueDictionary redirect = new();
                    redirect.Add("action", "Login");
                    redirect.Add("controller", "Account");
                    filterContext.Result = new RedirectToRouteResult(redirect);
                }
            }
        }
    }
}
