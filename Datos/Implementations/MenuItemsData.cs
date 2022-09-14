using KO.Data.EFScafolding;
using Framework.Common;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System;
using KO.Entities;
using KO.Data.Interfaces;

namespace KO.Data.Implementations
{
    public class MenuItemsData : BaseData, IMenuItemsData
    {

        public MenuItemsData(KOContext context) : base(context)
        {
        }

        public List<Category> GetAllCategories()
        {
            List<Category> categories = new();
            
            try
            {
                using SqlCommand command = new(Constants.SP_CATEGORIES_GET_ALL, (SqlConnection)_context.Database.GetDbConnection());
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
                            Category category = new()
                            {
                                Id = int.Parse(dataRow["Id"].ToString()),
                                Name = dataRow["Name"].ToString(),
                                CategoryImageUrl = dataRow["ImageUrl"].ToString(),
                                Active = (bool)dataRow["Active"]
                            };
                            categories.Add(category);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error in Data Get All Method", ex);
                throw new Exception(ex.Message);
            }

            return categories;
        }

        public List<MenuItem> GetAllFiltered(string searchField, bool? active)
        {
            List<MenuItem> menuItemsList = new ();
            try
            {
                using SqlCommand command = new(Constants.SP_MENUITEMS_GET_ALL_FILTERED, (SqlConnection)_context.Database.GetDbConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("SearchField", searchField);
                command.Parameters.AddWithValue("Active", active);

                using SqlDataAdapter da = new (command);
                DataTable dt = new ();
                da.Fill(dt);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {

                        foreach (DataRow dataRow in dt.Rows)
                        {
                            MenuItem menuItem = new ()
                            {
                                Id = int.Parse(dataRow["Id"].ToString()),
                                CategoryId = int.Parse(dataRow["Category_Id"].ToString()),
                                CategoryName = dataRow["CategoryName"].ToString(),
                                VisualizationOrder = int.Parse(dataRow["VisualizationOrder"].ToString()),
                                Name = dataRow["Name"].ToString(),
                                Description = dataRow["Description"].ToString(),
                                ImageUrl = dataRow["ImageUrl"].ToString(),
                                Price = double.Parse(dataRow["Price"].ToString()),
                                Active = (bool)dataRow["Active"],
                                //CreationDate = DateTime.Parse(dataRow["Creation_Date"].ToString()),
                                //CreationUser = int.Parse(dataRow["Creation_User"].ToString()),
                                //UpdateDate = string.IsNullOrEmpty(dataRow["Update_Date"].ToString()) ? null : DateTime.Parse(dataRow["Update_Date"].ToString()),
                                //UpdateUser = int.Parse(dataRow["Update_User"].ToString()),
                            };
                            menuItemsList.Add(menuItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error in Data Get All Filtered", ex);
                throw new Exception(ex.Message);
            }

            return menuItemsList;
        }


        public List<MenuItem> GetAllFilteredByCatId(int catId, bool? active)
        {
            List<MenuItem> menuItemsList = new();
            try
            {
                using SqlCommand command = new(Constants.SP_MENUITEMS_GET_ALL_FILTERED_BY_CATID, (SqlConnection)_context.Database.GetDbConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("catId", catId);
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
                            MenuItem menuItem = new()
                            {
                                Id = int.Parse(dataRow["Id"].ToString()),
                                VisualizationOrder = int.Parse(dataRow["VisualizationOrder"].ToString()),
                                CategoryId = int.Parse(dataRow["Category_Id"].ToString()),
                                Name = dataRow["Name"].ToString(),
                                Description = dataRow["Description"].ToString(),
                                ImageUrl = dataRow["ImageUrl"].ToString(),
                                Price = double.Parse(dataRow["Price"].ToString()),
                                Active = (bool)dataRow["Active"],
                                //CreationDate = DateTime.Parse(dataRow["Creation_Date"].ToString()),
                                //CreationUser = int.Parse(dataRow["Creation_User"].ToString()),
                                //UpdateDate = string.IsNullOrEmpty(dataRow["Update_Date"].ToString()) ? null : DateTime.Parse(dataRow["Update_Date"].ToString()),
                                //UpdateUser = int.Parse(dataRow["Update_User"].ToString()),
                            };
                            menuItemsList.Add(menuItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error in Data Get All Filtered", ex);
                throw new Exception(ex.Message);
            }

            return menuItemsList;
        }

    }
}
