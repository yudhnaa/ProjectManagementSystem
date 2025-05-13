using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DataLayer.DataAccess
{
    public class ProjectMemberDAL : IProjectMemberDAL
    {
        public List<ProjectMember> GetProjectMembersByProjectId(int projectId, bool isIncludeInActive)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    return dbContext.ProjectMembers
                        .Where(pm => pm.ProjectId == projectId &&
                                     (isIncludeInActive || (pm.IsActive == true && pm.IsDeleted == false)))
                        .ToList();
                }
                catch (Exception ex) when (ex is SqlException || ex is Exception)
                {
                    throw new Exception("An error occurred while retrieving project members.", ex);
                }
            }
        }

        public bool CreateProjectMember(ProjectMember projectMember)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    if (dbContext.ProjectMembers.Any(pm => pm.ProjectId == projectMember.ProjectId && pm.UserId == projectMember.UserId))
                        return false;

                    dbContext.ProjectMembers.Add(projectMember);
                    dbContext.SaveChanges();
                    return true;
                }
                catch (Exception ex) when (ex is SqlException || ex is Exception)
                {
                    throw;
                }
            }
        }

        public bool RemoveMemberFromProject(int projectId, int userId)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var projectMember = dbContext.ProjectMembers.FirstOrDefault(pm => pm.ProjectId == projectId && pm.UserId == userId);
                    if (projectMember == null)
                        return false;

                    dbContext.ProjectMembers.Remove(projectMember);
                    dbContext.SaveChanges();
                    return true;
                }
                catch (Exception ex) when (ex is SqlException || ex is Exception)
                {
                    throw;
                }
            }
        }

        public bool UpdateProjectMember(ProjectMember projectMember)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var existingMember = dbContext.ProjectMembers.FirstOrDefault(pm => pm.Id == projectMember.Id);
                    if (existingMember == null)
                        return false;

                    existingMember.RoleInProject = projectMember.RoleInProject;
                    existingMember.IsConfirmed = projectMember.IsConfirmed;
                    existingMember.ConfirmationDate = projectMember.ConfirmationDate;
                    existingMember.IsActive = projectMember.IsActive;
                    existingMember.UpdatedDate = DateTime.Now;

                    dbContext.SaveChanges();
                    return true;
                }
                catch (Exception ex) when (ex is SqlException || ex is Exception)
                {
                    throw;
                }
            }
        }

        public List<ProjectMember> GetAllProjectMembers(string kw, bool isIncludeInActive)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    return dbContext.ProjectMembers
                        .Where(pm => (pm.User.Username.Contains(kw) || pm.User.LastName.Contains(kw)) && (isIncludeInActive || (pm.IsActive == true && pm.IsDeleted == false)))
                        .ToList();
                }
                catch (Exception ex) when (ex is SqlException || ex is Exception)
                {
                    throw new Exception("An error occurred while retrieving all project members.", ex);
                }
            }
        }
    }
}
