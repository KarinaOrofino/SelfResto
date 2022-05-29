using Framework.Common;
using log4net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using KO.Framework.Web;
using KO.Servicios.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;

namespace KO.Servicios
{
    public class ServicioLogEventos : IServicioLogEventos
    {
        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        readonly IConfiguration Configuration;
        public ServicioLogEventos(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public JsonApiData LogInfo(string usuario, string idApp, string nombreEvento)
        {
            var httpClient = new HttpClient(new HttpRetryHandler(new HttpClientHandler()), false);
            var urlToSend = this.Configuration[Constantes.SGAASERVICE_KEY_URLEVENTO];

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, urlToSend)
            {
                Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>(){
                new KeyValuePair<string, string>(Configuration[Constantes.SGAASERVICE_KEY_USERNAME],usuario),
                new KeyValuePair<string, string>(Configuration[Constantes.SGAASERVICE_KEY_IDAPP],idApp),
                new KeyValuePair<string, string>(Configuration[Constantes.SGAASERVICE_KEY_EVENT], nombreEvento)

            })
            };

            var response = httpClient.SendAsync(requestMessage).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
            var apiResponse = JsonConvert.DeserializeObject<JsonApiData>(responseString);
            apiResponse.content = JsonConvert.DeserializeObject<dynamic>(apiResponse.message);

            return apiResponse;
        }
    }
}
