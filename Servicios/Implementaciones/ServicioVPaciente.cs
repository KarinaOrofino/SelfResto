using KO.Datos.Interfaces;
using KO.Servicios.Implementaciones;
using log4net;
using Servicios.Interfaces;
using System.Reflection;

namespace Servicios.Implementaciones
{
    public class ServicioVPacientes : ServicioGenerico, IServicioVPaciente
    {
        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ServicioVPacientes(IDatosGenerico datos) : base(datos)
        {

        }

        //public void CrearColadaYCargas(Colada nuevaColada, Receta recetaDeLaColada, int idUser)
        //{
        //    using (TransactionScope scope = new TransactionScope())
        //    {

        //        _servicioGenerico.Add(nuevaColada);

 
        //        scope.Complete();

        //    }
        //}
    }
}

