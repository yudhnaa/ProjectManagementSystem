using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ProjectMemberRoleDAL
    {
        public List<ProjectMemberRole> GetAllProjectMemberRoles()
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    List<ProjectMemberRole> projectMemberRoles = dbContext.ProjectMemberRoles.ToList();

                    if (projectMemberRoles == null || projectMemberRoles.Count == 0)
                    {
                        throw new Exception("No project member roles found.");
                    }

                    return projectMemberRoles;
                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                    throw new Exception("Database error occurred while retrieving project member roles.", ex);
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., log the error, rethrow, etc.)
                    throw new Exception("An error occurred while retrieving project member roles.", ex);
                }

            }
        }

        public ProjectMemberRole GetRoleById(int id)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    ProjectMemberRole projectMemberRole = dbContext.ProjectMemberRoles.Find(id);
                    if (projectMemberRole == null)
                    {
                        throw new Exception($"Project member role with ID {id} not found.");
                    }
                    return projectMemberRole;
                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                    throw new Exception("Database error occurred while retrieving project member role.", ex);
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., log the error, rethrow, etc.)
                    throw new Exception("An error occurred while retrieving project member role.", ex);
                }
            }
        }
    }
}
