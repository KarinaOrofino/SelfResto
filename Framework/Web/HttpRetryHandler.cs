using log4net;
using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace KO.Framework.Web
{
    public class HttpRetryHandler : DelegatingHandler
    {
        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private int MaxRetries;
        private int WaitTime;

        public HttpRetryHandler(HttpMessageHandler innerHandler, int waitMiliseconds = 10000, int maxRetries = 3)
            : base(innerHandler)
        {
            MaxRetries = maxRetries;
            WaitTime = waitMiliseconds;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;
            var currentWaitTime = WaitTime;
            for (int i = 0; i < MaxRetries; i++)
            {
                log.Info($"Try n°{i}");
                currentWaitTime = (int) currentWaitTime * (i + 1);
                try
                {
                    response = await base.SendAsync(request, cancellationToken);
                    if (response.IsSuccessStatusCode)
                    {
                        return response;
                    }
                    else if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        // Don't reattempt a bad request
                        break;
                    }
                }
                catch (Exception e)
                {
                    // Ignore Error As We Will Attempt Again
                    log.Error("Error, retrying...", e);
                }

                if (currentWaitTime > 0)
                {
                    log.Info($"Task.Delay Start for {currentWaitTime}ms...");
                    await Task.Delay(currentWaitTime);
                    log.Info("Task.Delay End...");
                }
            }

            return response;
        }
    }
}
