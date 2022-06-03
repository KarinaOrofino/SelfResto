using KO.Datos.Interfaces;
using KO.Entidades;
using KO.Servicios.Implementaciones;
using KO.Servicios.Interfaces;
using log4net;
using Servicios.Interfaces;
using System.Collections.Generic;
using System.Reflection;

namespace Servicios.Implementaciones
{
    public class ServicioVVacunas : ServicioBase<IDatosVVacunas>, IServicioVVacunas
    {
        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ServicioVVacunas(IDatosVVacunas datos) : base(datos)
        {
          
        }

        public List<Vacuna> ObtenerTodas()
        {
            return _datos.ObtenerTodas();
        }

        public List<Vacuna> ObtenerFiltradas(string campoBusqueda, bool? estado) 
        {
            return _datos.ObtenerFiltradas(campoBusqueda, estado);        
        }

        public Vacuna Obtener(int id)
        {
            return _datos.Obtener(id);
        }

        public void Agregar(Vacuna vacuna)
        {
            _datos.Agregar(vacuna);
        }

        public void Actualizar(Vacuna vacuna)
        {
            _datos.Actualizar(vacuna);
        }

        public void Inactivar(int id)
        {
            _datos.Inactivar(id);
        }

        public void Activar(int id)
        {
            _datos.Activar(id);
        }
    }

}

