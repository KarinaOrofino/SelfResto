using Microsoft.Extensions.Configuration;
using KO.Servicios.Interfaces;
using System.Threading.Tasks;

namespace KO.Servicios
{
    public class ServicioSocketMock : IServicioSocket
    {
        public ServicioSocketMock(IConfiguration configuration)
        {

        }

        public Task<string> SendAndWaitForResponse(string content)
        {
            return Task.Run(() => "ServicioSocketMock");
        }
    }
}
