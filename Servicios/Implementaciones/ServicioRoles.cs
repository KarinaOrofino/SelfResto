using Framework.Common;
using log4net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using KO.Entidades;
using KO.Framework.Web;
using KO.Servicios.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;

namespace KO.Servicios
{
    public class ServicioRoles : IServicioRoles
    {
        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        readonly IConfiguration Configuration;
        public ServicioRoles(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public List<Rol> ObtenerRoles(string idApp)
        {            
            var httpClient = new HttpClient(new HttpRetryHandler(new HttpClientHandler()), false);
            var urlToSend = this.Configuration[Constantes.SGAASERVICE_KEY_URLRoles];

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, urlToSend)
            {
                Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>(){
                new KeyValuePair<string, string>(Configuration[Constantes.SGAASERVICE_KEY_IDAPP],idApp)
            })
            };

            var response = httpClient.SendAsync(requestMessage).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
            var apiResponse = JsonConvert.DeserializeObject<JsonApiData>(responseString);
            List<Rol> roles = JsonConvert.DeserializeObject<List<Rol>>(apiResponse.message);

            return roles;
        }

    }
}
