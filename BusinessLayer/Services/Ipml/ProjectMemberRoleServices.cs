using DataLayer.DataAccess;
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
    public class ProjectMemberRoleServices : IProjectMemberRoleServices
    {
        public List<ProjectMemberRoleDTO> GetAllProjectMemberRoles(string kw)
        {
            try
            {
                ProjectMemberRoleDAL projectMemberRoleDAL = new ProjectMemberRoleDAL();
                var projectMemberRoles = projectMemberRoleDAL.GetAllProjectMemberRoles(kw, isIncludeInActive: false);
                if (projectMemberRoles == null)
                {
                    throw new Exception("No project member roles found.");
                }

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

        public List<ProjectMemberRoleDTO> GetAllProjectMemberRolesInlcudeInActive(string kw)
        {
            try
            {
                ProjectMemberRoleDAL projectMemberRoleDAL = new ProjectMemberRoleDAL();
                var projectMemberRoles = projectMemberRoleDAL.GetAllProjectMemberRoles(kw, isIncludeInActive: true);
                if (projectMemberRoles == null)
                {
                    throw new Exception("No project member roles found.");
                }

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
                var projectMemberRole = projectMemberRoleDAL.GetRoleById(id, isIncludeInActive: false);

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

        public ProjectMemberRoleDTO GetRoleByIdInlcudeInActive(int id)
        {
            try
            {
                ProjectMemberRoleDAL projectMemberRoleDAL = new ProjectMemberRoleDAL();
                var projectMemberRole = projectMemberRoleDAL.GetRoleById(id, isIncludeInActive: true);

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
