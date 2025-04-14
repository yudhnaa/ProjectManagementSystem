using DataLayer.Domain;
using DTOLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UserRoleServices
    {
        public List<UserRoleDTO> getAllUserRoles()
        {
            try
            {
                UserRoleDAL userRoleDAL = new UserRoleDAL();

                var userRoles = userRoleDAL.getAllUserRoles();

                return userRoles.Select(ur => ur.ToDto()).ToList();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while retrieving user roles.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while retrieving user roles.", ex);
            }
        }
    }
}
