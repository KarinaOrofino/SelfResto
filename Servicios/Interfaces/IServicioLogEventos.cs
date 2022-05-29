using KO.Framework.Web;

namespace KO.Servicios.Interfaces
{
    public interface IServicioLogEventos
    {
        JsonApiData LogInfo(string usuario, string nombreApp, string evento);
    }
}
