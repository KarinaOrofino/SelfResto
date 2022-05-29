using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.IO;
using Framework.Common;
using System.Data.SqlClient;

namespace Framework.Datos
{
    public class WrapperSqlServerConnection : IDisposable
    {
        Guid idObjeto;
        public DateTime Timestamp { get; private set; }

        SqlConnection conn = null;
        string _connectionString;

        public void BeginInit()
        {
            Timestamp = DateTime.UtcNow;
            idObjeto = System.Guid.NewGuid();

            conn = new SqlConnection(_connectionString);
            conn.Open();
            Trace.WriteLine(idObjeto + " - Metodo BeginInit");
            Trace.WriteLine(idObjeto + " - ConnectionString: " + conn.ConnectionString.ToString());
            Trace.WriteLine(idObjeto + " - State: " + conn.State.ToString());

        }

        public void EndInit()
        {
            Trace.WriteLine(idObjeto + " - Metodo EndInit");
        }

        public WrapperSqlServerConnection()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();
            _connectionString = root.GetSection("ConnectionStrings").GetSection(Constantes.DB_CONFIG_KEY).Value;

        }

        public SqlConnection GetConnection()
        {
            Trace.WriteLine(idObjeto.ToString() + " - Metodo GetConnection");
            return conn;
        }

        public void Destroy()
        {
            Trace.WriteLine(idObjeto.ToString() + " - Metodo Destroy");
        }

        public void Dispose()
        {
            Trace.WriteLine(idObjeto.ToString() + " - Metodo Dispose");
            try
            {
                conn.Close();
                // conn.Dispose(); // no es necesario, el close alcanza
            }
            catch
            { }
            conn = null;
        }

        public string IdObjeto()
        {
            return idObjeto.ToString();
        }
    }
}
