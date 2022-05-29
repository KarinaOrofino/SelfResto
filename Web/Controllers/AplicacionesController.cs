using KO.Entidades;
using KO.Recursos;
using KO.Servicios.Interfaces;
using Framework.Common;
using Framework.Utils;
using Framework.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace KO.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class AplicacionesController : BaseController
    {
        #region Propiedades de servicio
        //public AplicacionesController(IServicioGenerico servicioGenerico, IServicioIntegraciones servicioIntegraciones, IConfiguration configuration, IServicioLogEventos servicioLogEventos)
        //{
        //    this._servicioGenerico = servicioGenerico;
        //    this._servicioIntegraciones = servicioIntegraciones;
        //    this.Configuration = configuration;
        //    this._servicioLogEventos = servicioLogEventos;
        //}
        #endregion

        #region Páginas

        [HttpGet]
        public IActionResult Listado()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ObtenerFiltrados(int? Matricula, string Nombre, string Apellido, bool? Estado)
        {
            JsonData jsonData = new JsonData();

            //try
            //{
            //    List<Medico> listaMedicos = ServicioMedicos.ObtenerFiltrados(Matricula, Nombre, Apellido, Estado).ToList();

            //    List<MedicoViewModel> listaMedicosVM = new List<MedicoViewModel>();

            //    listaMedicosVM = listaMedicos.Select(med => new MedicoViewModel()
            //    {
            //        Matricula = med.Matricula,
            //        Nombre = med.Nombre,
            //        Apellido = med.Apellido,
            //        Estado = med.Estado

            //    }).OrderBy(medico => medico.Apellido).ToList();

            //    jsonData.content = listaMedicosVM;
            //    jsonData.result = JsonData.Result.Ok;
            //}
            //catch (Exception ex)
            //{
            //    log.Error("No se pudo obtener la lista de médicos", ex);
            //    Response.StatusCode = Constantes.ERROR_HTTP;
            //    jsonData.errorUi = "No se pudo obtener la lista de médicos";
            //    jsonData.result = JsonData.Result.Error;

            //}

            return Json(jsonData);
        }

        //[Route("{id}")]
        //[HttpGet]
        //public JsonResult ObtenerCestas(int id)
        //{
        //    JsonData jsonData = new JsonData();
        //    try
        //    {
        //        List<CapasReceta> capasRecetas = _servicioGenerico.GetAll<CapasReceta>(c => c.IdReceta == id).ToList();
        //        List<List<CapaViewModel>> cestas = new();
        //        var capasAgrupadas = capasRecetas.GroupBy(x => x.NumeroCarga);

        //        foreach (var grupoCapa in capasAgrupadas)
        //        {
        //            List<CapaViewModel> cesta = new List<CapaViewModel>();
        //            foreach (CapasReceta capa in grupoCapa)
        //            {
        //                CapaViewModel capaVM = CargarCestaVMDesdeEntidad(capa);
        //                cesta.Add(capaVM);
        //            }
        //            cestas.Add(cesta);
        //        }

        //        jsonData.content = cestas;
        //        jsonData.result = JsonData.Result.Ok;
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error("No se pudo obtener las capas correspondientes a la receta con id: " + id + " Error: " + ex.Message);
        //        Response.StatusCode = Constantes.ERROR_HTTP;
        //        jsonData.result = JsonData.Result.Error;
        //        jsonData.errorUi = Global.ErrorGenerico;
        //    }

        //    return Json(jsonData);
        //}


        #endregion

        #region Privados
        #endregion
    }
}
