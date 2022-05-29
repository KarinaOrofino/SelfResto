﻿using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using RHSP.Framework;
using KO.Framework.Utils;
using KO.Framework.Web;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;
using System;
using Framework.Common;
using KO.Servicios.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Security.Principal;
using Framework.Utils;
using System.Security.Claims;


namespace KO.Web.Controllers
{
    [AllowAnonymous]
    public class APIController : ControllerBase
    {
        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private IServicioAutenticacion ServicioAutenticacion { get; set; }
        private IConfiguration Configuration { get; set; }

        public APIController(IServicioAutenticacion servicioAutenticaion, IConfiguration configuration)
        {
            this.ServicioAutenticacion = servicioAutenticaion;
            this.Configuration = configuration;
        }

     }
}