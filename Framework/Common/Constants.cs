namespace Framework.Common
{
    public static class Constants
    {
        #region Keys
        public const string DOMAIN_NAME_KEY = "Domain_Name";
        public const string DOMAIN_SERVER_KEY = "Domain_Server";
        public const string PORT_LDAP_KEY = "Port_LDAP";
        public const string DB_CONFIG_KEY = "CDCContext";
        public const string CULTURE_DEFAULT = "es-AR";
        public const string GENERIC_PASSWORD = "CODES.-.PPAASSWWOORRDD";

        public const int ERROR_HTTP = 500;
        public const string CLAIMS_PERMISOS = "Permisos";
        public const string GENERAL_USER = "GeneralUser";
        public const string ADMINISTRATOR = "Admin";
        public const string IDAPP = "SelfRestoApp";

        #endregion

        #region Stored Procedures
        public const string SP_CATEGORIES_GET_ALL = "sp_categories_get_all";
        public const string SP_MENUITEMS_GET_ALL = "sp_menuItems_get_all";
        public const string SP_MENUITEMS_GET_ALL_FILTERED = "sp_menuItems_get_all_filtered";
        public const string SP_MENUITEMS_GET_BY_ID = "sp_menuItems_get_by_id";
        public const string SP_MENUITEMS_CREATE = "sp_menuItems_create";
        public const string SP_MENUITEMS_UPDATE = "sp_menuItems_update";
        public const string SP_MENUITEMS_ACTIVATE = "sp_menuItems_activate";
        public const string SP_MENUITEMS_INACTIVATE = "sp_menuItems_inactivate";
        public const string SP_USERS_GET_ALL = "sp_users_get_all";
        public const string SP_USERS_GET_ALL_FILTERED = "sp_users_get_all_filtered";
        public const string SP_USERS_GET_BY_ID = "sp_users_get_by_id";
        public const string SP_USERS_GET_BY_EMAIL = "sp_users_get_by_email";
        public const string SP_USERS_CREATE = "sp_users_create";
        public const string SP_USERS_UPDATE = "sp_users_update";
        public const string SP_USERS_ACTIVATE = "sp_users_activate";
        public const string SP_USERS_INACTIVATE = "sp_users_inactivate";


        #endregion







    }
}
