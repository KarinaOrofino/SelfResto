using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using RHSP.Framework;
using KO.Framework.Utils;
using KO.Framework.Web;
using System.Reflection;

namespace KO.Web.Controllers
{
    [AllowAnonymous]
    public class APIController : ControllerBase
    {
        //protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //private IServicioAutenticacion ServicioAutenticacion { get; set; }
        //private IConfiguration Configuration { get; set; }

        //public APIController(IServicioAutenticacion servicioAutenticaion, IConfiguration configuration)
        //{
        //    this.ServicioAutenticacion = servicioAutenticaion;
        //    this.Configuration = configuration;
        //}

     }
}
