using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Domain;


namespace BusinessLayer
{
    public class ProjectDAL
    {

        public List<Project> GetProjectsByUserId(int userId)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    // Fetch projects from the database
                    List<Project> projects = dbContext.Projects.
                        Where(p => dbContext.ProjectMembers.
                            Any(pm => pm.UserId == userId && pm.ProjectId == p.Id) && p.StatusId != 4 && p.IsDeleted != true).ToList();

                    List<Project> projectDTOs = projects;

                    return projectDTOs;
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

        public Project GetProjectById(int projectId)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    Project project = dbContext.Projects.FirstOrDefault(p => p.Id == projectId && p.IsDeleted != true);

                    return project;
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

        public List<Project> GetAllProjects()
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    List<Project> projects = dbContext.Projects.Where(p => p.StatusId != 4 && p.IsDeleted != true).ToList();
                    return projects;
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

        public List<Project> GetAllProjectsIncludeDeleteCancel()
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    List<Project> projects = dbContext.Projects.ToList();
                    return projects;
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

                    var res = dbContext.SaveChanges();
                    return res > 0;
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

        public bool DeleteProject(int projectId)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    Project project = dbContext.Projects.FirstOrDefault(p => p.Id == projectId);
                    if (project == null)
                        return false;

                    // Soft delete
                    project.IsDeleted = true;
                    project.UpdatedDate = DateTime.Now;
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
                    // Handle other exceptions
                    throw ex;
                }
            }
        }

        // Truy van danh sach Project theo keyword + status
    }
}
