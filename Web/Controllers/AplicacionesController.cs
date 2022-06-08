using Framework.Common;
using Framework.Utils;
using Framework.Web;
using KO.Entidades;
using KO.Servicios.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using KO.Web.Models.Aplicaciones;
using Servicios.Interfaces;

namespace Web.Controllers.Aplicaciones
{
    [Route("[controller]/[action]")]
    public class AplicacionesController : BaseController
    {
        #region Propiedades

        private IServicioVAplicaciones ServicioAplicaciones { get; set; }

        private IServicioVPacientes ServicioPacientes { get; set; }

        public TextInfo myCapitalize = new CultureInfo("es-AR", false).TextInfo;

        public AplicacionesController(IServicioVAplicaciones servicioAplicaciones, IServicioVPacientes servicioPacientes)
        {
            this.ServicioAplicaciones = servicioAplicaciones;
            this.ServicioPacientes = servicioPacientes;
        }
        #endregion

        #region Páginas 

        [HttpGet]
        public async Task<IActionResult> Listado()
        {
            try
            {

            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Detalle()
        {
            AplicacionViewModel aplicacionVM = new();
            try
            {
                
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return View(aplicacionVM);
        }

        [HttpPost]
        public async Task<IActionResult> Detalle(int id)
        {
            AplicacionViewModel aplicacionVM = new();

            try
            {
                    Aplicacion aplicacion = ServicioAplicaciones.Obtener(id);
                    aplicacionVM.Id = aplicacion.Id;
                    aplicacionVM.Fecha = aplicacion.Fecha;
                    aplicacionVM.IdPaciente = aplicacion.IdPaciente;
                    aplicacionVM.IdMedico = aplicacion.IdMedico;
                    aplicacionVM.ListaPacientes = ServicioPacientes.ObtenerTodos().ToList();
            }

            catch (Exception ex)
            {
                log.Error("No se pudo obtener la aplicación con id: " + id, ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
                return Redirect("/Home/Error");
            }

            return View(aplicacionVM);
        }

        #endregion

        #region Métodos Pantalla Listado 

        [HttpGet]
        public JsonResult ObtenerTodas()
        {
            JsonData jsonData = new();

            try
            {
                List<Aplicacion> listaAplicaciones = ServicioAplicaciones.ObtenerTodas().ToList();

                List<AplicacionViewModel> listaAplicacionesVM = new();

                listaAplicacionesVM = listaAplicaciones.Select(app => new AplicacionViewModel()
                {
                    Id = app.Id,
                    Fecha = app.Fecha,
                    FechaString = app.Fecha.ToShortDateString(),
                    IdPaciente = app.IdPaciente,
                    NombrePaciente = app.NombrePaciente,
                    IdMedico = app.IdMedico,
                    NombreMedico = app.NombreMedico,
                    ListaDetalles = ServicioAplicaciones.ObtenerVacunasPorAplicacion(app.Id),

                }).OrderByDescending(ap=>ap.Fecha).ToList();

                jsonData.content = listaAplicacionesVM;
                jsonData.result = JsonData.Result.Ok;
            }

            catch (Exception ex)
            {
                log.Error("No se pudo obtener la lista de aplicaciones", ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
                jsonData.errorUi = "No se pudo obtener la lista de vacunas";
                jsonData.result = JsonData.Result.Error;
            }

            return Json(jsonData);
        }

        [HttpGet]
        public IActionResult Exportar(string campoBusqueda)
        {
            try
            {

                List<Aplicacion> listaAplicaciones = ServicioAplicaciones.ObtenerFiltradas(campoBusqueda).ToList();

                List<AplicacionViewModel> listaAplicacionesVM = listaAplicaciones.Select(app => new AplicacionViewModel()
                {
                    Fecha = app.Fecha,
                    FechaString = app.Fecha.ToShortDateString(),
                    NombrePaciente = app.NombrePaciente,
                    NombreMedico = app.NombreMedico,
                    NombreVacuna = app.NombreVacuna,
                    MarcaVacuna = app.MarcaVacuna

                }).OrderByDescending(e=>e.Fecha).ToList();

                var listaReducida = listaAplicacionesVM.Select(app => new
                {

                    Fecha = app.FechaString,
                    app.NombrePaciente,
                    app.NombreMedico,
                    app.NombreVacuna,
                    app.MarcaVacuna

                }).ToList();

                var fileBytes = CreateExcelFile.CreateExcelDocumentAsByte(listaReducida);
                log.Info("Método crear excel OK");
                this.HttpContext.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                this.HttpContext.Response.Headers.Add("content-disposition", "attachment");
                return File(fileBytes, this.HttpContext.Response.ContentType);
            }
            catch (Exception ex)
            {
                this.HttpContext.Response.StatusCode = Constantes.ERROR_HTTP;
                log.Error(ex);
            }

            return Content(string.Empty);
        }
        #endregion

        #region Métodos Pantalla Detalle

        public JsonResult Agregar(AplicacionViewModel aplicacionVM)
        {
            JsonData jsonData = new();
            Aplicacion aplicacion = new();

            try
            {
                aplicacion.Fecha = aplicacionVM.Fecha;
                aplicacion.IdPaciente = aplicacionVM.IdPaciente;
                aplicacion.IdMedico = aplicacionVM.IdMedico;
                aplicacion.ListaIdsVacunas = aplicacionVM.ListaIdsVacunas;

                ServicioAplicaciones.Agregar(aplicacion);

                jsonData.result = JsonData.Result.Ok;
            }

            catch (Exception ex)
            {
                log.Error("No se pudo guardar la aplicacion. Error: ", ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
            }

            return Json(jsonData);
        }

        public JsonResult Eliminar(int idAplicacion)
        {
            JsonData jsonData = new();

            try
            {

                ServicioAplicaciones.Eliminar(idAplicacion);

                jsonData.result = JsonData.Result.Ok;
            }

            catch (Exception ex)
            {
                log.Error("No se pudo actualizar la aplicación, Error: ", ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
            }

            return Json(jsonData);
        }

  
        #endregion




    }
}

