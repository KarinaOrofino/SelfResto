using Framework.Common;
using Framework.Utils;
using Framework.Web;
using KO.Entidades;
using KO.Web.Models.Pacientes;
using Microsoft.AspNetCore.Mvc;
using Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers.Pacientes
{
    [Route("[controller]/[action]")]
    public class PacientesController : BaseController
    {
        #region Propiedades

        private IServicioVPacientes ServicioPacientes { get; set; }

        public PacientesController(IServicioVPacientes servicioPacientes)
        {
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
        public async Task<IActionResult> Detalle(int id)
        {
            PacienteViewModel pacienteVM = new();

            try
            {
                pacienteVM.ListaObrasSociales = ServicioPacientes.ObtenerObrasSociales().Where(os => os.Estado == true).OrderBy(os => os.Nombre).ToList();

                var todosPacientes = ServicioPacientes.ObtenerTodos().Select(pac => new PacienteViewModel
                {
                    Id = pac.Id,
                    Nombre = pac.Nombre,
                    Apellido = pac.Apellido,
                    FechaNacimientoString = pac.FechaNacimiento.ToShortDateString()
                }).ToList(); 

                if (id == 0)
                {
                    pacienteVM.ListaPacientes = todosPacientes;
                }

                if (id != 0)
                {
                    Paciente paciente = ServicioPacientes.Obtener(id);
                    pacienteVM.Id = paciente.Id;
                    pacienteVM.Nombre = paciente.Nombre;
                    pacienteVM.Apellido = paciente.Apellido;
                    pacienteVM.IdObraSocial = paciente.IdObraSocial;
                    pacienteVM.NumeroObraSocial = paciente.NumeroObraSocial;
                    pacienteVM.FechaNacimiento = paciente.FechaNacimiento;
                    pacienteVM.ListaPacientes = todosPacientes.Where(pac => pac.Id != id).ToList();

                }
            }

            catch (Exception ex)
            {
                log.Error("No se pudo obtener el paciente con Id: " + id, ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
                return Redirect("/Home/Error");
            }

            return View(pacienteVM);
        }

        #endregion

        #region Métodos Pantalla Listado 

        [HttpGet]
        public JsonResult ObtenerTodos()
        {
            JsonData jsonData = new();

            try
            {
                List<Paciente> listaPacientes = ServicioPacientes.ObtenerTodos().ToList();

                List<PacienteViewModel> listaPacientesVM = new();

                listaPacientesVM = listaPacientes.Select(pac => new PacienteViewModel()
                {
                    Id = pac.Id,
                    Nombre = pac.Nombre,
                    Apellido = pac.Apellido,
                    ObraSocial = pac.ObraSocial,
                    NumeroObraSocial = pac.NumeroObraSocial,
                    FechaNacimientoString = pac.FechaNacimiento.ToShortDateString()


                }).OrderBy(paciente => paciente.Apellido).ToList();

                jsonData.content = listaPacientesVM;
                jsonData.result = JsonData.Result.Ok;
            }

            catch (Exception ex)
            {
                log.Error("No se pudo obtener la lista de pacientes", ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
                jsonData.errorUi = "No se pudo obtener la lista de pacientes";
                jsonData.result = JsonData.Result.Error;
            }

            return Json(jsonData);
        }


        [HttpGet]
        public IActionResult Exportar(string campoBusqueda)
        {
            try
            {

                List<Paciente> listaPacientes = ServicioPacientes.ObtenerFiltrados(campoBusqueda).ToList();

                List<PacienteViewModel> listaPacientesVM = listaPacientes.Select(pac => new PacienteViewModel()
                {
                    Id = pac.Id,
                    Nombre = pac.Nombre,
                    Apellido = pac.Apellido,
                    ObraSocial = pac.ObraSocial,
                    NumeroObraSocial = pac.NumeroObraSocial,
                    FechaNacimiento = pac.FechaNacimiento

                }).OrderBy(paciente => paciente.Nombre).ToList();

                var listaReducida = listaPacientesVM.Select(pac => new
                {
                    pac.Nombre,
                    pac.Apellido,
                    pac.ObraSocial,
                    pac.NumeroObraSocial,
                    pac.FechaNacimiento
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

        public JsonResult Agregar(PacienteViewModel pacienteVM)
        {
            JsonData jsonData = new();
            Paciente paciente = new();

            try
            {
                paciente.Nombre = pacienteVM.Nombre;
                paciente.Apellido = pacienteVM.Apellido;
                paciente.IdObraSocial = pacienteVM.IdObraSocial;
                paciente.NumeroObraSocial = pacienteVM.NumeroObraSocial;
                paciente.FechaNacimiento = pacienteVM.FechaNacimiento;

                ServicioPacientes.Agregar(paciente);

                jsonData.result = JsonData.Result.Ok;
            }

            catch (Exception ex)
            {
                log.Error("No se pudo guardar el paciente. Error: ", ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
            }

            return Json(jsonData);
        }

        public JsonResult Actualizar(PacienteViewModel pacienteVM)
        {
            JsonData jsonData = new();
            Paciente paciente = new();

            try
            {

                paciente.Id = pacienteVM.Id;
                paciente.Nombre = pacienteVM.Nombre;
                paciente.Apellido = pacienteVM.Apellido;
                paciente.IdObraSocial = pacienteVM.IdObraSocial;
                paciente.NumeroObraSocial = pacienteVM.NumeroObraSocial;
                paciente.FechaNacimiento = pacienteVM.FechaNacimiento;

                ServicioPacientes.Actualizar(paciente);

                jsonData.result = JsonData.Result.Ok;
            }

            catch (Exception ex)
            {
                log.Error("No se pudo actualizar el paciente: " + paciente.Apellido + ", Error: ", ex);
                Response.StatusCode = Constantes.ERROR_HTTP;
            }

            return Json(jsonData);
        }

        #endregion



    }
}

