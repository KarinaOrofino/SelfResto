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
    public class UsersData : BaseData, IUsersData
    {

        public UsersData(IConfiguration configuration, KOContext context) : base(context)
        {
        }

        public List<User> GetAll()
        {
            List<User> usersList = new();
            try
            {
                using SqlCommand command = new(Constants.SP_USERS_GET_ALL, (SqlConnection)_context.Database.GetDbConnection());
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
                            User user = new()
                            {
                                Id = int.Parse(dataRow["Id"].ToString()),
                                Name = dataRow["Name"].ToString(),
                                Surname = dataRow["Surname"].ToString(),
                                Email = dataRow["Email"].ToString(),
                                Password = dataRow["Password"].ToString(),
                                AccessType = int.Parse(dataRow["Access_Type"].ToString()),
                                Active = (bool)dataRow["Active"],
                                CreationDate = DateTime.Parse(dataRow["Creation_Date"].ToString()),
                                CreationUser = int.Parse(dataRow["Creation_User"].ToString()),
                                UpdateDate = string.IsNullOrEmpty(dataRow["Update_Date"].ToString()) ? null : DateTime.Parse(dataRow["Update_Date"].ToString()),
                                UpdateUser = int.Parse(dataRow["Update_User"].ToString()),
                            };
                            usersList.Add(user);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error in Data Get All Method", ex);
                throw new Exception(ex.Message);
            }

            return usersList;
        }

        public List<User> GetAllFiltered(string searchField, bool? active)
        {
            List<User> usersList = new ();
            try
            {
                using SqlCommand command = new(Constants.SP_USERS_GET_ALL_FILTERED, (SqlConnection)_context.Database.GetDbConnection());
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
                            User user = new ()
                            {
                                Id = int.Parse(dataRow["Id"].ToString()),
                                Name = dataRow["Name"].ToString(),
                                Surname = dataRow["Surname"].ToString(),
                                Email = dataRow["Email"].ToString(),
                                Password = dataRow["Password"].ToString(),
                                AccessType = int.Parse(dataRow["Access_Type"].ToString()),
                                Active = (bool)dataRow["Active"],
                                CreationDate = DateTime.Parse(dataRow["Creation_Date"].ToString()),
                                CreationUser = int.Parse(dataRow["Creation_User"].ToString()),
                                UpdateDate = string.IsNullOrEmpty(dataRow["Update_Date"].ToString()) ? null : DateTime.Parse(dataRow["Update_Date"].ToString()),
                                UpdateUser = int.Parse(dataRow["Update_User"].ToString()),
                            };
                            usersList.Add(user);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error in Data Get All Filtered", ex);
                throw new Exception(ex.Message);
            }

            return usersList;
        }
     
        public User GetById(int id)
        {
            User user = new();
            try
            {
                using SqlCommand command = new(Constants.SP_USERS_GET_BY_ID, (SqlConnection)_context.Database.GetDbConnection());
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

                        user.Id = int.Parse(dataRow["Id"].ToString());
                        user.Name = dataRow["Name"].ToString();
                        user.Email = dataRow["Email"].ToString();
                        user.Surname = dataRow["Surname"].ToString();
                        user.Password = dataRow["Password"].ToString();
                        user.AccessType = int.Parse(dataRow["Access_Type"].ToString());
                        user.Active = (bool)dataRow["Active"];
                        user.CreationDate = DateTime.Parse(dataRow["Creation_Date"].ToString());
                        user.CreationUser = int.Parse(dataRow["Creation_User"].ToString());
                        user.UpdateDate = string.IsNullOrEmpty(dataRow["Update_Date"].ToString()) ? null : DateTime.Parse(dataRow["Update_Date"].ToString());
                        user.UpdateUser = int.Parse(dataRow["Update_User"].ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error in Data, Get By Id", ex);
                throw new Exception(ex.Message);
            }

            return user;
        }

        public User GetByEmail(string email)
        {
            User user = new();
            try
            {
                using SqlCommand command = new(Constants.SP_USERS_GET_BY_EMAIL, (SqlConnection)_context.Database.GetDbConnection());
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Email", email);

                using SqlDataAdapter da = new(command);
                DataTable dt = new();
                da.Fill(dt);

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dataRow = dt.Rows[0];

                        user.Id = int.Parse(dataRow["Id"].ToString());
                        user.Name = dataRow["Name"].ToString();
                        user.Email = dataRow["Email"].ToString();
                        user.Surname = dataRow["Surname"].ToString();
                        user.Password = dataRow["Password"].ToString();
                        user.AccessType = int.Parse(dataRow["Access_Type"].ToString());
                        user.Active = (bool)dataRow["Active"];
                        user.CreationDate = DateTime.Parse(dataRow["Creation_Date"].ToString());
                        user.CreationUser = int.Parse(dataRow["Creation_User"].ToString());
                        user.UpdateDate = string.IsNullOrEmpty(dataRow["Update_Date"].ToString()) ? null : DateTime.Parse(dataRow["Update_Date"].ToString());
                        user.UpdateUser = string.IsNullOrEmpty(dataRow["Update_User"].ToString())? null : int.Parse(dataRow["Update_User"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error in Data, Get By Email", ex);
                throw new Exception(ex.Message);
            }

            return user;
        }

        public void Create(User user)
        {
            try
            {
                var connection = (SqlConnection)_context.Database.GetDbConnection();
                using SqlCommand command = new(Constants.SP_USERS_CREATE, connection);

                connection.Open();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@AccessType", user.AccessType);
                command.Parameters.AddWithValue("@CreationUser", user.CreationUser);

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

        public void Update(User user)
        {
            try
            {
                var connection = (SqlConnection)_context.Database.GetDbConnection();
                using SqlCommand command = new(Constants.SP_USERS_UPDATE, connection);

                connection.Open();
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@Id", user.Id);
                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);
                command.Parameters.AddWithValue("@AccessType", user.AccessType);
                command.Parameters.AddWithValue("@UpdateUser", user.CreationUser);
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
                using SqlCommand command = new(Constants.SP_USERS_INACTIVATE, connection);

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
                using SqlCommand command = new(Constants.SP_USERS_ACTIVATE, connection);

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
