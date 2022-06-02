using KO.Datos.Interfaces;
using KO.Entidades;
using KO.Servicios.Implementaciones;
using KO.Servicios.Interfaces;
using System.Collections.Generic;
using System.Transactions;

namespace KO.Servicios
{
    public class ServicioIntegraciones : ServicioGenerico, IServicioIntegraciones
    {
        //private readonly IServicioGenerico _servicioGenerico;
        //private IDatosIntegraciones _datosIntegraciones { get; set; }

        public ServicioIntegraciones(IDatosGenerico datos /*,IDatosIntegraciones datosIntegraciones, IServicioGenerico servicioGenerico*/) : base(datos)
        {
            
        //    _datosIntegraciones = datosIntegraciones;
        //    _servicioGenerico = servicioGenerico;
        }


        public void ObtenerGerencias()
        {
            
        }

        public void ObtenerUsuariosInternos()
        {
            
        }


    }
}
