using Framework.Web;
using Microsoft.AspNetCore.Mvc;
using KO.Servicios.Interfaces;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Framework.Common;
using KO.Recursos;
using KO.Entidades;
using System.Linq;
using Servicios.Interfaces;
using Framework.Utils;
using KO.Web.Models.Medicos;

namespace Web.Controllers.Medicos
{
    public class VacunasController : BaseController
    {
        #region Propiedades de servicio

        private IServicioGenerico ServicioGenerico { get; set; }
        private IServicioVMedicos ServicioMedicos { get; set; }

        #endregion

        public VacunasController(IServicioGenerico servicioGenerico, IServicioVMedicos servicioMedicos)
        {
            this.ServicioGenerico = servicioGenerico;
            this.ServicioMedicos = servicioMedicos;
        }

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

            try
            {

            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return View();
        }

        #endregion

        #region Métodos Pantalla Listado 

        #endregion

        #region Métodos Pantalla Detalle

           #endregion

        #region Métodos Privados        

     
        #endregion

    }
}

