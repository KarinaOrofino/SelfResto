//using KO.Entidades;
//using KO.Recursos;
//using KO.Servicios.Interfaces;
//using KO.Web.Filters;
//using KO.Web.Models.Via;
//using Framework.Common;
//using Framework.Web;
//using Framework.Utils;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using KO.Web.Models.Reportes;

//namespace KO.Web.Controllers
//{
//    [Route("[controller]/[action]")]
//    public class ReportesController : BaseController
//    {
//        #region Propiedades de servicio
//        public ReportesController(IServicioGenerico servicioGenerico, IServicioReporteCargaCestas servicioReporteCargaCestas)
//        {
//            this._servicioGenerico = servicioGenerico;
//            this._servicioReporteCargaCestas = servicioReporteCargaCestas;
          
//        }
//        private IServicioGenerico _servicioGenerico { get; set; }
//        private IServicioReporteCargaCestas _servicioReporteCargaCestas { get; set; }
//        #endregion

//        [AutorizarAccion(Constantes.CDC_REPORTES_CARGAS)]
//        public ActionResult CargaDeCestas()
//        {
//            return View();
//        }

//        #region Metodos       
//        [HttpGet]
//        public JsonResult ObtenerVia()
//        {
//            JsonData jsonData = new JsonData();

//            try
//            {
//                List<Via> vias = _servicioGenerico.GetAll<Via>().ToList();

//                jsonData.content = vias.Select(v => new ViaViewModel()
//                {
//                    id = v.Id,
//                    nombre = v.Nombre.ToUpper()
//            })
//               .OrderBy(x => x.nombre);
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

//        public JsonResult obtenerColadaSinFecha()
//        {
//            JsonData jsonData = new JsonData();

//            try
//            {                
//                List<Carga> cargas = _servicioGenerico.GetAll<Carga>().GroupBy(c => c.IdColada).Select(grp => grp.First()).ToList();
 
//                List<Colada> coladas = _servicioGenerico.GetAll<Colada>().Join(cargas, co => co.Id,
//                                                                                      c => c.IdColada,
//                                                                                      (co, c) => co)
//                                                                            .Where(co => co.Nombre != "BACKUP").ToList();
           
//                var colada = coladas.Select(c => new filtroViewModel()
//                {
//                    id = c.Id,
//                    nombre = c.Nombre
//                })
//               .OrderBy(x => x.nombre).ToList();

//                jsonData.content = colada;
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

//        public JsonResult ObtenerEstado() 
//        {
//            JsonData jsonData = new JsonData();

//            try
//            {            
//                List<Colada> estado = _servicioGenerico.GetAll<Colada>().GroupBy(c => c.Chequeo).Select(co => co.FirstOrDefault()).ToList();

//                jsonData.content = estado.Select(e => new
//                {
//                    id = Convert.ToInt32(e.Chequeo).ToString(),//convertido en string para que 0 sea tomado como valor en el filter de v-chip, cuando es int lo toma como ''                
//                    nombre = (e.Chequeo == true ? "Chequeada" : "No chequeada").ToUpper()
//                    //Chequeada 1 // No chequeada 0                                                    
//                }).OrderBy(x => x.id);
                
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
        
//        [HttpPost]
//        public JsonResult ObtenerReporteCargaCestas(DateTime? fechaDesde, DateTime? fechaHasta, string idColadaInicial, string idColadaFinal, int? idEstado, string cesta, int? idVia)      
//        {
//            JsonData jsonData = new JsonData();
            
//            try
//            {
//                int? _idColadaInicial = idColadaInicial != null ? _servicioGenerico.Get<Colada>(co => co.Nombre == idColadaInicial).Id : null;
//                int? _idColadaFinal = idColadaFinal != null ? _servicioGenerico.Get<Colada>(co => co.Nombre == idColadaFinal).Id : null;          

//                List <ReporteCargaCestas> todosCargaCestas = _servicioReporteCargaCestas.ObtenerReporteCargaCestas(fechaDesde, fechaHasta, _idColadaInicial, _idColadaFinal, idEstado, cesta, idVia).ToList();

//                var gridData = todosCargaCestas.Select(cargaCestas => new ReporteCargaDeCestasViewModel()
//                { 
//                    Fecha = cargaCestas.Fecha,
//                    Colada = cargaCestas.Colada.Nombre,
//                    Cesta = cargaCestas.Cesta,
//                    RecetaCapa = cargaCestas.RecetaCapa,
//                    RecetaMaterial = cargaCestas.RecetaMaterial,
//                    RecetaPeso = cargaCestas.RecetaPeso,
//                    RealCapa = cargaCestas.RealCapa,
//                    RealMaterial = cargaCestas.RealMaterial,
//                    RealPeso = cargaCestas.RealPeso,
//                    RealHoraCarga = cargaCestas.RealHoraCarga,
//                    HoraFin = cargaCestas.HoraFin,
//                    Comentario = cargaCestas.Comentario,
//                    Estado = cargaCestas.Estado,
//                    Por = cargaCestas.Por,
//                    Via = cargaCestas.Via
//                });
                                
//                jsonData.content = gridData;
//                jsonData.result = JsonData.Result.Ok;
//            }

//            catch (Exception ex)
//            {
//                log.Error("No se pudo obtener la lista de Carga de Cestas con los siguientes parámetros de búsqueda: fecha de inicio: "
//                    + fechaDesde + ", fecha fin: " + fechaHasta + ", colada inicial: " + idColadaInicial + ", colada final: " + idColadaFinal + ", estado: " + idEstado + ", cesta: " + cesta + ", via: " + idVia, ex);
//                Response.StatusCode = Constantes.ERROR_HTTP;
//                jsonData.result = JsonData.Result.Error;
//                jsonData.errorUi = Global.ErrorGenerico;
//            }

//            return Json(jsonData);
//        }
//        #endregion

//        #region Export
//        [HttpGet]
//        public IActionResult ExportarReporteCargaCestas(DateTime? fechaDesde, DateTime? fechaHasta, string idColadaInicial, string idColadaFinal, int? idEstado, string cesta, int? idVia)
//        {
            
//            try
//            {
//                int? _idColadaInicial = idColadaInicial != null ? _servicioGenerico.Get<Colada>(co => co.Nombre == idColadaInicial).Id : null;
//                int? _idColadaFinal = idColadaFinal != null ? _servicioGenerico.Get<Colada>(co => co.Nombre == idColadaFinal).Id : null;
           
//                List<ReporteCargaCestas> todosCargaCestas = _servicioReporteCargaCestas.ObtenerReporteCargaCestas(fechaDesde, fechaHasta, _idColadaInicial, _idColadaFinal, idEstado, cesta, idVia).ToList();
//                var listaReducida = todosCargaCestas.Select(cargaCestas => new 
//                {
//                    Fecha = cargaCestas.Fecha,
//                    Colada = cargaCestas.Colada.Nombre,
//                    Cesta = cargaCestas.Cesta,
//                    RecetaCapa = cargaCestas.RecetaCapa,
//                    RecetaMaterial = cargaCestas.RecetaMaterial,
//                    RecetaPeso = cargaCestas.RecetaPeso,
//                    RealCapa = cargaCestas.RealCapa,
//                    RealMaterial = cargaCestas.RealMaterial,
//                    RealPeso = cargaCestas.RealPeso,
//                    RealHoraCarga = cargaCestas.RealHoraCarga,
//                    HoraFin = cargaCestas.HoraFin,
//                    Comentario = cargaCestas.Comentario,
//                    Estado = cargaCestas.Estado,
//                    Por = cargaCestas.Por,
//                    Via = cargaCestas.Via
//                }).ToList();

//                var fileBytes = CreateExcelFile.CreateExcelDocumentAsByte(listaReducida);
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
//    }
//}
