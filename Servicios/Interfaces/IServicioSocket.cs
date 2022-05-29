using System.Threading.Tasks;

namespace KO.Servicios.Interfaces
{
    public interface IServicioSocket
    {
        Task<string> SendAndWaitForResponse(string content);
    }
}
