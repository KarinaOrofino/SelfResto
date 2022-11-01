using Framework.Common;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;

namespace Framework.Utils
{
    public static class UserUtils
    {
        public static int GetId(ClaimsPrincipal user)
        {
            return int.Parse(user.Claims.First(u => u.Type == ClaimTypes.Sid).Value);
        }

        public static string GetName(ClaimsPrincipal user)
        {
            return user.Claims.First(u => u.Type == ClaimTypes.Name).Value.ToString() + ' ' + user.Claims.First(u => u.Type == ClaimTypes.Surname).Value.ToString();
        }

        public static string GetRole(ClaimsPrincipal user)
        {
            return user.Claims.First(u => u.Type == ClaimTypes.Role).Value.ToString();
        }

        public static bool UserPermission(ClaimsPrincipal user, int idAccessType)
        {
            if (!user.Claims.Any(claim => claim.Type == Constants.CLAIMS_PERMISOS))
                return false;

            int permiso = JsonConvert.DeserializeObject<int>(user.Claims.First(u => u.Type == Constants.CLAIMS_PERMISOS).Value);

            return permiso == idAccessType;
        }

    }
}
