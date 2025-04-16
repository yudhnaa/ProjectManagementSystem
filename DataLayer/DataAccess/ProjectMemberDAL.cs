using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ProjectMemberDAL
    {

        public List<ProjectMember> GetProjectMembersById(int projectId)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var members = dbContext.ProjectMembers
                        .Where(pm => pm.ProjectId == projectId)
                        .ToList();

                    return members;
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

        public bool AddMemberToProject(ProjectMember projectMember)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    // Check if the user is already a member of the project
                    bool isAlreadyMember = dbContext.ProjectMembers.Any(pm => pm.ProjectId == projectMember.ProjectId && pm.UserId == projectMember.UserId);
                    if (isAlreadyMember)
                        return false;

                    dbContext.ProjectMembers.Add(projectMember);
                    dbContext.SaveChanges();
                    return true;
                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool RemoveMemberFromProject(int projectId, int userId)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    ProjectMember projectMember = dbContext.ProjectMembers.FirstOrDefault(pm => pm.ProjectId == projectId && pm.UserId == userId);
                    if (projectMember == null)
                        return false;

                    dbContext.ProjectMembers.Remove(projectMember);
                    dbContext.SaveChanges();
                    return true;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool UpdateProjectMember(ProjectMember projectMember)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var existingMember = dbContext.ProjectMembers.FirstOrDefault(pm => pm.Id == projectMember.Id);
                    if (existingMember != null)
                    {
                        existingMember.RoleInProject = projectMember.RoleInProject;
                        existingMember.IsConfirmed = projectMember.IsConfirmed;
                        existingMember.ConfirmationDate = projectMember.ConfirmationDate;
                        existingMember.IsActive = projectMember.IsActive;
                        existingMember.UpdatedDate = DateTime.Now;
                        dbContext.SaveChanges();

                        return true;
                    }
                    else
                        return false;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
