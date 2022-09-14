using Framework.Utils;
using Framework.Web;
using Microsoft.AspNetCore.Mvc;
using KO.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using KO.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using KO.Entities;
using KO.Web.Models.Order;

namespace Web.Controllers.Home
{
    [Route("[controller]/[action]")]
    public class HomeController : BaseController
    {
        private IConfiguration Configuration { get; set; }

        private IUsersService IUsersService { get; set; }

        private IGenericService IGenericService { get; set; }

        private readonly IAppInfo _appInfo;

        public HomeController(IConfiguration configuration, IUsersService usersService, IAppInfo appInfo, IGenericService GenericService)
        {
            _appInfo = appInfo;
            this.Configuration = configuration;
            this.IUsersService = usersService;
            this.IGenericService = GenericService;
        }


        [Route("{id}")]
        [HttpGet]
        public IActionResult IndexClient(int id)
        {
            Order order = IGenericService.GetById<Order>(id);

            OrderViewModel orderVM = new();
            orderVM.Id = order.Id;
            orderVM.TableId = order.TableId;

            return View(orderVM);
        }

        [HttpGet]
        public IActionResult IndexEmployee()
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



        public Task<JsonData> JSGlobales(/*string cultura*/)
        {
            JsonData jsonData = new ();

            try
            {
                ResourceManager MyResourceClass = new(typeof(Global));
                ResourceSet resourceSet = MyResourceClass.GetResourceSet(CultureInfo.CurrentUICulture, true, true);


                List<KeyValuePair<string, string>> data = new();
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

            JsonData jsonData = new ();

            try
            {

                jsonData.content = "build: " + _appInfo.GetVersion() + " - " + _appInfo.GetBuildDate();
                jsonData.result = JsonData.Result.Ok;
            }
            catch (Exception ex)
            {
                log.Error(Global.GenericError, ex);
                jsonData.result = JsonData.Result.Error;
                jsonData.error = ex.ToString();
            }

            return Task.FromResult(jsonData);
        }

    }
}
