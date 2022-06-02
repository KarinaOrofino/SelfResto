using log4net;
using KO.Datos.Interfaces;
using KO.Entidades;
using System.Collections.Generic;
using System.Reflection;
using Servicios.Interfaces;
using KO.Servicios.Implementaciones;
using KO.Servicios.Interfaces;
using KO.Datos.Implementacion;

namespace Servicios.Implementaciones
{
    public class ServicioVMedicos : ServicioBase<IDatosVMedicos>, IServicioVMedicos

    {
        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        //private IServicioGenerico _servicioGenerico { get; set; }

        public ServicioVMedicos(/*IServicioGenerico servicioGenerico, */IDatosVMedicos datos) : base(datos)
        {
            //_servicioGenerico = servicioGenerico;
        }


        public List<Medico> ObtenerTodos()
        {
            return _datos.ObtenerTodos();
        }

        public List<Medico> ObtenerFiltrados(int? Matricula, string Nombre, string Apellido, bool? Estado) 
        {
            return _datos.ObtenerFiltrados(Matricula, Nombre, Apellido, Estado);
        }

        public Medico Obtener(int Matricula) 
        {
            return _datos.Obtener(Matricula);
        }

        public void Agregar(Medico medico) 
        {
            _datos.Agregar(medico);
        }

        public void Actualizar(Medico medico)
        {
            _datos.Actualizar(medico);
        }

        public void Inactivar(int Matricula)
        {
            _datos.Inactivar(Matricula);
        }

        public void Activar(int Matricula) 
        {
            _datos.Activar(Matricula);
        }
    }
}

