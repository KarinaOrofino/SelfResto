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
    public class DatosVAplicaciones : DatosBase, IDatosVAplicaciones
    {

        public DatosVAplicaciones(KOContext context) : base(context)
        {
        }

        public List<Aplicacion> ObtenerTodas()
        {
            List<Aplicacion> listaAplicaciones = new();
            try
            {
                using SqlCommand command = new(Constantes.SP_APLICACIONES_OBTENER_TODAS, (SqlConnection)_context.Database.GetDbConnection());
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
                            Aplicacion aplicacion = new()
                            {
                                Id = int.Parse(dataRow["IdAplicacion"].ToString()),
                                //IdDetalle = int.Parse(dataRow["IdDetalle"].ToString()),
                                Fecha = (DateTime)dataRow["Fecha"],
                                IdPaciente = int.Parse(dataRow["IdPaciente"].ToString()),
                                NombrePaciente = dataRow["NombrePaciente"].ToString(),
                                IdMedico = int.Parse(dataRow["IdMedico"].ToString()),
                                NombreMedico = dataRow["NombreMedico"].ToString(),
                                //IdVacuna = int.Parse(dataRow["IdVacuna"].ToString()),
                                //NombreVacuna = dataRow["NombreVacuna"].ToString()
                            };
                            listaAplicaciones.Add(aplicacion);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error método en Datos", ex);
                throw new Exception(ex.Message);
            }

            return listaAplicaciones;
        }

        public List<Aplicacion> ObtenerFiltradas(string campoBusqueda)
        {
            List<Aplicacion> listaAplicaciones = new();
            try
            {
                using SqlCommand command = new(Constantes.SP_APLICACIONES_OBTENER_FILTRADAS, (SqlConnection)_context.Database.GetDbConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("CampoBusqueda", campoBusqueda);

                using SqlDataAdapter da = new(command);
                DataTable dt = new();
                da.Fill(dt);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        foreach (DataRow dataRow in dt.Rows)
                        {
                            Aplicacion aplicacion = new()
                            {
                                Fecha = (DateTime)dataRow["Fecha"],
                                NombrePaciente = dataRow["NombrePaciente"].ToString(),
                                NombreMedico = dataRow["NombreMedico"].ToString(),
                                NombreVacuna = dataRow["Nombre"].ToString(),
                                MarcaVacuna = dataRow["Marca"].ToString()
                            };
                            listaAplicaciones.Add(aplicacion);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error método en Datos", ex);
                throw new Exception(ex.Message);
            }

            return listaAplicaciones;
        }

        public Aplicacion Obtener(int id)
        {
            Aplicacion aplicacion = new();
            try
            {
                using SqlCommand command = new(Constantes.SP_APLICACION_OBTENER_POR_ID, (SqlConnection)_context.Database.GetDbConnection());
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

                        aplicacion = new Aplicacion
                        {
                            Id = int.Parse(dataRow["IdAplicacion"].ToString()),
                            IdDetalle = int.Parse(dataRow["IdDetalle"].ToString()),
                            Fecha = (DateTime)dataRow["Fecha"],
                            IdPaciente = int.Parse(dataRow["IdPaciente"].ToString()),
                            NombrePaciente = dataRow["NombrePaciente"].ToString(),
                            IdMedico = int.Parse(dataRow["IdMedico"].ToString()),
                            NombreMedico = dataRow["NombreMedico"].ToString(),
                            IdVacuna = int.Parse(dataRow["IdVacuna"].ToString()),
                            NombreVacuna = dataRow["NombreVacuna"].ToString()
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error método en Datos", ex);
                throw new Exception(ex.Message);
            }

            return aplicacion;
        }

        public int Agregar(Aplicacion aplicacion)
        {
            try
            {
                var connection = (SqlConnection)_context.Database.GetDbConnection();
                using SqlCommand command = new(Constantes.SP_APLICACION_AGREGAR, connection);

                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Fecha", aplicacion.Fecha);
                command.Parameters.AddWithValue("@IdPaciente", aplicacion.IdPaciente);
                command.Parameters.AddWithValue("@IdMedico", aplicacion.IdMedico);
                command.Parameters.Add("IdAplicacion", SqlDbType.Int).Direction = ParameterDirection.Output;

                command.ExecuteNonQuery();

                int result = Convert.ToInt32(command.Parameters["IdAplicacion"].Value);

                connection.Close();
                return result;
            }
            catch (Exception ex)
            {
                log.Error("Error método en Datos", ex);
                throw new Exception(ex.Message);
            }


        }

        public void AgregarAplicacionDetalle(int idVacuna, int idAplicacion)
        {
            try
            {
                var connection = (SqlConnection)_context.Database.GetDbConnection();
                using SqlCommand command = new(Constantes.SP_APLICACION_DETALLE_AGREGAR, connection);
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();
                command.Parameters.AddWithValue("@idVacuna", idVacuna);
                command.Parameters.AddWithValue("@idAplicacion", idAplicacion);

                command.ExecuteNonQuery();

                connection.Close();

            }
            catch (Exception ex)
            {
                log.Error("Error método en Datos", ex);
                throw new Exception(ex.Message);
            }
        }

        public void Eliminar(int idAplicacion)
        {
            try
            {
                var connection = (SqlConnection)_context.Database.GetDbConnection();
                using SqlCommand command = new(Constantes.SP_APLICACION_ELIMINAR, connection);

                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdAplicacion", idAplicacion);

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

        public List<AplicacionDetalle> ObtenerVacunasPorAplicacion(int idAplicacion)
        {
            List<AplicacionDetalle> listaAplicacionesDetalle = new();
            try
            {
                using SqlCommand command = new(Constantes.SP_APLICACION_OBTENER_VACUNAS_POR_APLICACION, (SqlConnection)_context.Database.GetDbConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@IdAplicacion", idAplicacion);

                using SqlDataAdapter da = new(command);
                DataTable dt = new();
                da.Fill(dt);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        foreach (DataRow dataRow in dt.Rows)
                        {
                            AplicacionDetalle aplicacionDetalle = new()
                            {
                                IdDetalle = int.Parse(dataRow["IdDetalle"].ToString()),
                                IdAplicacion = int.Parse(dataRow["IdAplicacion"].ToString()),
                                Nombre = dataRow["Nombre"].ToString(),
                                Marca = dataRow["Marca"].ToString()
                            };
                            listaAplicacionesDetalle.Add(aplicacionDetalle);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error método en Datos", ex);
                throw new Exception(ex.Message);
            }

            return listaAplicacionesDetalle;
        }
    }
}
