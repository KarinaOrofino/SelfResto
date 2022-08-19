namespace Framework.Data
{
    public abstract class DBData
    {
        protected WrapperSqlServerConnection connection = null;

        public DBData()
        {
            this.connection = new WrapperSqlServerConnection();
        }
    }
}
