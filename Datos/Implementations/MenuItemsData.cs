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

        public List<MenuItem> GetAll()
        {
            List<MenuItem> menuItemsList = new();
            try
            {
                using SqlCommand command = new(Constants.SP_MENUITEMS_GET_ALL, (SqlConnection)_context.Database.GetDbConnection());
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
                            MenuItem menuItem = new()
                            {
                                Id = int.Parse(dataRow["Id"].ToString()),
                                Name = dataRow["Name"].ToString(),
                                Price = double.Parse(dataRow["Price"].ToString()),
                                CategoryId = int.Parse(dataRow["Id"].ToString()),
                                Active = (bool)dataRow["Active"],
                                CreationDate = DateTime.Parse(dataRow["Creation_Date"].ToString()),
                                CreationUser = int.Parse(dataRow["Creation_User"].ToString()),
                                UpdateDate = string.IsNullOrEmpty(dataRow["Update_Date"].ToString()) ? null : DateTime.Parse(dataRow["Update_Date"].ToString()),
                                UpdateUser = int.Parse(dataRow["Update_User"].ToString()),
                            };
                            menuItemsList.Add(menuItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error in Data Get All Method", ex);
                throw new Exception(ex.Message);
            }

            return menuItemsList;
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
                                Name = dataRow["Name"].ToString(),
                                Price = double.Parse(dataRow["Price"].ToString()),
                                CategoryId = int.Parse(dataRow["Id"].ToString()),
                                Active = (bool)dataRow["Active"],
                                CreationDate = DateTime.Parse(dataRow["Creation_Date"].ToString()),
                                CreationUser = int.Parse(dataRow["Creation_User"].ToString()),
                                UpdateDate = string.IsNullOrEmpty(dataRow["Update_Date"].ToString()) ? null : DateTime.Parse(dataRow["Update_Date"].ToString()),
                                UpdateUser = int.Parse(dataRow["Update_User"].ToString()),
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
     
        public MenuItem GetById(int id)
        {
            MenuItem menuItem = new();
            try
            {
                using SqlCommand command = new(Constants.SP_MENUITEMS_GET_BY_ID, (SqlConnection)_context.Database.GetDbConnection());
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

                        menuItem = new MenuItem
                        {
                            Id = int.Parse(dataRow["Id"].ToString()),
                            Name = dataRow["Name"].ToString(),
                            Price = double.Parse(dataRow["Price"].ToString()),
                            CategoryId = int.Parse(dataRow["Id"].ToString()),
                            Active = (bool)dataRow["Active"],
                            CreationDate = DateTime.Parse(dataRow["Creation_Date"].ToString()),
                            CreationUser = int.Parse(dataRow["Creation_User"].ToString()),
                            UpdateDate = string.IsNullOrEmpty(dataRow["Update_Date"].ToString()) ? null : DateTime.Parse(dataRow["Update_Date"].ToString()),
                            UpdateUser = int.Parse(dataRow["Update_User"].ToString()),
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error in Data, Get By Id", ex);
                throw new Exception(ex.Message);
            }

            return menuItem;
        }

        public void Create(MenuItem menuItem)
        {
            try
            {
                var connection = (SqlConnection)_context.Database.GetDbConnection();
                using SqlCommand command = new(Constants.SP_MENUITEMS_CREATE, connection);

                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", menuItem.Name);
                command.Parameters.AddWithValue("@CategoryId", menuItem.CategoryId);
                command.Parameters.AddWithValue("@Price", menuItem.Price);
                command.Parameters.AddWithValue("@CreationUser", menuItem.CreationUser);

                command.ExecuteNonQuery();

                connection.Dispose();
                connection.Close();

            }
            catch (Exception ex)
            {
                log.Error("Error in Data, Create", ex);
                throw new Exception(ex.Message);
            }
        }

        public void Update(MenuItem menuItem)
        {
            try
            {
                var connection = (SqlConnection)_context.Database.GetDbConnection();
                using SqlCommand command = new(Constants.SP_MENUITEMS_UPDATE, connection);

                connection.Open();
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", menuItem.Id);
                command.Parameters.AddWithValue("@Name", menuItem.Name);
                command.Parameters.AddWithValue("@Active", menuItem.Active);
                command.Parameters.AddWithValue("@CategoryId", menuItem.CategoryId);
                command.Parameters.AddWithValue("@Price", menuItem.Price);
                command.Parameters.AddWithValue("@UpdateUser", menuItem.CreationUser);
                command.ExecuteNonQuery();

                connection.Dispose();
                connection.Close();

            }
            catch (Exception ex)
            {
                log.Error("Error in Data, Update", ex);
                throw new Exception(ex.Message);
            }
        }

        public void Inactivate(int id)
        {
            try
            {
                var connection = (SqlConnection)_context.Database.GetDbConnection();
                using SqlCommand command = new(Constants.SP_MENUITEMS_INACTIVATE, connection);

                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();

                connection.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                log.Error("Error in Data, Inactivate", ex);
                throw new Exception(ex.Message);
            }

        }

        public void Activate(int id)
        {
            try
            {
                var connection = (SqlConnection)_context.Database.GetDbConnection();
                using SqlCommand command = new(Constants.SP_MENUITEMS_ACTIVATE, connection);

                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();

                connection.Dispose();
                connection.Close();

            }
            catch (Exception ex)
            {
                log.Error("Error in Data, Activate", ex);
                throw new Exception(ex.Message);
            }
        }
    }
}
