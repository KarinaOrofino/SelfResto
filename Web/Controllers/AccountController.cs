using Framework.Common;
using Framework.Web;
using KO.Entities;
using KO.Resources;
using KO.Services.Implementations;
using KO.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using Web.Models.Account;

namespace Web.Controllers.Account
{
    public class AccountController : BaseController
    {
        #region Propiedades
        private IConfiguration Configuration { get; set; }

        private IUsersService IUsersService { get; set; }

        public AccountController(IConfiguration configuration, IUsersService usersService)
        {
            this.Configuration = configuration;
            this.IUsersService = usersService;

        }
        #endregion

        #region Get
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            try
            {
                //await HttpContext.SignOutAsync(this.Configuration[Constants.IDAPP].ToString());

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
            
            return View("Login", new UserViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            //JsonData jsonData = new();

            //try
            //{
            //    int idUser = UserUtils.GetId(User);
            //    string nombreUsuario = ServicioGenerico.GetById<User>(idUser)?.Login ?? "Usuario no se encuentra en la base de datos";
            //    string nombreEvento = "Log Out";

            //    JsonApiData apiResponse = this.ServicioLogEventos.LogInfo(nombreUsuario, this.Configuration[Constants.IDAPP].ToString(), nombreEvento);
            //    if (apiResponse.result == JsonApiData.Result.Error)
            //    {
            //        jsonData.content = new { mensaje = Global.MensajeCredencialesInvalidas };
            //        jsonData.result = JsonData.Result.Error;
            //        return Json(jsonData);
            //    }

            //    await HttpContext.SignOutAsync(this.Configuration[Constants.IDAPP].ToString());
            //}
            //catch (Exception ex)
            //{
            //    log.Error(ex);
            //    Response.StatusCode = Constants.ERROR_HTTP;
            //    jsonData.result = JsonData.Result.Error;
            //    jsonData.errorUi = Global.ErrorGenerico;
            //}
            return Json(new JsonData() { result = JsonData.Result.Ok });
        }

        [HttpGet]
        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }

        #endregion

        #region Post
        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel userVM)
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
                    jsonData.content.messages = messages;
                    jsonData.result = JsonData.Result.Error;
                    return Json(jsonData);
                }

                User user = IUsersService.GetByEmail(userVM.Email);

                if (user != null || !user.Active)
                {
                    if (user.Password != userVM.Password)
                    {
                        jsonData.content = new { mensaje = Global.MsgIncorrectPassword };
                        jsonData.result = JsonData.Result.Error;
                        return Json(jsonData);
                    }
                }
                else
                {
                    jsonData.content = new { mensaje = Global.MsgNotAUser };
                    jsonData.result = JsonData.Result.Error;
                    return Json(jsonData);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                Response.StatusCode = Constants.ERROR_HTTP;
                jsonData.result = JsonData.Result.Error;
                jsonData.errorUi = Global.GenericError;
            }
            return Redirect("/MenuItems/List");
        }

        #endregion
    }
}
