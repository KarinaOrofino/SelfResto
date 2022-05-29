using Framework.Common;
using Newtonsoft.Json;
using KO.Framework.Web;
using KO.Servicios.Interfaces;

namespace KO.Servicios
{
    public class ServicioAutenticacionMock : IServicioAutenticacion
    {
        public JsonApiData AutenticarUsuarioAplicacion(string usuario, string password, string idApp)
        {
            string data = string.Empty;

            switch (usuario)
            {
                case "CODES\\mmollo":
                    data = JsonConvert.SerializeObject(
                                                    new
                                                    {
                                                        id = 918,
                                                        rol = Constantes.KO_ADMINISTRADOR,
                                                        nombre = "Micaela Mollo",
                                                        email = "mmollo@codes.com.ar",
                                                        login = "CODES\\mmollo"
                                                    });
                    
                    break;
                case "CODES\\korofino":
                    data = JsonConvert.SerializeObject(
                                                    new
                                                    {
                                                        id = 919,
                                                        rol = Constantes.KO_OPERARIO,
                                                        nombre = "Karina Orofino",
                                                        email = "korofino@codes.com.ar",
                                                        login = "CODES\\korofino"
                                                    });
                    break;
                
                default:
                    data = "ERROR";
                    break;
            }

            if (data != "ERROR")
            {
                return new JsonApiData()
                {
                    result = JsonApiData.Result.Ok,
                    message = data,
                    content = JsonConvert.DeserializeObject<dynamic>(data)
                };
            }
            else {
                return new JsonApiData() { result = JsonApiData.Result.Error, message ="MOCK ERROR!" };
            }
        }
 
    }
}
