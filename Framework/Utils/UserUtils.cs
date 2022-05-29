using Framework.Common;
using Newtonsoft.Json;
using KO.Entidades;
using System.Collections.Generic;
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
            return user.Claims.First(u => u.Type == ClaimTypes.Name).Value;
        }

        public static string GetRol(ClaimsPrincipal user)
        {
            return user.Claims.First(u => u.Type == ClaimTypes.Role).Value;
        }

        public static bool UsuarioTienePermiso(ClaimsPrincipal user, int idFuncionalidad)
        {
            if (!user.Claims.Any(claim => claim.Type == Constantes.CLAIMS_PERMISOS))
                return false;

            IList<int> permisos = JsonConvert.DeserializeObject<IList<int>>(user.Claims.First(u => u.Type == Constantes.CLAIMS_PERMISOS).Value).ToList();
            return permisos.Contains(idFuncionalidad);
        }
}
}
