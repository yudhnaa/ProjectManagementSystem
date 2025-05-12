using BusinessLayer.Services;
using DataLayer.DataAccess;
using DataLayer.Domain;
using DTOLayer.Mappers;
using DTOLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace BusinessLayer.Services
{
    public class ProjectServices : IProjectServices
    {
        public ProjectDTO CreateProject(ProjectDTO projectDTO)
        {
            try
            {
                ProjectDAL projectDAL = new ProjectDAL();

                Project project = projectDTO.ToProjectEntity();

                project.PercentComplete = 0;
                project.IsDeleted = false;

                Project res = projectDAL.CreateProject(project);

                return ProjectDTOMapper.ToDto(res);
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

        public bool DeleteProject(ProjectDTO project)
        {
            try
            {
                ProjectDAL projectDAL = new ProjectDAL();
                ProjectDTO existingProject = this.GetProjectById(project.Id);

                if (existingProject == null)
                    throw new Exception("Project not found.");

                ProjectStatusServices projectStatusServices = new ProjectStatusServices();
                int projectCancelledStatusId = projectStatusServices.GetProjectStatusByName("Cancelled").First().Id;

                existingProject.StatusId = projectCancelledStatusId;
                existingProject.IsDeleted = true;
                existingProject.UpdatedDate = DateTime.Now;

                var res = projectDAL.UpdateProject(existingProject.ToProjectEntity());

                return res;
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while deleting project.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while deleting project.", ex);
            }
        }

        public List<ProjectDTO> GetAllProjects(string kw)
        {
            try
            {
                ProjectDAL projectDAL = new ProjectDAL();

                var projects = projectDAL.GetAllProjects(kw, isIncludeInActive: false);
                if (projects == null)
                    return null;

                return projects.Select(p => ProjectDTOMapper.ToDto(p)).ToList();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while retrieving all projects.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while retrieving all projects.", ex);
            }
        }

        public List<ProjectDTO> GetAllProjectsInlcudeInActive(string kw)
        {
            try
            {
                ProjectDAL projectDAL = new ProjectDAL();

                var projects = projectDAL.GetAllProjects(kw, isIncludeInActive: true);
                if (projects == null)
                    return null;

                return projects.Select(p => ProjectDTOMapper.ToDto(p)).ToList();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while retrieving all projects.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while retrieving all projects.", ex);
            }
        }

        public List<ProjectForListDTO> GetAllProjectsForList(string kw)
        {
            try
            {
                ProjectDAL projectDAL = new ProjectDAL();

                var projects = projectDAL.GetAllProjects(kw, isIncludeInActive: false);
                if (projects == null)
                    return null;

                return projects.Select(p => ProjectForListDTOMapper.ToDto(p)).ToList();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while retrieving all projects.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while retrieving all projects.", ex);
            }
        }

        public List<ProjectForListDTO> GetAllProjectsForListInlcudeInActive(string kw)
        {
            try
            {
                ProjectDAL projectDAL = new ProjectDAL();

                var projects = projectDAL.GetAllProjects(kw, isIncludeInActive: true);
                if (projects == null)
                    return null;

                return projects.Select(p => ProjectForListDTOMapper.ToDto(p)).ToList();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while retrieving all projects.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while retrieving all projects.", ex);
            }
        }

        public ProjectDTO GetProjectById(int projectId)
        {
            try
            {
                ProjectDAL projectDAL = new ProjectDAL();
                var project = projectDAL.GetProjectById(projectId, isIncludeInActive: false);

                if (project == null)
                    throw new Exception("Project not found.");

                return ProjectDTOMapper.ToDto(project);
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

        public ProjectDTO GetProjectByIdInlcudeInActive(int projectId)
        {
            try
            {
                ProjectDAL projectDAL = new ProjectDAL();
                var project = projectDAL.GetProjectById(projectId, isIncludeInActive: true);

                if (project == null)
                    throw new Exception("Project not found.");

                return ProjectDTOMapper.ToDto(project);
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

        public ProjectForListDTO GetProjectForListById(int projectId)
        {
            try
            {
                ProjectDAL projectDAL = new ProjectDAL();
                var project = projectDAL.GetProjectById(projectId, isIncludeInActive: false);

                if (project == null)
                    throw new Exception("Project not found.");

                return ProjectForListDTOMapper.ToDto(project);
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

        public ProjectForListDTO GetProjectForListByIdInlcudeInActive(int projectId)
        {
            try
            {
                ProjectDAL projectDAL = new ProjectDAL();
                var project = projectDAL.GetProjectById(projectId, isIncludeInActive: true);

                if (project == null)
                    throw new Exception("Project not found.");

                return ProjectForListDTOMapper.ToDto(project);
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

        public List<ProjectDTO> GetProjectsByUserId(int userId)
        {
            try
            {
                ProjectDAL projectDAL = new ProjectDAL();
                var projects = projectDAL.GetProjectsByUserId(userId, isIncludeInActive: false);
                if (projects == null)
                    return null;

                return projects.Select(p => ProjectDTOMapper.ToDto(p)).ToList();
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

        public List<ProjectDTO> GetProjectsByUserIdInlcudeInActive(int userId)
        {
            try
            {
                ProjectDAL projectDAL = new ProjectDAL();
                var projects = projectDAL.GetProjectsByUserId(userId, isIncludeInActive: false);
                if (projects == null)
                    return null;

                return projects.Select(p => ProjectDTOMapper.ToDto(p)).ToList();
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

        public List<ProjectForListDTO> GetProjectsForListByUserId(int userId)
        {
            try
            {
                ProjectDAL projectDAL = new ProjectDAL();
                var projects = projectDAL.GetProjectsByUserId(userId, isIncludeInActive: false);
                if (projects == null)
                    return null;

                return projects.Select(p => ProjectForListDTOMapper.ToDto(p)).ToList();
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

        public List<ProjectForListDTO> GetProjectsForListByUserIdInlcudeInActive(int userId)
        {
            try
            {
                ProjectDAL projectDAL = new ProjectDAL();
                var projects = projectDAL.GetProjectsByUserId(userId, isIncludeInActive: true);
                if (projects == null)
                    return null;

                return projects.Select(p => ProjectForListDTOMapper.ToDto(p)).ToList();
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

        public bool HardDeleteProjectById(int id)
        {
            // check relation before delete
            throw new NotImplementedException();
        }

        public bool UpdateProject(ProjectDTO project)
        {
            try
            {
                ProjectDAL projectDAL = new ProjectDAL();
                ProjectDTO existingProject = this.GetProjectById(project.Id);
                if (existingProject == null)
                    throw new Exception("Project not found.");

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

                var res = projectDAL.UpdateProject(existingProject.ToProjectEntity());
                return res;
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while updating project.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while updating project.", ex);
            }
        }
    }
}