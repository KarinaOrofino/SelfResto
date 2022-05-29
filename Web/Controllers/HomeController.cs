using Framework.Utils;
using Framework.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KO.Recursos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Threading.Tasks;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IAppInfo _appInfo;

        public HomeController(ILogger<HomeController> logger, IAppInfo appInfo)
        {
            _logger = logger;
            _appInfo = appInfo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Account/Login");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Error()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Account/Login");
            }
            return View();
        }



        public Task<JsonData> JSGlobales(string cultura)
        {
            JsonData jsonData = new JsonData();

            try
            {
                ResourceManager MyResourceClass = new ResourceManager(typeof(Global));
                ResourceSet resourceSet = MyResourceClass.GetResourceSet(CultureInfo.CurrentUICulture, true, true);


                List<KeyValuePair<string, string>> data = new List<KeyValuePair<string, string>>();
                foreach (DictionaryEntry resourceItem in resourceSet)
                {
                    data.Add(new KeyValuePair<string, string>(resourceItem.Key.ToString(), resourceItem.Value.ToString()));
                }

                jsonData.content = data;
                jsonData.result = JsonData.Result.Ok;
            }
            catch (Exception ex)
            {
                log.Error(ex);
                jsonData.content = new { mensaje = ex };
                jsonData.result = JsonData.Result.Ok;
            }

            return Task.FromResult(jsonData);
        }

        public Task<JsonData> ObtenerVersion()
        {

            JsonData jsonData = new JsonData();

            try
            {

                jsonData.content = "build: " + _appInfo.GetVersion() + " - " + _appInfo.GetBuildDate();
                jsonData.result = JsonData.Result.Ok;
            }
            catch (Exception ex)
            {
                log.Error(Global.ErrorGenerico, ex);
                jsonData.result = JsonData.Result.Error;
                jsonData.error = ex.ToString();
            }

            return Task.FromResult(jsonData);
        }

    }
}
