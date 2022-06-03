using Framework.Common;
using KO.Datos.EFScafolding;
using KO.Datos.Interfaces;
using KO.Entidades;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

namespace KO.Datos.Implementacion
{
    public class DatosVMedicos : DatosBase, IDatosVMedicos
    {
        public DatosVMedicos(IConfiguration configuration, KOContext context) : base(context)
        {
        }

        public List<Medico> ObtenerTodos()
        {
            List<Medico> listaMedicos = new();
            try
            {
                using SqlCommand command = new(Constantes.SP_MEDICOS_OBTENER_TODOS, (SqlConnection)_context.Database.GetDbConnection());
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
                            Medico medico = new()
                            {
                                Matricula = int.Parse(dataRow["Matricula"].ToString()),
                                Nombre = dataRow["Nombre"].ToString(),
                                Apellido = dataRow["Apellido"].ToString(),
                                Estado = (bool)dataRow["Estado"]
                            };
                            listaMedicos.Add(medico);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error método en Datos", ex);
                throw new Exception(ex.Message);
            }

            return listaMedicos;
        }

        public List<Medico> ObtenerFiltrados(string campoBusqueda, bool? estado)
        {
            List<Medico> listaMedicos = new ();
            try
            {
                using SqlCommand command = new(Constantes.SP_MEDICOS_OBTENER_FILTRADOS, (SqlConnection)_context.Database.GetDbConnection());
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
                            Medico medico = new ();
                            medico.Matricula = int.Parse(row["Matricula"].ToString());
                            medico.Nombre = row["Nombre"].ToString();
                            medico.Apellido = row["Apellido"].ToString();
                            medico.Estado = (bool)row["Estado"];
                            listaMedicos.Add(medico);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error método en Datos", ex);
                throw new Exception(ex.Message);
            }

            return listaMedicos;
        }

        public Medico Obtener(int Matricula)
        {
            Medico medico = new();
            try
            {
                using SqlCommand command = new(Constantes.SP_MEDICO_OBTENER_POR_MATRICULA, (SqlConnection)_context.Database.GetDbConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Matricula", Matricula);

                using SqlDataAdapter da = new(command);
                DataTable dt = new();
                da.Fill(dt);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dataRow = dt.Rows[0];

                        medico = new Medico
                        {
                            Matricula = int.Parse(dataRow["Matricula"].ToString()),
                            Nombre = dataRow["Nombre"].ToString(),
                            Apellido = dataRow["Apellido"].ToString(),
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

            return medico;
        }

        public void Agregar(Medico medico)
        {
            try
            {
                var connection = (SqlConnection)_context.Database.GetDbConnection();
                using SqlCommand command = new(Constantes.SP_MEDICO_AGREGAR, connection);

                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Matricula", medico.Matricula);
                command.Parameters.AddWithValue("@Nombre", medico.Nombre);
                command.Parameters.AddWithValue("@Apellido", medico.Apellido);

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

        public void Actualizar(Medico medico)
        {
            try
            {
                var connection = (SqlConnection)_context.Database.GetDbConnection();
                using SqlCommand command = new(Constantes.SP_MEDICO_ACTUALIZAR, connection);

                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                
                command.Parameters.AddWithValue("@Matricula", medico.Matricula);
                command.Parameters.AddWithValue("@Apellido", medico.Apellido);
                command.Parameters.AddWithValue("@Nombre", medico.Nombre);
                command.Parameters.AddWithValue("@Estado", medico.Estado);
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

        public void Inactivar(int Matricula)
        {
            try
            {
                var connection = (SqlConnection)_context.Database.GetDbConnection();
                using SqlCommand command = new(Constantes.SP_MEDICO_INACTIVAR, connection);

                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Matricula", Matricula);

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

        public void Activar(int Matricula)
        {
            try
            {
                var connection = (SqlConnection)_context.Database.GetDbConnection();
                using SqlCommand command = new(Constantes.SP_MEDICO_ACTIVAR, connection);

                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Matricula", Matricula);

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
