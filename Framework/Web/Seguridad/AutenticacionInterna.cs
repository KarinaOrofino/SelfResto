using Framework.Common;
using log4net;
using Microsoft.Extensions.Configuration;
using Novell.Directory.Ldap;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Framework.Web.Seguridad
{
    public interface IAutenticacionInterna
    {
        Task<bool> ValidarUsuarioInternoActiveDirectory(string nombreDeUsuario, string password);
        string GetUsuarioDominio(string nombreusuario);
    }
    public class AutenticacionInterna : IAutenticacionInterna
    {
        protected readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private string NombreDominioCompleto { get; set; }
        protected IConfiguration _iconfiguration { get; set; }
        public AutenticacionInterna(IConfiguration configuration)
        {
            _iconfiguration = configuration;
        }

        private void ResolverNombreUsuario(string nombreDeUsuario)
        {
            this.NombreDominioCompleto = nombreDeUsuario;

            string nombreDominio = _iconfiguration[Constantes.NOMBRE_DOMINIO_KEY].ToString();

            if (!nombreDeUsuario.Contains(nombreDominio))
            {
                NombreDominioCompleto = string.Concat(nombreDominio, "\\", nombreDeUsuario);
            }
        }

        /// <summary>
        /// Verifica si el usuario tiene acceso al dominio
        /// </summary>
        /// <param name="nombreDeUsuario"> </param>
        /// <param name="password"> </param>
        /// <returns></returns>
        public Task<bool> ValidarUsuarioInternoActiveDirectory(string nombreDeUsuario, string password)
        {
            bool usuarioValido = false;
            try
            {
                ResolverNombreUsuario(nombreDeUsuario);

                string servidorDominio = _iconfiguration[Constantes.SERVIDOR_DOMINIO_KEY].ToString();
                int puertoLDAP = int.Parse(_iconfiguration[Constantes.PUERTO_LDAP_KEY].ToString());

                using (var cn = new LdapConnection())
                {
                    // connect
                    cn.Connect(servidorDominio, puertoLDAP);
                    cn.Bind(NombreDominioCompleto, password);
                    usuarioValido = true;
                }
            }
            catch (LdapException lexc)
            {
                log.Error(lexc.Message);
            }
            catch (Exception exc)
            {
                log.Error(exc.Message);
            }

            return Task.FromResult(usuarioValido);
        }

        /// <summary>
        /// Concatena el nombre del usuario con el nombre del dominio
        /// </summary>
        /// <param name="nombreusuario"></param>
        /// <returns></returns>
        public string GetUsuarioDominio(string nombreusuario)
        {
            ResolverNombreUsuario(nombreusuario);
            return this.NombreDominioCompleto;
        }
    }
}
