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
        private IConfiguration Configuration;

        public DatosVMedicos(IConfiguration configuration, KOContext context) : base(context)
        {
            this.Configuration = configuration;
        }

        public List<Medico> ObtenerTodos()
        {
            List<Medico> listaMedicos = new();
            try
            {
                using SqlCommand command = new(Constantes.SP_OBTENER_TODOS_LOS_MEDICOS, (SqlConnection)_context.Database.GetDbConnection());
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

        public Medico Obtener(int Matricula)
        {
            Medico medico = new();
            try
            {
                using SqlCommand command = new(Constantes.SP_OBTENER_MEDICO, (SqlConnection)_context.Database.GetDbConnection());
                command.CommandType = CommandType.StoredProcedure;

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
                using SqlCommand command = new(Constantes.SP_AGREGAR_MEDICO, connection);

                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
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
                using SqlCommand command = new(Constantes.SP_ACTUALIZAR_MEDICO, connection);

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
                using SqlCommand command = new(Constantes.SP_INACTIVAR_MEDICO, connection);

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
                using SqlCommand command = new(Constantes.SP_ACTIVAR_MEDICO, connection);

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
