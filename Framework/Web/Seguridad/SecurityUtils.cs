using Framework.Helpers;
using RHSP.Framework.Utils;
using KO.Framework.Web;
using System;

namespace KO.Framework.Utils
{
    public static class SecurityUtils
    {
        public static string BuildTokenForMethod(string method)
        {
            var secretKey = "token";
            var timestamp = EpochUtils.ConvertToEpoch(DateTime.Now);
            
            return EncryptionHelper.Encrypt(secretKey + "|" + timestamp + "|" + method);
        }

        public static bool IsValidServicioRequestForMethod(ServicioRequest servicioRequest,string method)
        {
            int POS_SECRET_KEY = 0;
            int POS_TIMESTAMP = 1;
            int POS_METHOD = 2;

            var tokenDataString = EncryptionHelper.Decrypt(servicioRequest.Token);
            var tokenData = tokenDataString.Split("|");

            var secretKey = tokenData[POS_SECRET_KEY];

            //IMPLEMENTAR app.config
            if (secretKey != "token"){
                return false;
            }

            var epochTimestamp = tokenData[POS_TIMESTAMP];
            var timestampDate = EpochUtils.ConvertFromEpoch(epochTimestamp);
            var timeDiference = DateTime.Now - timestampDate;

            //IMPLEMENTAR app.config
            if (timeDiference.TotalSeconds > 60 * 3){
                return false;
            }

            var requestedMethod = tokenData[POS_METHOD];
            if(requestedMethod != method){
                return false;
            }

            return true;
        }
    }
}
