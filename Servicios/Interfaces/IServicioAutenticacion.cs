using KO.Framework.Web;

namespace KO.Servicios.Interfaces
{
    public interface IServicioAutenticacion
    {
        JsonApiData AutenticarUsuarioAplicacion(string usuario, string password, string idApp);

    }
}
