using KO.Datos.EFScafolding;
using KO.Datos.Interfaces;
using KO.Entidades;
using Framework.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System;

namespace KO.Datos.Implementacion
{
    public class DatosVVacunas : DatosBase, IDatosVVacunas
    {

        public DatosVVacunas(IConfiguration configuration, KOContext context) : base(context)
        {
        }

        public List<Vacuna> ObtenerTodas()
        {
            List<Vacuna> listaVacunas = new();
            try
            {
                using SqlCommand command = new(Constantes.SP_VACUNAS_OBTENER_TODAS, (SqlConnection)_context.Database.GetDbConnection());
                command.CommandType = CommandType.StoredProcedure;

                using SqlDataAdapter da = new(command);
                DataTable dt = new();
                da.Fill(dt);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        foreach (DataRow dataRow in dt.Rows)
                        {
                            Vacuna vacuna = new()
                            {
                                Id = int.Parse(dataRow["Id"].ToString()),
                                Nombre = dataRow["Nombre"].ToString(),
                                IdMarca = int.Parse(dataRow["IdMarca"].ToString()),
                                Marca = dataRow["Marca"].ToString(),
                                Estado = (bool)dataRow["Estado"]
                            };
                            listaVacunas.Add(vacuna);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error método en Datos", ex);
                throw new Exception(ex.Message);
            }

            return listaVacunas;
        }

        public List<Vacuna> ObtenerFiltradas(string campoBusqueda, bool? estado)
        {
            List<Vacuna> listaVacunas = new ();
            try
            {
                using SqlCommand command = new(Constantes.SP_VACUNAS_OBTENER_FILTRADAS, (SqlConnection)_context.Database.GetDbConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("CampoBusqueda", campoBusqueda);
                command.Parameters.AddWithValue("Estado", estado);

                using SqlDataAdapter da = new (command);
                DataTable dt = new ();
                da.Fill(dt);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        foreach (DataRow row in dt.Rows)
                        {
                            Vacuna vacuna = new ()
                            {
                                Id = int.Parse(row["Id"].ToString()),
                                Nombre = row["Nombre"].ToString(),
                                Marca = row["Marca"].ToString(),
                                Estado = (bool)row["Estado"]
                            };
                        listaVacunas.Add(vacuna);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error método en Datos", ex);
                throw new Exception(ex.Message);
            }

            return listaVacunas;
        }

        public Vacuna Obtener(int id)
        {
            Vacuna vacuna = new();
            try
            {
                using SqlCommand command = new(Constantes.SP_VACUNA_OBTENER_POR_ID, (SqlConnection)_context.Database.GetDbConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                using SqlDataAdapter da = new(command);
                DataTable dt = new();
                da.Fill(dt);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dataRow = dt.Rows[0];

                        vacuna = new Vacuna
                        {
                            Id = int.Parse(dataRow["Id"].ToString()),
                            Nombre = dataRow["Nombre"].ToString(),
                            Marca = dataRow["Marca"].ToString(),
                            Estado = (bool)dataRow["Estado"]
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error método en Datos", ex);
                throw new Exception(ex.Message);
            }

            return vacuna;
        }

        public void Agregar(Vacuna vacuna)
        {
            try
            {
                var connection = (SqlConnection)_context.Database.GetDbConnection();
                using SqlCommand command = new(Constantes.SP_VACUNA_AGREGAR, connection);

                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Nombre", vacuna.Nombre);
                command.Parameters.AddWithValue("@Marca", vacuna.Marca);

                command.ExecuteNonQuery();

                connection.Dispose();
                connection.Close();

            }
            catch (Exception ex)
            {
                log.Error("Error método en Datos", ex);
                throw new Exception(ex.Message);
            }
        }

        public void Actualizar(Vacuna vacuna)
        {
            try
            {
                var connection = (SqlConnection)_context.Database.GetDbConnection();
                using SqlCommand command = new(Constantes.SP_VACUNA_ACTUALIZAR, connection);

                connection.Open();
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", vacuna.Id);
                command.Parameters.AddWithValue("@Nombre", vacuna.Nombre);
                command.Parameters.AddWithValue("@Estado", vacuna.Estado);
                command.ExecuteNonQuery();

                connection.Dispose();
                connection.Close();

            }
            catch (Exception ex)
            {
                log.Error("Error método en Datos", ex);
                throw new Exception(ex.Message);
            }
        }

        public void Inactivar(int id)
        {
            try
            {
                var connection = (SqlConnection)_context.Database.GetDbConnection();
                using SqlCommand command = new(Constantes.SP_VACUNA_INACTIVAR, connection);

                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();

                connection.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                log.Error("Error método en Datos", ex);
                throw new Exception(ex.Message);
            }

        }

        public void Activar(int id)
        {
            try
            {
                var connection = (SqlConnection)_context.Database.GetDbConnection();
                using SqlCommand command = new(Constantes.SP_VACUNA_ACTIVAR, connection);

                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();

                connection.Dispose();
                connection.Close();

            }
            catch (Exception ex)
            {
                log.Error("Error método en Datos", ex);
                throw new Exception(ex.Message);
            }
        }
    }
}
