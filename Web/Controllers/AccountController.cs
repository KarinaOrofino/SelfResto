using Framework.Common;
using Framework.Web;
using KO.Entities;
using KO.Framework.Web;
using KO.Resources;
using KO.Web.Models.Table;
using KO.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using KO.Web.Models.Account;

namespace Web.Controllers.Account
{
    public class AccountController : BaseController
    {
        #region Propiedades
        private IConfiguration Configuration { get; set; }

        private IAuthenticateService IAuthenticateService { get; set; }

        private IUsersService IUsersService { get; set; }

        private IGenericService IGenericService { get; set; }

        public AccountController(IConfiguration configuration, IUsersService usersService, IAuthenticateService AuthenticateService, IGenericService GenericService)
        {
            this.Configuration = configuration;
            this.IUsersService = usersService;
            this.IAuthenticateService = AuthenticateService;
            this.IGenericService = GenericService;

        }
        #endregion

        #region Get
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {

            try
            {
                await HttpContext.SignOutAsync(this.Configuration[Constants.IDAPP].ToString());

                //Limpio el IdentityUser y las cookies de autenticacion
                HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

                foreach (var cookie in Request.Cookies.Keys)
                {
                    Response.Cookies.Delete(cookie);
                }
                ViewData["ReturnUrl"] = returnUrl;

            }
            catch (Exception ex)
            {
                log.Error(ex);

            }

            return View("Login",new UserViewModel());
        }



        [HttpGet]
        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllTables()
        {
            JsonData jsonData = new JsonData();
            List<TableViewModel> tablesVM = new();

            try
            {

                List<Table> tables = IGenericService.GetAll<Table>(t => t.Active).ToList();
                List<Order> orders = IGenericService.GetAll<Order>(t => t.Active).ToList();
                //List<Table> tablesWithoutOpenOrder = tables.Where(tab => !orders.Any(o => o.TableId == tab.Id)).ToList();

                tablesVM = tables.Select(tab => new TableViewModel
                {
                    Id = tab.Id,
                    Number = tab.Number,
                }).ToList();

                jsonData.content = tablesVM;
                jsonData.result = JsonData.Result.Ok;
            }

            catch (Exception ex)
            {
                log.Error("No se pudieron obtener las mesas. Error: ", ex);
                Response.StatusCode = Constants.ERROR_HTTP;
                jsonData.result = JsonData.Result.Error;
                jsonData.errorUi = "No se pudieron obtener las mesas.";
            }

            return Json(jsonData);

        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel userVM, int tableId)
        {
            JsonData jsonData = new();
            List<string> messages = new();

            try
            {
                if (!ModelState.IsValid)
                {

                    foreach (var modelState in ModelState.Values)
                    {
                        foreach (var error in modelState.Errors)
                        {
                            messages.Add(error.ErrorMessage);
                        }
                    }

                    jsonData.result = JsonData.Result.Error;
                    return Json(jsonData);
                }

                JsonApiData apiResponse = IAuthenticateService.AutenticarUsuarioAplicacion(userVM.Email, userVM.Password);
                if (apiResponse.result == JsonApiData.Result.Error)
                {

                    jsonData.result = JsonData.Result.Error;
                    jsonData.errorUi = apiResponse.message;
                    return Json(jsonData);
                }
                apiResponse.content = JsonConvert.DeserializeObject<dynamic>(apiResponse.message);
                User user = IUsersService.GetByEmail(userVM.Email);

                var identity = new ClaimsIdentity(new[] {
                            new Claim(ClaimTypes.Sid, user.Id.ToString()),
                            new Claim(ClaimTypes.Name, user.Name),
                            new Claim(ClaimTypes.Surname, user.Surname), 
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Role, user.AccessTypeName),
                            new Claim(Constants.CLAIMS_PERMISOS, JsonConvert.SerializeObject(user.Access_Type))
                        }, this.Configuration[Constants.IDAPP].ToString());

                var principal = new ClaimsPrincipal(identity);
                var login = HttpContext.SignInAsync(this.Configuration[Constants.IDAPP].ToString(), principal);

                if(user.Access_Type == 10 ) { 

                    Order order = IGenericService.Get<Order>(o => o.Active && o.TableId == tableId);
                    Order newOrder = new();

                if (order == null) {
                   
                    newOrder.TableId = tableId;
                    newOrder.CreationUser = user.Id;
                    newOrder.CreationDate = DateTime.Now;
                    newOrder.Active = true;

                    IGenericService.Add(newOrder);
                    jsonData.content = newOrder.Id;
                    }
                

                else
                {
                    jsonData.content = order.Id;
                }
                

                jsonData.result = JsonData.Result.Ok;
                }

            }
            catch (Exception ex)
            {
                log.Error(ex);
                Response.StatusCode = Constants.ERROR_HTTP;
                jsonData.result = JsonData.Result.Error;
                jsonData.errorUi = Global.GenericError;
            }
            return Json(jsonData);
        }

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            JsonData jsonData = new();

            try
            {
                await HttpContext.SignOutAsync(this.Configuration[Constants.IDAPP].ToString());
            }
            catch (Exception ex)
            {
                log.Error(ex);
                Response.StatusCode = Constants.ERROR_HTTP;
                jsonData.result = JsonData.Result.Error;
                jsonData.errorUi = Global.GenericError;
            }
            return Json(new JsonData() { result = JsonData.Result.Ok });
        }
       

        #endregion
    }
}
