using DataLayer.DataAccess;
using DataLayer.Domain;
using DTOLayer;
using DTOLayer.Mappers;
using DTOLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UserRoleServices : IUserRoleServices
    {
        public bool CreateUserRole(UserRoleDTO userRoleDTO)
        {
            try
            {
                UserRoleDAL userRoleDAL = new UserRoleDAL();

                var userRole = userRoleDTO.ToUserRoleEntity();
                //userRole.IsDeleted = false;
                //userRole.IsActive = true;
                userRole.CreatedDate = DateTime.Now;
                userRole.UpdatedDate = null;

                var res = userRoleDAL.CreateUserRole(userRole);

                return res;
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while adding user role.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while adding user role.", ex);
            }
        }

        public List<UserRoleDTO> GetAllUserRoles(string kw)
        {
            try
            {
                UserRoleDAL userRoleDAL = new UserRoleDAL();

                var userRoles = userRoleDAL.GetAllUserRoles(kw, isIncludeInActive: false);

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

        public List<UserRoleDTO> GetAllUserRolesInlcudeInActive(string kw)
        {
            try
            {
                UserRoleDAL userRoleDAL = new UserRoleDAL();

                var userRoles = userRoleDAL.GetAllUserRoles(kw, isIncludeInActive: true);

                if (userRoles == null)
                    return null;

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

        public UserRoleDTO GetUserRoleById(int id)
        {
            try
            {
                UserRoleDAL userRoleDAL = new UserRoleDAL();

                var userRole = userRoleDAL.GetUserRoleById(id, isIncludeInActive: false);

                if (userRole == null)
                    throw new Exception("User role not found.");

                return userRole.ToDto();
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

        public UserRoleDTO GetUserRoleByIdInlcudeInActive(int id)
        {
            try
            {
                UserRoleDAL userRoleDAL = new UserRoleDAL();

                var userRole = userRoleDAL.GetUserRoleById(id, isIncludeInActive: false);

                if (userRole == null)
                    throw new Exception("User role not found.");

                return userRole.ToDto();
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

        public bool UpdateUserRole(UserRoleDTO item)
        {
            try
            {
                UserRoleDAL userRoleDAL = new UserRoleDAL();

                var userRole = item.ToUserRoleEntity();
                userRole.UpdatedDate = DateTime.Now;

                var res = userRoleDAL.UpdateUserRole(userRole);
                return res;
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while updating user role.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while updating user role.", ex);
            }
        }
    }
}