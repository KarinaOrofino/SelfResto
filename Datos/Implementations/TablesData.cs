using Framework.Common;
using KO.Data.EFScafolding;
using KO.Data.Interfaces;
using KO.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;

namespace KO.Data.Implementations
{
    public class TablesData : BaseData, ITablesData
    {

        public TablesData(KOContext context) : base(context)
        {
        }

        public List<Table> GetAllFiltered(string searchField, bool? active)
        {
            List<Table> tablesList = new();
            try
            {
                using SqlCommand command = new(Constants.SP_TABLES_GET_ALL_FILTERED, (SqlConnection)_context.Database.GetDbConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("SearchField", searchField);
                command.Parameters.AddWithValue("Active", active);

                using SqlDataAdapter da = new(command);
                DataTable dt = new();
                da.Fill(dt);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        foreach (DataRow dataRow in dt.Rows)
                        {
                            Table table = new()
                            {
                                Id = int.Parse(dataRow["Id"].ToString()),
                                Number = int.Parse(dataRow["Number"].ToString()),
                                Name = dataRow["Name"].ToString(),
                                Description = dataRow["Description"].ToString(),
                                Active = (bool)dataRow["Active"],
                                CreationDate = DateTime.Parse(dataRow["Creation_Date"].ToString()),
                                CreationUser = int.Parse(dataRow["Creation_User"].ToString()),
                            };
                            tablesList.Add(table);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error in Data Get All Filtered", ex);
                throw new Exception(ex.Message);
            }

            return tablesList;
        }

    }
}
