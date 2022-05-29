using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Common
{
    public static class Constantes
    {
        #region Keys
        public const string NOMBRE_DOMINIO_KEY = "Nombre_Dominio";
        public const string SERVIDOR_DOMINIO_KEY = "Servidor_Dominio";
        public const string PUERTO_LDAP_KEY = "Puerto_LDAP";
        public const string DB_CONFIG_KEY = "CDCContext";
        public const string CULTURA_DEFAULT = "es-AR";
        public const string GENERIC_PASSWORD = "CODES.-.PPAASSWWOORRDD";

        public const string SGAASERVICE_KEY_URL = "SGAAService.URL";
        public const string SGAASERVICE_KEY_URLRoles = "SGAAService.URLRoles";
        public const string SGAASERVICE_KEY_USERNAME = "SGAAService.Auth.userName";
        public const string SGAASERVICE_KEY_PASSWORD = "SGAAService.Auth.password";
        public const string SGAASERVICE_KEY_IDAPP = "SGAAService.Auth.idApp";
        public const string SGAASERVICE_KEY_URLEVENTO = "SGAAService.URLEvento";
        public const string IDAPP = "SGAAService.idApp";
        public const string SGAASERVICE_KEY_EVENT = "SGAAService.nombreEvento";
        public const int ERROR_HTTP = 500;
        public const int OPERARIO = 20;
        public const int ADMINISTRADOR = 20;
        public const string CLAIMS_PERMISOS = "Permisos";
        public const string KO_OPERARIO = "Operario";
        public const string KO_ADMINISTRADOR = "Administrador";

        #endregion

        #region Stored Procedures
        public const string SP_OBTENER_TODOS_LOS_MEDICOS = "sp_obtener_todos_los_medicos";
        public const string SP_OBTENER_MEDICO = "sp_obtener_medico";
        public const string SP_AGREGAR_MEDICO = "sp_agregar_medico";
        public const string SP_ACTUALIZAR_MEDICO = "sp_actualizar_medico";
        public const string SP_INACTIVAR_MEDICO = "sp_inactivar_medico";
        public const string SP_ACTIVAR_MEDICO = "sp_activar_medico";
        public const string SP_OBTENER_VACUNAS = "sp_obtener_todas_las_vacunas";
        public const string SP_AGREGAR_VACUNAS = "sp_agregar_vacuna";
        public const string SP_ACTUALIZAR_VACUNA = "sp_actualizar_vacuna";
        public const string SP_INACTIVAR_VACUNA = "sp_inactivar_vacuna";
        public const string SP_ACTIVAR_VACUNA = "sp_activar_vacuna";
        public const string SP_OBTENER_PACIENTES = "sp_obtener_todos_los_pacientes";
        public const string SP_AGREGAR_PACIENTE = "sp_agregar_paciente";
        public const string SP_ACTUALIZAR_PACIENTE = "sp_actualizar_paciente";
        public const string SP_INACTIVAR_PACIENTE = "sp_inactivar_paciente";
        public const string SP_ACTIVAR_PACIENTE = "sp_ictivar_paciente";
        public const string SP_OBTENER_APLICACIONES_FILTRADAS = "sp_obtener_aplicaciones_filtradas";
        public const string SP_AGREGAR_APLICACION = "sp_agregar_aplicacion";
        public const string SP_ACTUALIZAR_APLICACION = "sp_actualizar_aplicacion";
        public const string SP_INACTIVAR_APLICACION = "sp_inactivar_aplicacion";

        #endregion

        #region Usuarios
        public const string SP_USUARIOS_GET_BY_LOGIN = "sp_usuarios_get_by_login";
        public const string SP_USUARIOS_GET_BY_EMAIL = "sp_usuarios_get_by_email";
        public const string SP_USUARIOS_GET_BY_USUARIO = "sp_usuarios_get_by_usuario";
        public const string SP_USUARIOS_GET_ALL = "sp_usuarios_get_all";
        public const string SP_USUARIOS_GET_BY_ID = "sp_usuarios_get_by_id";
        #endregion







    }
}
