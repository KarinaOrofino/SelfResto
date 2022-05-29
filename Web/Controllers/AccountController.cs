using Framework.Common;
using Framework.Utils;
using Framework.Web;
using KO.Entidades;
using KO.Framework.Web;
using KO.Recursos;
using KO.Servicios.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Web.Models.Account;

namespace Web.Controllers.Account
{
    public class AccountController : BaseController
    {
        #region Propiedades
        private IServicioAutenticacion ServicioAutenticacion { get; set; }
        private IConfiguration Configuration { get; set; }

        private IServicioGenerico ServicioGenerico { get; set; }
        private IServicioLogEventos ServicioLogEventos { get; set; }

        

        public AccountController(IServicioAutenticacion servicioAutenticacion,
                                 IConfiguration configuration,
                                 IServicioGenerico servicioGenerico,
                                 IServicioLogEventos servicioLogEventos)
        {
            this.ServicioAutenticacion = servicioAutenticacion;
            this.Configuration = configuration;
            this.ServicioGenerico = servicioGenerico;
            this.ServicioLogEventos = servicioLogEventos;
        }
        #endregion

        #region Get
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            try
            {
                await HttpContext.SignOutAsync(this.Configuration[Constantes.IDAPP].ToString());

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
            
            return View("Login", new LoginViewModel());
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            JsonData jsonData = new();

            try
            {
                int idUser = UserUtils.GetId(User);
                string nombreUsuario = ServicioGenerico.GetById<Usuario>(idUser)?.Login ?? "Usuario no se encuentra en la base de datos";
                string nombreEvento = "Log Out";

                JsonApiData apiResponse = this.ServicioLogEventos.LogInfo(nombreUsuario, this.Configuration[Constantes.IDAPP].ToString(), nombreEvento);
                if (apiResponse.result == JsonApiData.Result.Error)
                {
                    jsonData.content = new { mensaje = Global.MensajeCredencialesInvalidas };
                    jsonData.result = JsonData.Result.Error;
                    return Json(jsonData);
                }

                await HttpContext.SignOutAsync(this.Configuration[Constantes.IDAPP].ToString());
            }
            catch (Exception ex)
            {
                log.Error(ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
                jsonData.result = JsonData.Result.Error;
                jsonData.errorUi = Global.ErrorGenerico;
            }
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
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            JsonData jsonData = new();
            List<string> mensajes = new();
            try
            {
                if (!ModelState.IsValid)
                {
                   
                    foreach (var modelState in ModelState.Values)
                    {
                        foreach (var error in modelState.Errors)
                        {
                            mensajes.Add(error.ErrorMessage);
                        }
                    }
                    return Json(new JsonData() { content = new { mensajes }, result = JsonData.Result.Error });
                }

                JsonApiData apiResponse = this.ServicioAutenticacion.AutenticarUsuarioAplicacion(model.Usuario,
                                                                                                 model.Password,
                                                                                                 this.Configuration[Constantes.IDAPP].ToString());
                if (apiResponse.result == JsonApiData.Result.Error)
                {
                    mensajes.Add(Global.MensajeCredencialesInvalidas);

                    return Json(new JsonData() { content = new { mensajes }, result = JsonData.Result.Error });
                }

                var usuario = ObtenerUsuarioInterno(apiResponse);

                string rol = apiResponse.content.rol.ToString();
                var identity = new ClaimsIdentity(new[] {
                            new Claim(ClaimTypes.Sid, usuario.Id.ToString()),
                            new Claim(ClaimTypes.Name, usuario.Nombre),
                            new Claim(ClaimTypes.Role, rol),
                            new Claim(Constantes.CLAIMS_PERMISOS, JsonConvert.SerializeObject(ObtenerFuncionalidadesPermitidas(rol)))
                        }, this.Configuration[Constantes.IDAPP].ToString());

                var principal = new ClaimsPrincipal(identity);
                var login = HttpContext.SignInAsync(this.Configuration[Constantes.IDAPP].ToString(), principal);

            }
            catch (Exception ex)
            {
                log.Error(ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
                jsonData.result = JsonData.Result.Error;
                jsonData.errorUi = Global.ErrorGenerico;
            }
            return Json(new JsonData() { result = JsonData.Result.Ok });
        }

        #endregion
        
        #region Métodos
        /// <summary>
        /// Obtiene el usuario interno de Cestas a partir del id del usuario de SGAA retornado por la 
        /// consulta a la API. Si el servicio de SGAA lo autentica correctamente y no se encuentra 
        /// en la base de Cestas, el usuario se inserta
        /// </summary>
        /// <param name="apiResponse"></param>
        /// <returns></returns>
        private Usuario ObtenerUsuarioInterno(JsonApiData apiResponse)
        {
           var usuario = this.ServicioGenerico.Get<Usuario>(u => u.IdUsuarioSGAA == int.Parse(apiResponse.content.id.ToString()));

            if (usuario == null)
            {
                var usuarioNuevo = new Usuario() 
                                   { 
                                     Activo = true,
                                     IdUsuarioSGAA = apiResponse.content.id,
                                     Nombre = apiResponse.content.nombre.ToString(),
                                     Email = apiResponse.content.email.ToString(),
                                     Login = apiResponse.content.login.ToString(),
                                   };

                this.ServicioGenerico.Add<Usuario>(usuarioNuevo);

                usuario = usuarioNuevo;
            }

            return usuario;
        }

        private IList<int> ObtenerFuncionalidadesPermitidas(string rolDesc)
        {
            int idRol = ResolverRol(rolDesc);
            return this.ServicioGenerico.GetAll<FuncionalidadRol>(func => func.IdRol == idRol && func.IdTipoAcceso == (int)TipoAccesoEnum.AccesoTotal).Select(funcRol => funcRol.Funcionalidad.Id).ToList();
        }

        private static int ResolverRol(string rolDesc)
        {
            return rolDesc switch
            {
                Constantes.KO_OPERARIO => 1,
                Constantes.KO_ADMINISTRADOR => 4,
                _ => 2,
            };
        }

        #endregion
          


    }
}