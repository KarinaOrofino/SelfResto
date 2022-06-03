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
        public const string SPAPLICACIONES__OBTENER_FILTRADAS = "sp_aplicaciones_obtener_filtradas";
        public const string SP_APLICACION_AGREGAR = "sp_aplicacion_agregar";
        public const string SP_APLICACION_ACTUALIZAR = "sp_aplicacion_actualizar";
        //public const string SP_INACTIVAR_APLICACION = "sp_inactivar_aplicacion";
        public const string SP_MEDICOS_OBTENER_TODOS = "sp_medicos_obtener_todos";
        public const string SP_MEDICOS_OBTENER_FILTRADOS = "sp_medicos_obtenerfiltrados";
        public const string SP_MEDICO_OBTENER_POR_MATRICULA = "sp_medico_obtener_por_matricula";
        public const string SP_MEDICO_AGREGAR = "sp_medico_agregar";
        public const string SP_MEDICO_ACTUALIZAR = "sp_medico_actualizar";
        public const string SP_MEDICO_INACTIVAR = "sp_medico_inactivar";
        public const string SP_MEDICO_ACTIVAR = "sp_medico_activar";
        public const string SP_PACIENTES_OBTENER_TODOS= "sp_pacientes_obtener_todos";
        public const string SP_PACIENTES_OBTENER_FILTRADOS = "sp_pacientes_obtener_filtrados";
        public const string SP_PACIENTE_OBTENER_POR_ID = "sp_paciente_obtener_por_id";
        public const string SP_PACIENTE_AGREGAR = "sp_paciente_agregar";
        public const string SP_PACIENTE_ACTUALIZAR = "sp_paciente_actualizar";
        public const string SP_VACUNAS_OBTENER_TODAS = "sp_vacunas_obtener_todas";
        public const string SP_VACUNAS_OBTENER_FILTRADAS = "sp_vacunas_obtener_filtradas";
        public const string SP_VACUNA_AGREGAR = "sp_vacuna_agregar";
        public const string SP_VACUNA_OBTENER_POR_ID = "sp_vacuna_obtener_por_id";
        public const string SP_VACUNA_ACTUALIZAR= "sp_vacuna_actualizar";
        public const string SP_VACUNA_INACTIVAR = "sp_vacuna_inactivar";
        public const string SP_VACUNA_ACTIVAR = "sp_vacuna_activar";
        public const string SP_OBRAS_SOCIALES_OBTENER = "sp_obras_sociales_obtener";



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
