using KO.Datos.Interfaces;
using KO.Entidades;
using KO.Servicios.Implementaciones;
using log4net;
using Servicios.Interfaces;
using System.Collections.Generic;
using System.Reflection;

namespace Servicios.Implementaciones
{
    public class ServicioVPacientes : ServicioBase<IDatosVPacientes>, IServicioVPacientes
    {
        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ServicioVPacientes(IDatosVPacientes datos) : base(datos)
        {

        }

        public List<Paciente> ObtenerTodos()
        {
            return _datos.ObtenerTodos();
        }


        public List<Paciente> ObtenerFiltrados(string campoBusqueda)
        {
            return _datos.ObtenerFiltrados(campoBusqueda);
        }


        public Paciente Obtener(int id)
        {
            return _datos.Obtener(id);
        }


        public void Agregar(Paciente paciente)
        {
            _datos.Agregar(paciente);
        }


        public void Actualizar(Paciente paciente)
        {
            _datos.Actualizar(paciente);
        }

        public List<ObraSocial> ObtenerObrasSociales() 
        {
            return _datos.ObtenerObrasSociales();
        }

    }
}

