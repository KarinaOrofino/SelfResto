using Framework.Common;
using log4net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Framework.Web;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using KO.Framework.Web;
using KO.Entities;
using KO.Services.Interfaces;
using KO.Resources;
using System;

namespace KO.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        IConfiguration Configuration;

        IUsersService IUsersService { get; set; }

        public AuthenticateService(IConfiguration configuration, IUsersService UsersService)
        {
            this.Configuration = configuration;
            this.IUsersService = UsersService;
        }

        public JsonApiData AutenticarUsuarioAplicacion(string email, string password)
        {

            try
            {
                User user = IUsersService.GetByEmail(email);

                if (user.Email == null)
                {
                    return new JsonApiData() { message = Global.MsgNotAUser, result = JsonApiData.Result.Error };
                }

                if (!user.Active)
                {
                    return new JsonApiData() { message = Global.MsgNotAnActiveUser, result = JsonApiData.Result.Error };
                }

                if ((user.Password != password))
                {
                    return new JsonApiData() { message = Global.MsgIncorrectPassword, result = JsonApiData.Result.Error };
                }

                else {
                    return new JsonApiData()
                    {
                        result = JsonApiData.Result.Ok,
                        message = JsonConvert.SerializeObject(
                            new
                            {
                                id = user.Id,
                                name = user.Name,
                                surname = user.Surname,
                                email = user.Email,
                                access_Type = user.Access_Type,
                                accessTypeName = user.AccessTypeName
                            })
                    };
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return new JsonApiData() { message = Global.GenericError, result = JsonApiData.Result.Error };
            }
        }

    }
}
