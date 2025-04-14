using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Domain;
using DTOLayer;
using DTOLayer.Mappers;
using DTOLayer.Models;


namespace BusinessLayer
{
    public class ProjectServices
    {

        public List<ProjectDTO> GetProjectsByUserId(int userId)
        {
            try
            {
                ProjectDAL projectDAL = new ProjectDAL();
                var projects = projectDAL.GetProjectsByUserId(userId);

                return projects.Select(p => p.ToDto()).ToList();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while retrieving projects.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while retrieving projects.", ex);

            }
        }

        public ProjectDTO CreateProject(ProjectDTO projectDTO)
        {
            try
            {
                ProjectDAL projectDAL = new ProjectDAL();

                Project project = projectDTO.ToProjectEntity();

                project.PercentComplete = 0;
                project.IsDeleted = false;

                Project res = projectDAL.CreateProject(project);

                return res.ToDto();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while creating project.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while creating project.", ex);



            }
        }

        //public ProjectDTO GetProjectById(int projectId)
        //{
        //    using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
        //    {
        //        Project project = dbContext.Projects.FirstOrDefault(p => p.Id == projectId);
        //        return project != null ? project.ToDto() : null;
        //    }
        //}

        //public List<ProjectDTO> GetAllProjects()
        //{
        //    using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
        //    {
        //        List<Project> projects = dbContext.Projects.Where(p => p.StatusId != 4 && p.IsDeleted != true).ToList();
        //        return projects.Select(p => p.ToDto()).ToList();
        //    }
        //}



        //public bool UpdateProject(ProjectDTO projectDTO)
        //{
        //    using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
        //    {
        //        Project existingProject = dbContext.Projects.FirstOrDefault(p => p.Id == projectDTO.Id);
        //        if (existingProject == null)
        //            return false;

        //        existingProject.Name = projectDTO.Name;
        //        existingProject.ProjectCode = projectDTO.ProjectCode;
        //        existingProject.Description = projectDTO.Description;
        //        existingProject.StartDate = projectDTO.StartDate;
        //        existingProject.EndDate = projectDTO.EndDate;
        //        existingProject.Budget = projectDTO.Budget;
        //        existingProject.StatusId = projectDTO.StatusId;
        //        existingProject.ManagerId = projectDTO.ManagerId;
        //        existingProject.PriorityId = projectDTO.PriorityId;
        //        existingProject.PercentComplete = projectDTO.PercentComplete;
        //        existingProject.UpdatedDate = DateTime.Now;

        //        dbContext.SaveChanges();
        //        return true;
        //    }
        //}

        //public bool DeleteProject(int projectId)
        //{
        //    using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
        //    {
        //        Project project = dbContext.Projects.FirstOrDefault(p => p.Id == projectId);
        //        if (project == null)
        //            return false;

        //        // Soft delete
        //        project.IsDeleted = true;
        //        project.UpdatedDate = DateTime.Now;
        //        dbContext.SaveChanges();
        //        return true;
        //    }
        //}

        //public decimal CalculateProjectProgress(int projectId)
        //{
        //    using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
        //    {
        //        var tasks = dbContext.Tasks.Where(t => t.ProjectId == projectId).ToList();
        //        if (tasks == null || tasks.Count == 0)
        //            return 0;

        //        decimal totalTasks = tasks.Count;
        //        decimal completedTasks = tasks.Count(t => t.PercentComplete == 100);

        //        return Math.Round((completedTasks / totalTasks) * 100, 2);
        //    }
        //}
    }
}
