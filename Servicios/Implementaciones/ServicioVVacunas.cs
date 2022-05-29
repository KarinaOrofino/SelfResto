using KO.Datos.Interfaces;
using KO.Servicios.Implementaciones;
using KO.Servicios.Interfaces;
using log4net;
using Servicios.Interfaces;
using System.Reflection;

namespace Servicios.Implementaciones
{
    public class ServicioVVacunas : ServicioGenerico, IServicioVVacunas
    {
        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private IServicioGenerico _servicioGenerico { get; set; }

        public ServicioVVacunas(IDatosGenerico datos) : base(datos)
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

