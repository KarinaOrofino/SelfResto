using log4net;
using Microsoft.Extensions.Configuration;
using KO.Entidades;
using KO.Servicios.Interfaces;
using System.Collections.Generic;
using System.Reflection;

namespace KO.Servicios
{
    public class ServicioRolesMock : IServicioRoles
    {
        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        IConfiguration Configuration;
        public ServicioRolesMock(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public List<Rol> ObtenerRoles(string idApp)
        {
        List<Rol> roles = new List<Rol>();
            roles.Add(new Rol() { Id = 1, Descripcion = "Operario", Funcion = "General", Activo = true });
            roles.Add(new Rol() { Id = 4, Descripcion = "Administrador", Funcion = "General", Activo = true });

            return roles;
        }

    }
}
