using DataLayer.Domain;
using DataLayer.EnumObjects;
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

        public ProjectMember GetProjectMemberByNotification(int userId, NotificationTypeEnum type, string kw)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var query = dbContext.ProjectMembers
                        .Join(dbContext.Notifications,
                            pm => pm.UserId,
                            noti => noti.UserId,
                            (pm, noti) => new { pm, noti })
                        .Join(dbContext.Projects,
                            temp => temp.pm.ProjectId,
                            p => p.Id,
                            (temp, p) => new { temp.pm, temp.noti, Project = p })
                        .Where(x =>
                            x.pm.UserId == userId &&
                            x.noti.NotificationTypeId == (int)type &&
                            kw.Contains(x.Project.Name))                     
                        .Select(x => x.pm)
                        .FirstOrDefault();

                    return query; 
                }
                catch (Exception ex) when (ex is SqlException || ex is Exception)
                {
                    throw new Exception("An error occurred while retrieving project member by ID.", ex);
                }
            }
        }

    }
}
