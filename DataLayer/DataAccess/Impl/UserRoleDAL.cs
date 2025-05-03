using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataAccess
{
    public class UserRoleDAL : IUserRoleDAL
    {
        public bool CreateUserRole(UserRole userRole)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    dbContext.UserRoles.Add(userRole);
                    return dbContext.SaveChanges() > 0;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Database error occurred while adding user role.", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while adding user role.", ex);
                }
            }
        }

        public bool UpdateUserRole(UserRole userRole)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var existingUserRole = dbContext.UserRoles.Find(userRole.Id);
                    if (existingUserRole == null)
                        return false;

                    existingUserRole.Name = userRole.Name;
                    existingUserRole.Description = userRole.Description;
                    existingUserRole.UpdatedDate = DateTime.Now;

                    return dbContext.SaveChanges() > 0;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Database error occurred while updating user role.", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while updating user role.", ex);
                }
            }
        }

        public List<UserRole> GetAllUserRoles(string kw, bool isIncludeInActive)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    IQueryable<UserRole> query = dbContext.UserRoles;

                    // Filter by keyword if provided
                    if (!string.IsNullOrWhiteSpace(kw))
                    {
                        kw = kw.ToLower();
                        query = query.Where(r => r.Name.ToLower().Contains(kw) ||
                                             (r.Description != null && r.Description.ToLower().Contains(kw)));
                    }

                    //// Filter by active status if needed
                    //if (!isIncludeInActive)
                    //{
                    //     //query = query.Where(r => r.IsActive == true);
                    //}

                    return query.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving user roles.", ex);
                }
            }
        }

        public UserRole GetUserRoleById(int id, bool isIncludeInActive)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    IQueryable<UserRole> query = dbContext.UserRoles;

                    //if (!isIncludeInActive)
                    //{
                    //    // query = query.Where(r => r.IsActive == true);
                    //}

                    return query.FirstOrDefault(r => r.Id == id);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving user role with ID {id}.", ex);
                }
            }
        }
    }
}
