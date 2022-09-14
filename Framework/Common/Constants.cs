namespace Framework.Common
{
    public static class Constants
    {
        #region Keys
        public const string DOMAIN_NAME_KEY = "Domain_Name";
        public const string DOMAIN_SERVER_KEY = "Domain_Server";
        public const string PORT_LDAP_KEY = "Port_LDAP";
        public const string DB_CONFIG_KEY = "SelfRestoContext";
        public const string CULTURE_DEFAULT = "es-AR";
        public const string GENERIC_PASSWORD = "CODES.-.PPAASSWWOORRDD";
        public const int ERROR_HTTP = 500;
        public const string IDAPP = "SelfRestoApp";
        #endregion

        #region Keys
        public const string CLAIMS_PERMISOS = "Permisos";
        public const int CLIENT = 10;
        public const int ADMINISTRATOR = 6;
        public const int WAITER = 11;
        public const int KITCHEN = 13;
        #endregion

        #region Stored Procedures
        public const string SP_CATEGORIES_GET_ALL = "sp_categories_get_all";
        public const string SP_MENUITEMS_GET_ALL_FILTERED = "sp_menuItems_get_all_filtered";
        public const string SP_MENUITEMS_GET_ALL_FILTERED_BY_CATID = "sp_menuItems_get_all_filtered_by_catId";
        public const string SP_USERS_GET_ALL_FILTERED = "sp_users_get_all_filtered";
        public const string SP_USERS_GET_BY_EMAIL = "sp_users_get_by_email";
        public const string SP_TABLES_GET_ALL_FILTERED = "sp_tables_get_all_filtered";

        #endregion







    }
}
