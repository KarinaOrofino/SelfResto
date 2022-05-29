namespace Framework.Datos
{
    public abstract class DBDatos
    {
        protected WrapperSqlServerConnection connection = null;

        public DBDatos()
        {
            this.connection = new WrapperSqlServerConnection();
        }
    }
}
