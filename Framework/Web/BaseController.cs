using log4net;
using KO.Entidades;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Framework.Web
{
    public class BaseController : Controller
    {
        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected string Paso;
        public Usuario ObtenerUsuarioLogueado()
        {
            return null;/*new Usuario { Id = int.Parse(User.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).SingleOrDefault()) };            */
        }

        protected void CargarErroresModelo(IList<string> mensajes, JsonData jsonData)
        {            
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    mensajes.Add(error.ErrorMessage);
                }
            }

            jsonData.content = new { mensajes = mensajes };
            jsonData.result = JsonData.Result.Error;
        }
    }
}

