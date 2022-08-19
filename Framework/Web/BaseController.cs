using log4net;
using KO.Entities;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Framework.Web
{
    public class BaseController : Controller
    {
        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        //public User ObtenerUsuarioLogueado()
        //{
        //    return null;
        //}

        protected void LoadModelErrors(IList<string> messages, JsonData jsonData)
        {            
            foreach (var modelState in ModelState.Values)
            {
                foreach (var error in modelState.Errors)
                {
                    messages.Add(error.ErrorMessage);
                }
            }

            jsonData.content = new { mensajes = messages };
            jsonData.result = JsonData.Result.Error;
        }
    }
}

