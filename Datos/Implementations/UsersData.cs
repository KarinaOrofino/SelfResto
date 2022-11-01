using Framework.Common;
using KO.Data.EFScafolding;
using KO.Data.Interfaces;
using KO.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

namespace KO.Data.Implementations
{
    public class UsersData : BaseData, IUsersData
    {

        public UsersData(IConfiguration configuration, KOContext context) : base(context)
        {
        }

        public List<User> GetAllFiltered(string searchField, bool? active)
        {
            List<User> usersList = new();
            try
            {
                using SqlCommand command = new(Constants.SP_USERS_GET_ALL_FILTERED, (SqlConnection)_context.Database.GetDbConnection());
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
                            User user = new()
                            {
                                Id = int.Parse(dataRow["Id"].ToString()),
                                Name = dataRow["Name"].ToString(),
                                Surname = dataRow["Surname"].ToString(),
                                Email = dataRow["Email"].ToString(),
                                Password = dataRow["Password"].ToString(),
                                Access_Type = int.Parse(dataRow["Access_Type"].ToString()),
                                AccessTypeName = dataRow["AccessTypeName"].ToString(),
                                Active = (bool)dataRow["Active"],
                                CreationDate = DateTime.Parse(dataRow["Creation_Date"].ToString()),
                                CreationUser = int.Parse(dataRow["Creation_User"].ToString()),
                                //UpdateDate = string.IsNullOrEmpty(dataRow["Update_Date"].ToString()) ? null : DateTime.Parse(dataRow["Update_Date"].ToString()),
                                //UpdateUser = int.Parse(dataRow["Update_User"].ToString()),
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
                        user.Access_Type = int.Parse(dataRow["Access_Type"].ToString());
                        user.AccessTypeName = dataRow["AccessTypeName"].ToString();
                        user.Active = (bool)dataRow["Active"];
                        user.CreationDate = DateTime.Parse(dataRow["Creation_Date"].ToString());
                        user.CreationUser = int.Parse(dataRow["Creation_User"].ToString());
                        user.UpdateDate = string.IsNullOrEmpty(dataRow["Update_Date"].ToString()) ? null : DateTime.Parse(dataRow["Update_Date"].ToString());
                        user.UpdateUser = string.IsNullOrEmpty(dataRow["Update_User"].ToString()) ? null : int.Parse(dataRow["Update_User"].ToString());
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

    }
}
