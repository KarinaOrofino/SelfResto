using Framework.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System.Linq;

namespace KO.Web.Filters
{
    public class AutorizarAccionAttribute : TypeFilterAttribute
    {
        public AutorizarAccionAttribute(int permiso) : base(typeof(AutorizarAccionFilter))
        {
            Arguments = new object[] { permiso };
        }
    }

    public class AutorizarAccionFilter : IAuthorizationFilter
    {
        readonly int _permiso;

        public AutorizarAccionFilter(int permiso)
        {
            _permiso = permiso;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool tienePermiso = UserUtils.UsuarioTienePermiso(context.HttpContext.User, _permiso);
            if (!tienePermiso)
            {
                var request = context.HttpContext.Request;
                bool isAjax = request.Headers["X-Requested-With"].Any();

                if (isAjax)
                {
                    var host = request.Host.ToUriComponent();
                    var pathBase = request.PathBase.ToUriComponent();

                    context.HttpContext.Response.ContentType = "application/json; charset=utf-8";
                    context.Result = new JsonResult(new
                    {
                        result = -1,
                        redirect = "https://" + $"{request.Scheme}://{host}{pathBase}" + "/TrenLaminacion/TrenLaminacion",
                        error = "No tiene acceso al recurso solicitado",
                        errorUi = "No tiene acceso al recurso solicitado"
                    });
                }
                else
                {
                    RouteValueDictionary redirect = new();
                    redirect.Add("action", "TrenLaminacion");
                    redirect.Add("controller", "TrenLaminacion");
                    context.Result = new RedirectToRouteResult(redirect);
                }
            }
        }
    }
}
