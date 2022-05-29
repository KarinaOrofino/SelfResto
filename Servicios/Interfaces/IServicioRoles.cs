using KO.Entidades;
using System.Collections.Generic;

namespace KO.Servicios.Interfaces
{
    public interface IServicioRoles
    {
        List<Rol> ObtenerRoles(string idApp);

    }
}
