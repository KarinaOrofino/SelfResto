//using Framework.Common;
//using Framework.Utils;
//using Framework.Web;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using KO.Entidades;
//using KO.Recursos;
//using KO.Servicios.Interfaces;
//using KO.Web.Filters;
//using KO.Web.Models.Funcionalidad;
//using Servicios.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace KO.Web.Controllers
//{
//    [Route("[controller]/[action]")]
//    public class FuncionalidadController : BaseController
//    {
//        #region Propiedades de servicio
//        public FuncionalidadController(IServicioGenerico servicioGenerico, IServicioFuncionalidad servicioFuncionalidad, IServicioRoles servicioRoles, IConfiguration configuration)
//        {
//            this._servicioGenerico = servicioGenerico;
//            this._servicioFuncionalidad = servicioFuncionalidad;
//            this._servicioRoles = servicioRoles;
//            this.Configuration = configuration;
//        }
//        private IServicioGenerico _servicioGenerico { get; set; }

//        private IServicioFuncionalidad _servicioFuncionalidad { get; set; }

//        private IServicioRoles _servicioRoles { get; set; }

//        private IConfiguration Configuration { get; set; }


//        #endregion

//        #region Paginas        

//        [HttpGet]
//        public IActionResult Listado()
//        {
//            return View();
//        }

//        [HttpGet]
//        public IActionResult Detalle()
//        {
//            try
//            {
//                var funcionalidadVM = new FuncionalidadViewModel();

//                funcionalidadVM.activo = true;

//                return View(funcionalidadVM);

//            }
//            catch (Exception ex)
//            {
//                log.Error(ex);
//                return Redirect("/Home/Error");
//            }

//        }

//        [Route("{id}")]
//        [HttpGet]
//        public IActionResult Detalle(int id)
//        {
//            var funcionalidadVM = new FuncionalidadViewModel();

//            IList<FuncionalidadRol> listafuncionalidadesRoles = _servicioGenerico.GetAll<FuncionalidadRol>(rel => rel.IdFuncionalidad == id).ToList();

//            try
//            {
//                Funcionalidad funcionalidad = _servicioGenerico.GetById<Funcionalidad>(id);

//                this.CargarVMdesdeEntidad(funcionalidadVM, funcionalidad, listafuncionalidadesRoles);
//            }
//            catch (Exception ex)
//            {
//                log.Error("$Error al buscar la guía con id=" + id + ", Error:", ex);
//                return Redirect("/Home/Error");
//            }

//            return View(funcionalidadVM);
//        }
//        #endregion

//        #region Metodos 
//        [HttpPost]
//        public JsonResult ObtenerFuncionalidades(string descripcion, bool? activo) 
//        {
//            JsonData jsonData = new JsonData();

//            if (descripcion == null)
//            {
//                descripcion = "";
//            }

//            List<Funcionalidad> funcionalidades = new List<Funcionalidad>();

//            try
//            {
//                funcionalidades = _servicioGenerico.GetAll<Funcionalidad>(f =>
//                (f.Descripcion.ToLower().Contains(descripcion.ToLower()))
//                && (!activo.HasValue || f.Activo == activo.Value)
//                ).ToList();

//                var gridData = funcionalidades.Select(funcionalidad => new FuncionalidadViewModel()
//                {
//                    id = funcionalidad.Id,
//                    descripcion = funcionalidad.Descripcion,
//                    activo = funcionalidad.Activo,

//                }).OrderBy(g => g.descripcion);

//                jsonData.content = gridData;
//                jsonData.result = JsonData.Result.Ok;
//            }

//            catch (Exception ex)
//            {
//                log.Error("No se pudo obtener la lista de Funcionalidades con los siguientes parámetros de búsqueda: descripcion: " + descripcion + ", activo: " + activo, ex);
//                Response.StatusCode = Constantes.ERROR_HTTP;
//                jsonData.result = JsonData.Result.Error;
//                jsonData.errorUi = Global.ErrorGenerico;
//            }

//            return Json(jsonData);

//        }

//        public JsonResult ObtenerRolesxFuncionalidad(int idFuncionalidad)
//        {
//            JsonData jsonData = new JsonData();
//            Funcionalidad funcionalidad = _servicioGenerico.GetById<Funcionalidad>(idFuncionalidad);

//            try
//            {
//                List<FuncionalidadRol> funcRol = _servicioGenerico.GetAll<FuncionalidadRol>(fr => fr.IdFuncionalidad == idFuncionalidad).ToList();
//                //var gridData = funcRol.Select(fr => new FuncionalidadRolViewModel()
//                //{
//                //    idFuncionalidad= fr.IdFuncionalidad,
//                //    nombreFuncionalidad = fr.Funcionalidad.Descripcion,
//                //    idRol= fr.IdRol,
//                //    nombreRol = ObtenerRoles().Where(r=>r.Id == fr.IdRol).FirstOrDefault().Descripcion,
//                //    idTipoAcceso = fr.IdTipoAcceso,
//                //    nombreTipoAcceso = _servicioGenerico.GetById<TipoAcceso>(fr.IdTipoAcceso).Descripcion

//                //}).OrderBy(n => n.nombreFuncionalidad);

//                jsonData.content = gridData;
//                jsonData.result = JsonData.Result.Ok;
//            }

//            catch (Exception ex)
//            {
//                log.Error(ex);
//                Response.StatusCode = Constantes.ERROR_HTTP;
//                jsonData.result = JsonData.Result.Error;
//                jsonData.errorUi = Global.ErrorGenerico;
//            }

//            return Json(jsonData);
//        }

//        [HttpGet]
//        public Task<JsonData> Inactivar(int funcionalidadId)
//        {
//            JsonData jsonData = new JsonData();

//            try
//            {
//                Funcionalidad funcionalidad = _servicioGenerico.GetById<Funcionalidad>(funcionalidadId);

//                //_servicioGenerico.Deactivate<Funcionalidad>(funcionalidad);

//                jsonData.result = JsonData.Result.Ok;

//            }
//            catch (Exception ex)
//            {
//                log.Error("$Error al inactivar la Funcionalidad con id=" + funcionalidadId + ", Error:", ex);
//                Response.StatusCode = Constantes.ERROR_HTTP;
//                jsonData.result = JsonData.Result.Error;
//                jsonData.errorUi = Global.ErrorGenerico;
//            }

//            return Task.FromResult(jsonData);
//        }

//        [HttpPost]
//        public Task<JsonData> Guardar(FuncionalidadViewModel funcionalidadVM)
//        {
//            IList<string> mensajes = new List<string>();
//            JsonData jsonData = new JsonData();

//            try
//            {
//                if (!ModelState.IsValid)
//                {
//                    this.CargarErroresModelo(mensajes, jsonData);
//                    Response.StatusCode = Constantes.ERROR_HTTP;
//                    return Task.FromResult(jsonData);
//                }

//                var esAlta = !funcionalidadVM.id.HasValue;

//                var appval = this._servicioGenerico.Get<Funcionalidad>(funcionalidad => funcionalidad.Descripcion == funcionalidadVM.descripcion);
//                if (esAlta && appval != null)
//                {
//                    jsonData.result = JsonData.Result.ModelValidation;
//                    jsonData.errorUi = Global.PorFavorRevise;

//                    jsonData.errors = new { Nombre = $"El nombre '{funcionalidadVM.descripcion}' ya existe." };

//                    Response.StatusCode = Constantes.ERROR_HTTP;
//                    return Task.FromResult(jsonData);
//                }

//                Funcionalidad funcionalidad = esAlta ? new Funcionalidad() : this._servicioGenerico.GetById<Funcionalidad>(funcionalidadVM.id.Value);

//                this.CargarEntidadDesdeVM(funcionalidad, funcionalidadVM);

//                List<FuncionalidadRol> rolesxFuncionalidadAModificar= new List<FuncionalidadRol>();

//                if (funcionalidadVM.listaRolesxFuncionalidad != null)
//                {
//                    rolesxFuncionalidadAModificar = funcionalidadVM.listaRolesxFuncionalidad.Select(funcRol => new FuncionalidadRol()
//                    {
//                        IdFuncionalidad = funcRol.idFuncionalidad,
//                        IdRol = funcRol.idRol,
//                        IdTipoAcceso = funcRol.idTipoAcceso,

//                    }).ToList();
//                }

//                if (esAlta)
//                {
//                    try
//                    {
//                        funcionalidad.IdUsuarioAlta = UserUtils.GetId(User);
//                        this._servicioFuncionalidad.AgregarFuncionalidad(funcionalidad, rolesxFuncionalidadAModificar);
//                    }
//                    catch (Exception ex)
//                    {
//                        log.Error("No se pudo dar de alta la nueva Funcionalidad", ex);
//                    }
//                }
//                else
//                {
//                    try
//                    {
//                        funcionalidad.IdUsuarioModificacion = UserUtils.GetId(User);
//                        this._servicioFuncionalidad.ModificarFuncionalidad(funcionalidad, rolesxFuncionalidadAModificar);
//                    }
//                    catch (Exception ex)
//                    {
//                        log.Error("No se pudieron actualizar los datos de la funcionalidad con id: " + funcionalidadVM.id + " Error: ", ex);
//                    }

//                }

//                jsonData.result = JsonData.Result.Ok;
//            }
//            catch (Exception ex)
//            {
//                log.Error(ex);
//                Response.StatusCode = Constantes.ERROR_HTTP;
//                jsonData.result = JsonData.Result.Error;
//                jsonData.errorUi = Global.ErrorGenerico;
//            }

//            return Task.FromResult(jsonData);

//        }

//        [HttpGet]
//        public IActionResult Exportar(string descripcion, bool? activo)
//        {
//            if (descripcion == null)
//            {
//                descripcion = "";
//            }

//            try
//            {
//                List<Funcionalidad> funcionalidad = _servicioGenerico.GetAll<Funcionalidad>(f =>
//                (f.Descripcion.ToLower().Contains(descripcion.ToLower()))
//                && (!activo.HasValue || f.Activo == activo.Value)).ToList();

//                List<FuncionalidadRol> fRol = _servicioGenerico.GetAll<FuncionalidadRol>().Where(f =>
//                 (funcionalidad.FirstOrDefault(x => f.IdFuncionalidad == x.Id) != null)).ToList();
//                var listaFuncionalidades = fRol.Select(fr => new
//                {
//                    Funcionalidad = fr.Funcionalidad.Descripcion,
//                    Estado = (funcionalidad.FirstOrDefault(x => x.Id == fr.IdFuncionalidad).Activo) == true ? Global.Activa : Global.Inactiva,
//                    Rol = ObtenerRoles().Where(r => r.Id == fr.IdRol).FirstOrDefault().Descripcion,
//                    TipoAcceso = _servicioGenerico.GetById<TipoAcceso>(fr.IdTipoAcceso).Descripcion
//                }).OrderBy(f => f.Funcionalidad).ToList();

//                log.Info("Método GetAll OK");

                
//                var fileBytes = CreateExcelFile.CreateExcelDocumentAsByte(listaFuncionalidades);
//                log.Info("Método crear excel OK");
//                this.HttpContext.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
//                this.HttpContext.Response.Headers.Add("content-disposition", "attachment");
//                return File(fileBytes, this.HttpContext.Response.ContentType);
//            }
//            catch (Exception ex)
//            {
//                this.HttpContext.Response.StatusCode = Constantes.ERROR_HTTP;
//                log.Error(ex);
//            }

//            return Content(string.Empty);
//        }

//        #endregion

//        #region Privados
//        private void CargarEntidadDesdeVM(Funcionalidad funcionalidad, FuncionalidadViewModel funcionalidadVM)
//        {
//            funcionalidad.Descripcion = funcionalidadVM.descripcion;
//            funcionalidad.Activo = funcionalidadVM.activo;
//        }

//        private void CargarVMdesdeEntidad(FuncionalidadViewModel funcionalidadVM, Funcionalidad funcionalidad, IList<FuncionalidadRol> listaRolesxFuncionalidad)
//        {
//            funcionalidadVM.id = funcionalidad.Id;
//            funcionalidadVM.descripcion = funcionalidad.Descripcion;
//            funcionalidadVM.activo = funcionalidad.Activo;
//            funcionalidadVM.listaRolesxFuncionalidad = listaRolesxFuncionalidad.Select(func => new FuncionalidadRolViewModel()
//            {
//                idFuncionalidad = func.IdFuncionalidad,
//                nombreFuncionalidad = func.Funcionalidad.Descripcion,
//                idRol = func.IdRol,
//                nombreRol = ObtenerRoles().Where(r=>r.Id == func.IdRol).FirstOrDefault().Descripcion,
//                idTipoAcceso = func.IdTipoAcceso,
//                nombreTipoAcceso = _servicioGenerico.GetById<TipoAcceso>(func.IdTipoAcceso).Descripcion,
//            }).OrderBy(f => f.nombreFuncionalidad).ToList();

//        }
 
//        #endregion
//    }
//}

