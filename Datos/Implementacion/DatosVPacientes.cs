using KO.Datos.EFScafolding;
using KO.Datos.Interfaces;
using KO.Entidades;
using Framework.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System;
using System.Collections.Generic;

namespace KO.Datos.Implementacion
{
    public class DatosVPacientes : DatosBase, IDatosVPacientes
    {
        public DatosVPacientes(IConfiguration configuration, KOContext context) : base(context)
        {
        }

        public List<Paciente> ObtenerTodos()
        {
            List<Paciente> listaPacientes = new();
            try
            {
                using SqlCommand command = new(Constantes.SP_PACIENTES_OBTENER_TODOS, (SqlConnection)_context.Database.GetDbConnection());
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
                            Paciente Paciente = new()
                            {
                                Id = int.Parse(dataRow["Id"].ToString()),
                                Nombre = dataRow["Nombre"].ToString(),
                                Apellido = dataRow["Apellido"].ToString(),
                                ObraSocial = dataRow["ObraSocial"].ToString(),
                                NumeroObraSocial = long.Parse(dataRow["NumeroObraSocial"].ToString()),
                                FechaNacimiento = (DateTime)(dataRow["FechaNacimiento"])
                            };
                            listaPacientes.Add(Paciente);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error método en Datos", ex);
                throw new Exception(ex.Message);
            }

            return listaPacientes;
        }

        public List<Paciente> ObtenerFiltrados(string campoBusqueda)
        {
            List<Paciente> listaPacientes = new();
            try
            {
                using SqlCommand command = new(Constantes.SP_PACIENTES_OBTENER_FILTRADOS, (SqlConnection)_context.Database.GetDbConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CampoBusqueda", campoBusqueda);

                using SqlDataAdapter da = new(command);
                DataTable dt = new();
                da.Fill(dt);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        foreach (DataRow row in dt.Rows)
                        {
                            Paciente Paciente = new()
                            {
                                Id = int.Parse(row["Id"].ToString()),
                                Nombre = row["Nombre"].ToString(),
                                Apellido = row["Apellido"].ToString(),
                                ObraSocial = row["ObraSocial"].ToString(),
                                NumeroObraSocial = long.Parse(row["NumeroObraSocial"].ToString()),
                                FechaNacimiento = (DateTime)(row["FechaNacimiento"])
                            };
                        listaPacientes.Add(Paciente);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error método en Datos", ex);
                throw new Exception(ex.Message);
            }

            return listaPacientes;
        }

        public Paciente Obtener(int id)
        {
            Paciente Paciente = new();
            try
            {
                using SqlCommand command = new(Constantes.SP_PACIENTE_OBTENER_POR_ID, (SqlConnection)_context.Database.GetDbConnection());
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

                        Paciente = new Paciente
                        {
                            Id = int.Parse(dataRow["Id"].ToString()),
                            Nombre = dataRow["Nombre"].ToString(),
                            Apellido = dataRow["Apellido"].ToString(),
                            IdObraSocial = int.Parse(dataRow["IdObraSocial"].ToString()),
                            NumeroObraSocial = long.Parse(dataRow["NumeroObraSocial"].ToString()),
                            FechaNacimiento = (DateTime)(dataRow["FechaNacimiento"])
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error método en Datos", ex);
                throw new Exception(ex.Message);
            }

            return Paciente;
        }

        public void Agregar(Paciente paciente)
        {
            try
            {
                var connection = (SqlConnection)_context.Database.GetDbConnection();
                using SqlCommand command = new(Constantes.SP_PACIENTE_AGREGAR, connection);

                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Nombre", paciente.Nombre);
                command.Parameters.AddWithValue("@Apellido", paciente.Apellido);
                command.Parameters.AddWithValue("@IdObraSocial", paciente.IdObraSocial);
                command.Parameters.AddWithValue("@NumeroObraSocial", paciente.NumeroObraSocial);
                command.Parameters.AddWithValue("@FechaNacimiento", paciente.FechaNacimiento);


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

        public void Actualizar(Paciente paciente)
        {
            try
            {
                var connection = (SqlConnection)_context.Database.GetDbConnection();
                using SqlCommand command = new(Constantes.SP_PACIENTE_ACTUALIZAR, connection);

                connection.Open();
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", paciente.Id);
                command.Parameters.AddWithValue("@Apellido", paciente.Apellido);
                command.Parameters.AddWithValue("@Nombre", paciente.Nombre);
                command.Parameters.AddWithValue("@IdObraSocial", paciente.IdObraSocial);
                command.Parameters.AddWithValue("@NumeroObraSocial", paciente.NumeroObraSocial);
                command.Parameters.AddWithValue("@FechaNacimiento", paciente.FechaNacimiento);
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

        public List<ObraSocial> ObtenerObrasSociales()
        {
            List<ObraSocial> listaObrasSociales = new();
            try
            {
                using SqlCommand command = new(Constantes.SP_OBRAS_SOCIALES_OBTENER, (SqlConnection)_context.Database.GetDbConnection());
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
                            ObraSocial oSocial = new()
                            {
                                Id = int.Parse(dataRow["Id"].ToString()),
                                Nombre = dataRow["Nombre"].ToString(),
                                Estado = (bool)dataRow["Estado"],
                            };
                            listaObrasSociales.Add(oSocial);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error método en Datos", ex);
                throw new Exception(ex.Message);
            }

            return listaObrasSociales;
        }

    }
}
