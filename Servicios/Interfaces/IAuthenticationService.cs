using KO.Framework.Web;

namespace KO.Services.Interfaces
{
    public interface IAuthenticateService
    {
        JsonApiData AutenticarUsuarioAplicacion(string usuario, string password);

    }
}
