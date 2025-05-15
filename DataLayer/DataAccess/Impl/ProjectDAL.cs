using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Domain;
using DataLayer.EnumObjects;


namespace DataLayer.DataAccess
{
    public class ProjectDAL : IProjectDAL
    {
        public Project CreateProject(Project project)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    dbContext.Projects.Add(project);
                    dbContext.SaveChanges();

                    return project;
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

        public List<Project> GetAllProjects(string kw, bool isIncludeInActive)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var query = dbContext.Projects.AsQueryable();

                    if (!string.IsNullOrEmpty(kw))
                    {
                        query = query.Where(p => p.Name.Contains(kw) || p.ProjectCode.Contains(kw));
                    }

                    if (!isIncludeInActive)
                    {
                        int cancelledId = ProjectStatusEnumExtensions.ToId(ProjectStatusEnum.Cancelled);

                        query = query.Where(p => p.StatusId != cancelledId && p.IsDeleted == false);
                    }

                    return query.ToList();
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

        public Project GetProjectById(int projectId, bool isIncludeInActive)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {

                    var query = dbContext.Projects.Where(p => p.Id == projectId);

                    if (!isIncludeInActive)
                    {
                        int cancelledSatusId = ProjectStatusEnumExtensions.ToId(ProjectStatusEnum.Cancelled);
                        query = query.Where(p => p.StatusId != cancelledSatusId && p.IsDeleted == false);
                    }

                    return query.FirstOrDefault();
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

        public List<Project> GetProjectsByKeywordAndStatus(string keyword, int statusId, bool isIncludeInActive)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var query = dbContext.Projects.Where(p => p.Name.Contains(keyword) && p.StatusId == statusId);

                    if (!isIncludeInActive)
                    {
                        query = query.Where(p => ProjectStatusEnumExtensions.FromId(p.StatusId) != ProjectStatusEnum.Cancelled && p.IsDeleted == false);

                    }

                    return query.ToList();
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

        public List<Project> GetProjectsByUserId(int userId, bool isIncludeInActive)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var query = dbContext.Projects.Where(p => dbContext.ProjectMembers
                        .Any(pm => pm.UserId == userId && pm.ProjectId == p.Id));

                    if (!isIncludeInActive)
                    {
                        int canncelledSatusId = ProjectStatusEnumExtensions.ToId(ProjectStatusEnum.Cancelled);
                        query = query.Where(p => p.StatusId != canncelledSatusId && p.IsDeleted == false);
                    }

                    return query.ToList();
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

        public bool UpdateProject(Project project)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    Project existingProject = dbContext.Projects.FirstOrDefault(p => p.Id == project.Id);
                    if (existingProject == null)
                        return false;

                    existingProject.Name = project.Name;
                    existingProject.ProjectCode = project.ProjectCode;
                    existingProject.Description = project.Description;
                    existingProject.StartDate = project.StartDate;
                    existingProject.EndDate = project.EndDate;
                    existingProject.Budget = project.Budget;
                    existingProject.StatusId = project.StatusId;
                    existingProject.ManagerId = project.ManagerId;
                    existingProject.PriorityId = project.PriorityId;
                    existingProject.PercentComplete = project.PercentComplete;
                    existingProject.UpdatedDate = DateTime.Now;
                    existingProject.IsDeleted = project.IsDeleted;

                    var res = dbContext.SaveChanges();
                    return res > 0;
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

        public List<Project> GetProjectsByUserWhoConfimedInvitation(int userId)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var projects = dbContext.Projects
                         .Join(dbContext.ProjectMembers,
                               p => p.Id,
                               pm => pm.ProjectId,
                               (p, pm) => new { Project = p, ProjectMember = pm })
                         .Where(x => x.ProjectMember.UserId == userId && (x.ProjectMember.IsConfirmed ?? false))
                         .Select(x => x.Project)
                         .ToList();

                    return projects;
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
