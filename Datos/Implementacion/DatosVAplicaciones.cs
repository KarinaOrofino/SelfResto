using KO.Datos.EFScafolding;
using KO.Datos.Interfaces;
using KO.Entidades;
using Framework.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;

namespace KO.Datos.Implementacion
{
    public class DatosVAplicaciones : DatosBase, IDatosVAplicaciones
    {
        //private IConfiguration Configuration;


        public DatosVAplicaciones(IConfiguration configuration, KOContext context) : base(context)
        {
            //this.Configuration = configuration;
        }

        //public List<Medico> ObtenerFiltrados(int? Matricula, string Nombre, string Apellido, bool? Estado)
        //{
        //    List<Medico> listaMedicos = new List<Medico>();
        //    try
        //    {
        //        using (SqlCommand command = new SqlCommand(Constantes.SP_OBTENER_MEDICOS_FILTRADOS, (SqlConnection)_context.Database.GetDbConnection()))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("Matricula", Matricula);
        //            command.Parameters.AddWithValue("Nombre", Nombre);
        //            command.Parameters.AddWithValue("Apellido", Apellido);
        //            command.Parameters.AddWithValue("Estado", Estado);

        //            using (SqlDataAdapter da = new SqlDataAdapter(command))
        //            {
        //                DataTable dt = new DataTable();
        //                da.Fill(dt);

        //                if (dt != null)
        //                {
        //                    if (dt.Rows.Count > 0)
        //                    {

        //                        foreach (DataRow row in dt.Rows)
        //                        {
        //                            Medico medico = new Medico();
        //                            medico.Matricula = int.Parse(row["Matricula"].ToString());
        //                            medico.Nombre = row["Nombre"].ToString();
        //                            medico.Apellido = row["Apellido"].ToString();
        //                            medico.Estado = (bool)row["Estado"];
        //                            listaMedicos.Add(medico);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error("Error método en Datos", ex);
        //        throw new Exception(ex.Message);
        //    }

        //    return listaMedicos;
        //}


    }
}
