using Framework.Common;
using Framework.Web;
using KO.Entidades;
using KO.Web.Models.Medicos;
using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers.Medicos
{
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
        public async Task<IActionResult> Detalle()
        {
            MedicoViewModel medicoVM = new();
            try
            {

            }
            catch (Exception ex)
            {
                log.Error(ex);

            }

            return View(medicoVM);
        }

        [Route("{matricula}")]
        [HttpGet]
        public async Task<IActionResult> Detalle(int matricula)
        {
            MedicoViewModel medicoVM = new();

            try
            {
                Medico medico = ServicioMedicos.Obtener(matricula);

                medicoVM.Matricula = medico.Matricula;
                medicoVM.Nombre = medico.Nombre;
                medicoVM.Apellido = medico.Apellido;
                medicoVM.Estado = medico.Estado;
            }

            catch (Exception ex)
            {
                log.Error("No se pudo obtener el médico con matricula: " + matricula, ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
            }

            return View(medicoVM);
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

        public JsonResult Inactivar(int Matricula)
        {
            JsonData jsonData = new();

            try
            {
                ServicioMedicos.Inactivar(Matricula);

                jsonData.result = JsonData.Result.Ok;
            }
            catch (Exception ex)
            {
                log.Error("No se pudo inactivar el médico con matrícula: " + Matricula, ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
                jsonData.errorUi = "No se pudo inactivar el médico";
                jsonData.result = JsonData.Result.Error;
            }

            return Json(jsonData);
        }

        public JsonResult Activar(int Matricula)
        {
            JsonData jsonData = new();

            try
            {
                ServicioMedicos.Activar(Matricula);

                jsonData.result = JsonData.Result.Ok;
            }
            catch (Exception ex)
            {
                log.Error("No se pudo activar el médico con matrícula: " + Matricula, ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
                jsonData.errorUi = "No se pudo activar el médico";
                jsonData.result = JsonData.Result.Error;
            }

            return Json(jsonData);
        }
        #endregion

        #region Métodos Pantalla Detalle

        public JsonResult Guardar(MedicoViewModel medicoVM)
        {
            JsonData jsonData = new();
            Medico medico = new();

            try
            {

                medico.Matricula = medicoVM.Matricula;
                medico.Nombre = medicoVM.Nombre;
                medico.Apellido = medicoVM.Apellido;

                if (medico.Matricula == 0)
                {
                    medico.Estado = true;
                    ServicioMedicos.Agregar(medico);
                }

                else
                {
                    medico.Estado = medicoVM.Estado;
                    ServicioMedicos.Actualizar(medico);
                }

                jsonData.result = JsonData.Result.Ok;
            }

            catch (Exception ex)
            {
                log.Error("No se pudo guardar el médico. Error: ", ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
            }

            return Json(jsonData);
        }

        #endregion



    }
}

