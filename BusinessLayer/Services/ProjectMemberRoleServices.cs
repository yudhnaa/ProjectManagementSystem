using DataLayer.Domain;
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
    public class ProjectMemberRoleServices
    {
        public List<ProjectMemberRoleDTO> GetAllProjectMemberRoles()
        {
            try
            {
                ProjectMemberRoleDAL projectMemberRoleDAL = new ProjectMemberRoleDAL();
                var projectMemberRoles = projectMemberRoleDAL.GetAllProjectMemberRoles();

                return projectMemberRoles.Select(p => p.ToDto()).ToList();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while retrieving project member roles.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while retrieving project member roles.", ex);
            }
        }

        public ProjectMemberRoleDTO GetRoleById(int id)
        {
            try
            {
                ProjectMemberRoleDAL projectMemberRoleDAL = new ProjectMemberRoleDAL();
                var projectMemberRole = projectMemberRoleDAL.GetRoleById(id);
                
                if (projectMemberRole == null)
                {
                    throw new Exception($"Project member role with ID {id} not found.");
                }

                return projectMemberRole.ToDto();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while retrieving project member role.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while retrieving project member role.", ex);
            }
        }
    }
}
