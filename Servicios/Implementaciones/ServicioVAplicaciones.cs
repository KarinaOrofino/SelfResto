using KO.Datos.Interfaces;
using KO.Entidades;
using KO.Servicios.Interfaces;
using log4net;
using Servicios.Implementaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace KO.Servicios.Implementaciones
{
    public class ServicioVAplicaciones : ServicioBase<IDatosVAplicaciones>, IServicioVAplicaciones
    {
        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ServicioVAplicaciones(IDatosVAplicaciones datos) : base(datos)
        {

        }

        public List<Aplicacion> ObtenerTodas()
        {
            return _datos.ObtenerTodas();
        }

        public List<Aplicacion> ObtenerFiltradas(string campoBusqueda)
        {
            return _datos.ObtenerFiltradas(campoBusqueda);
        }

        public Aplicacion Obtener(int id)
        {
            return _datos.Obtener(id);
        }

        public void Agregar(Aplicacion aplicacion)
        {

            int idAplicacion = _datos.Agregar(aplicacion);

            foreach (int elem in aplicacion.ListaIdsVacunas)
            {
                _datos.AgregarAplicacionDetalle(elem, idAplicacion);
            }

        }

        public void Eliminar(int idAplicacion)
        {
            _datos.Eliminar(idAplicacion);
        }

        public List<AplicacionDetalle> ObtenerVacunasPorAplicacion(int idAplicacion) 
        {
            return _datos.ObtenerVacunasPorAplicacion(idAplicacion);
        }
    }
}
