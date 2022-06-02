using Framework.Common;
using Framework.Utils;
using Framework.Web;
using KO.Entidades;
using KO.Recursos;
using KO.Web.Models.Vacunas;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers.vacunas
{
    [Route("[controller]/[action]")]
    public class VacunasController : BaseController
    {
        #region Propiedades

        private IServicioVVacunas ServicioVacunas { get; set; }

        public VacunasController(IServicioVVacunas servicioVacunas)
        {
            this.ServicioVacunas = servicioVacunas;
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
        public async Task<IActionResult> Detalle(int id)
        {
            VacunaViewModel vacunaVM = new();

            try
            {

                if (id == 0)
                {

                    vacunaVM.Estado = true;
                    vacunaVM.VacunaExistente = false;
                    vacunaVM.ListaVacunas = ServicioVacunas.ObtenerTodas().Select(vac => vac.Nombre).ToList();

                }

                else
                {

                    Vacuna vacuna = ServicioVacunas.Obtener(id);
                    vacunaVM.Id = vacuna.Id;
                    vacunaVM.Nombre = vacuna.Nombre;
                    vacunaVM.Estado = vacuna.Estado;
                    vacunaVM.VacunaExistente = true;

                }
            }

            catch (Exception ex)
            {
                log.Error("No se pudo obtener la vacuna con id: " + id, ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
                return Redirect("/Home/Error");
            }

            return View(vacunaVM);
        }

        #endregion

        #region Métodos Pantalla Listado 

        [HttpGet]
        public JsonResult ObtenerTodas()
        {
            JsonData jsonData = new();

            try
            {
                List<Vacuna> listavacunas = ServicioVacunas.ObtenerTodas().ToList();

                List<VacunaViewModel> listavacunasVM = new();

                listavacunasVM = listavacunas.Select(vac => new VacunaViewModel()
                {
                    Id = vac.Id,
                    Nombre = vac.Nombre,
                    Estado = vac.Estado

                }).OrderBy(vacuna => vacuna.Nombre).ToList();

                jsonData.content = listavacunasVM;
                jsonData.result = JsonData.Result.Ok;
            }

            catch (Exception ex)
            {
                log.Error("No se pudo obtener la lista de vacunas", ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
                jsonData.errorUi = "No se pudo obtener la lista de vacunas";
                jsonData.result = JsonData.Result.Error;
            }

            return Json(jsonData);
        }

        public JsonResult Inactivar(int id)
        {
            JsonData jsonData = new();

            try
            {
                ServicioVacunas.Inactivar(id);

                jsonData.result = JsonData.Result.Ok;
            }
            catch (Exception ex)
            {
                log.Error("No se pudo inactivar la vacuna con id: " + id, ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
                jsonData.errorUi = "No se pudo inactivar la vacuna";
                jsonData.result = JsonData.Result.Error;
            }

            return Json(jsonData);
        }

        public JsonResult Activar(int id)
        {
            JsonData jsonData = new();

            try
            {
                ServicioVacunas.Activar(id);

                jsonData.result = JsonData.Result.Ok;
            }
            catch (Exception ex)
            {
                log.Error("No se pudo activar la vacuna con id: " + id, ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
                jsonData.errorUi = "No se pudo activar la vacuna";
                jsonData.result = JsonData.Result.Error;
            }

            return Json(jsonData);
        }

        [HttpGet]
        public IActionResult Exportar(string nombre, bool? estado)
        {
            try
            {

                List<Vacuna> listavacunas = ServicioVacunas.ObtenerFiltradas(nombre, estado).ToList();

                List<VacunaViewModel> listavacunasVM = listavacunas.Select(vac => new VacunaViewModel()
                {
                    Id = vac.Id,
                    Nombre = vac.Nombre,
                    Estado = vac.Estado

                }).OrderBy(vacuna => vacuna.Nombre).ToList();

                var listaReducida = listavacunasVM.Select(vac => new
                {
                    vac.Nombre,
                    Estado = vac.Estado == true ? Global.Activa : Global.Inactiva,
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

        public JsonResult Agregar(VacunaViewModel vacunaVM)
        {
            JsonData jsonData = new();
            Vacuna vacuna = new();

            try
            {

                vacuna.Nombre = vacunaVM.Nombre;
                vacuna.Estado = true;

                ServicioVacunas.Agregar(vacuna);

                jsonData.result = JsonData.Result.Ok;
            }

            catch (Exception ex)
            {
                log.Error("No se pudo guardar la vacuna. Error: ", ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
            }

            return Json(jsonData);
        }

        public JsonResult Actualizar(VacunaViewModel vacunaVM)
        {
            JsonData jsonData = new();
            Vacuna vacuna = new();

            try
            {

                vacuna.Id = vacunaVM.Id;
                vacuna.Nombre = vacunaVM.Nombre;
                vacuna.Estado = vacunaVM.Estado;

                ServicioVacunas.Actualizar(vacuna);

                jsonData.result = JsonData.Result.Ok;
            }

            catch (Exception ex)
            {
                log.Error("No se pudo actualizar la vacuna: " + vacuna.Nombre + ", Error: ", ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
            }

            return Json(jsonData);
        }

        #endregion



    }
}

