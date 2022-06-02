using log4net;
using KO.Datos.Interfaces;
using KO.Entidades;
using KO.Servicios.Implementaciones;
using KO.Servicios.Interfaces;
using Servicios.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Transactions;

namespace Servicios.Implementaciones
{
    public class ServicioFuncionalidad : ServicioGenerico, IServicioFuncionalidad
    {
        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private IServicioGenerico ServicioGenerico { get; set; }

        public ServicioFuncionalidad(IDatosGenerico datos, IServicioGenerico servGenerico) : base(datos)
        {
            ServicioGenerico = servGenerico;
        }

        public void AgregarFuncionalidad(Funcionalidad funcionalidad, IList<FuncionalidadRol> funcionalidadesRolesAAgregar)
        {
            using TransactionScope scope = new ();

            this._datos.Add(funcionalidad);

            if (funcionalidadesRolesAAgregar != null)
            {
                foreach (FuncionalidadRol fr in funcionalidadesRolesAAgregar)
                {
                    fr.IdFuncionalidad = funcionalidad.Id;
                    ServicioGenerico.Add(fr);

                }
            }
            scope.Complete();
        }

        public void ModificarFuncionalidad(Funcionalidad funcionalidad, IList<FuncionalidadRol> funcionalidadesRolesAModificar)
        {

            using TransactionScope scope = new ();
            this.ServicioGenerico.Update(funcionalidad);

            List<FuncionalidadRol> funcRoles = ServicioGenerico.GetAll<FuncionalidadRol>(r => r.IdFuncionalidad == funcionalidad.Id).ToList();

            //roles a agregar
            List<FuncionalidadRol> listaRolesAAgregar = funcionalidadesRolesAModificar.Where(ra => !funcRoles.Any(r => r.IdRol == ra.IdRol)).ToList();
            listaRolesAAgregar.ForEach(rol => ServicioGenerico.Add<FuncionalidadRol>(rol));

            //roles a eliminar
            List<FuncionalidadRol> listaRelAEliminar = funcRoles.Where(re => !funcionalidadesRolesAModificar.Any(r => r.IdRol == re.IdRol)).ToList();
            listaRelAEliminar.ForEach(rol => ServicioGenerico.Delete<FuncionalidadRol>(rol));

            scope.Complete();
        }


    }
}
