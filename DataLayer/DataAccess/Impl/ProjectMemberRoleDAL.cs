using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataAccess
{
    public class ProjectMemberRoleDAL : IProjectMemberRoleDAL
    {
        public bool CreateProjectMemberRole(ProjectMemberRole projectMemberRole)
        {
            if (projectMemberRole == null)
                throw new ArgumentNullException(nameof(projectMemberRole));

            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    dbContext.ProjectMemberRoles.Add(projectMemberRole);
                    return dbContext.SaveChanges() > 0;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Database error occurred while creating project member role.", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while creating project member role.", ex);
                }
            }
        }

        public List<ProjectMemberRole> GetAllProjectMemberRoles(string kw, bool isIncludeInActive)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var query = dbContext.ProjectMemberRoles.AsQueryable();

                    // Apply keyword filter if specified
                    if (!string.IsNullOrWhiteSpace(kw))
                    {
                        string keyword = kw.Trim().ToLowerInvariant();
                        query = query.Where(r => r.Name.ToLower().Contains(keyword) ||
                                               (r.Description != null && r.Description.ToLower().Contains(keyword)));
                    }

                    //// Apply active status filter if specified
                    //if (!isIncludeInActive)
                    //{
                    //    // Assuming ProjectMemberRole has an IsActive property
                    //    query = query.Where(r => r.IsActive == true);
                    //}

                    List<ProjectMemberRole> projectMemberRoles = query.ToList();

                    return projectMemberRoles;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Database error occurred while retrieving project member roles.", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while retrieving project member roles.", ex);
                }
            }
        }

        public ProjectMemberRole GetRoleById(int id, bool isIncludeInActive)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than zero.", nameof(id));

            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var query = dbContext.ProjectMemberRoles.AsQueryable();

                    //// Apply active status filter if specified
                    //if (!isIncludeInActive)
                    //{
                    //    // Assuming ProjectMemberRole has an IsActive property
                    //    query = query.Where(r => r.IsActive == true);
                    //}

                    ProjectMemberRole projectMemberRole = query.FirstOrDefault(r => r.Id == id);

                    return projectMemberRole; // May return null if not found
                }
                catch (SqlException ex)
                {
                    throw new Exception($"Database error occurred while retrieving project member role with ID {id}.", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error occurred while retrieving project member role with ID {id}.", ex);
                }
            }
        }

        public bool UpdateProjectMemberRole(ProjectMemberRole projectMemberRole)
        {
            if (projectMemberRole == null)
                throw new ArgumentNullException(nameof(projectMemberRole));

            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var existingRole = dbContext.ProjectMemberRoles.Find(projectMemberRole.Id);
                    if (existingRole != null)
                    {
                        existingRole.Name = projectMemberRole.Name;
                        existingRole.Description = projectMemberRole.Description;
                        existingRole.UpdatedDate = DateTime.Now;
                        return dbContext.SaveChanges() > 0;
                    }

                    return false;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Database error occurred while updating project member role.", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while updating project member role.", ex);
                }
            }
        }
    }
}
