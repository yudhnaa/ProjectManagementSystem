using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UserRoleDAL
    {
        public List<UserRole> getAllUserRoles()
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var userRoles = dbContext.UserRoles.ToList();
                    return userRoles;
                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                    throw ex;
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    throw ex;
                }
            }
        }

        public UserRole GetUserRoleById(int id)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var userRole = dbContext.UserRoles.Single(r => r.Id == id);

                    return userRole;
                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                    throw ex;
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    throw ex;
                }
            }
        }
    }
}
