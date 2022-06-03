using Framework.Common;
using Framework.Utils;
using Framework.Web;
using KO.Entidades;
using KO.Recursos;
using KO.Web.Models.Medicos;
using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers.Medicos
{
    [Route("[controller]/[action]")]
    public class MedicosController : BaseController
    {
        #region Propiedades

        private IServicioVMedicos ServicioMedicos { get; set; }

        public MedicosController(IServicioVMedicos servicioMedicos)
        {
            this.ServicioMedicos = servicioMedicos;
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
        public async Task<IActionResult> Detalle(int matricula)
        {
            MedicoViewModel medicoVM = new();

            try
            {

                if (matricula == 0) { 

                    medicoVM.Estado = true;
                    medicoVM.MedicoExistente = false;
                    medicoVM.ListaMatriculasMedicos = ServicioMedicos.ObtenerTodos().Select(med => med.Matricula.ToString()).ToList();

                }

                else {

                    Medico medico = ServicioMedicos.Obtener(matricula);
                    medicoVM.Matricula = medico.Matricula;
                    medicoVM.Nombre = medico.Nombre;
                    medicoVM.Apellido = medico.Apellido;
                    medicoVM.Estado = medico.Estado;
                    medicoVM.MedicoExistente = true;

                }
            }

            catch (Exception ex)
            {
                log.Error("No se pudo obtener el médico con matrícula: " + matricula, ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
                return Redirect("/Home/Error");
            }

            return View(medicoVM);
        }

        [HttpGet]
        public JsonResult VerMedico(int matricula)
        {
            JsonData jsonData = new();
            MedicoViewModel medicoVM = new();

            try
            {

                if (matricula == 0)
                {

                    medicoVM.Estado = true;
                    medicoVM.MedicoExistente = false;
                    medicoVM.ListaMatriculasMedicos = ServicioMedicos.ObtenerTodos().Select(med => med.Matricula.ToString()).ToList();

                }

                else
                {

                    Medico medico = ServicioMedicos.Obtener(matricula);
                    medicoVM.Matricula = medico.Matricula;
                    medicoVM.Nombre = medico.Nombre;
                    medicoVM.Apellido = medico.Apellido;
                    medicoVM.Estado = medico.Estado;
                    medicoVM.MedicoExistente = true;

                }
            

                jsonData.content = medicoVM;
                jsonData.result = JsonData.Result.Ok;
            }
            catch (Exception ex)
            {
                log.Error("No se pudo obtener el médico con matrícula: " + matricula, ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
            }

            return Json(jsonData); ;
        }

        #endregion

        #region Métodos Pantalla Listado 

        [HttpGet]
        public JsonResult ObtenerTodos()
        {
            JsonData jsonData = new();

            try
            {
                List<Medico> listaMedicos = ServicioMedicos.ObtenerTodos().ToList();

                List<MedicoViewModel> listaMedicosVM = new();

                listaMedicosVM = listaMedicos.Select(med => new MedicoViewModel()
                {
                    Matricula = med.Matricula,
                    Nombre = med.Nombre,
                    Apellido = med.Apellido,
                    Estado = med.Estado

                }).OrderBy(medico => medico.Apellido).ToList();

                jsonData.content = listaMedicosVM;
                jsonData.result = JsonData.Result.Ok;
            }
            
            catch (Exception ex)
            {
                log.Error("No se pudo obtener la lista de médicos", ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
                jsonData.errorUi = "No se pudo obtener la lista de médicos";
                jsonData.result = JsonData.Result.Error;
            }

            return Json(jsonData);
        }

        public JsonResult Inactivar(int matricula)
        {
            JsonData jsonData = new();

            try
            {
                ServicioMedicos.Inactivar(matricula);

                jsonData.result = JsonData.Result.Ok;
            }
            catch (Exception ex)
            {
                log.Error("No se pudo inactivar el médico con matrícula: " + matricula, ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
                jsonData.errorUi = "No se pudo inactivar el médico";
                jsonData.result = JsonData.Result.Error;
            }

            return Json(jsonData);
        }

        public JsonResult Activar(int matricula)
        {
            JsonData jsonData = new();

            try
            {
                ServicioMedicos.Activar(matricula);

                jsonData.result = JsonData.Result.Ok;
            }
            catch (Exception ex)
            {
                log.Error("No se pudo activar el médico con matrícula: " + matricula, ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
                jsonData.errorUi = "No se pudo activar el médico";
                jsonData.result = JsonData.Result.Error;
            }

            return Json(jsonData);
        }

        [HttpGet]
        public IActionResult Exportar(string campoBusqueda, bool? estado)
        {
            try
            {

                List<Medico> listaMedicos = ServicioMedicos.ObtenerFiltrados(campoBusqueda, estado).ToList();

                List<MedicoViewModel> listaMedicosVM = listaMedicos.Select(med => new MedicoViewModel()
                {
                    Matricula = med.Matricula,
                    Nombre = med.Nombre,
                    Apellido = med.Apellido,
                    Estado = med.Estado

                }).OrderBy(medico => medico.Nombre).ToList();

                var listaReducida = listaMedicosVM.Select(med => new
                {
                    med.Matricula,
                    med.Nombre,
                    med.Apellido,
                    Estado = med.Estado == true ? Global.Activo : Global.Inactivo,
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

        public JsonResult Agregar(MedicoViewModel medicoVM)
        {
            JsonData jsonData = new();
            Medico medico = new();

            try
            {

                medico.Matricula = medicoVM.Matricula;
                medico.Nombre = medicoVM.Nombre;
                medico.Apellido = medicoVM.Apellido;
                medico.Estado = true;

                ServicioMedicos.Agregar(medico);

                jsonData.result = JsonData.Result.Ok;
            }

            catch (Exception ex)
            {
                log.Error("No se pudo guardar el médico. Error: ", ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
            }

            return Json(jsonData);
        }

        public JsonResult Actualizar(MedicoViewModel medicoVM)
        {
            JsonData jsonData = new();
            Medico medico = new();

            try
            {

                medico.Matricula = medicoVM.Matricula;
                medico.Nombre = medicoVM.Nombre;
                medico.Apellido = medicoVM.Apellido;
                medico.Estado = medicoVM.Estado;

                ServicioMedicos.Actualizar(medico);

                jsonData.result = JsonData.Result.Ok;
            }

            catch (Exception ex)
            {
                log.Error("No se pudo actualizar el médico: " + medico.Apellido +", Error: ", ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
            }

            return Json(jsonData);
        }

        #endregion



    }
}

