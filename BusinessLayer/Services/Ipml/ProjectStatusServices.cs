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
    public class ProjectStatusServices : IProjectStatusServices
    {
        public bool CreateProjectStatus(ProjectStatusDTO projectStatusDTO)
        {
            try
            {
                ProjectStatusDAL projectStatusDAL = new ProjectStatusDAL();

                var projectStatus = projectStatusDTO.ToProjectStatusEntity();
                projectStatus.IsDeleted = false;
                projectStatus.IsActive = true;
                projectStatus.CreatedDate = DateTime.Now;
                projectStatus.UpdatedDate = null;

                var res = projectStatusDAL.CreateProjectStatus(projectStatus);

                return res;
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while adding project status.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while adding project status.", ex);
            }
        }

        public List<ProjectStatusDTO> GetAllProjectStatuses(string kw)
        {
            try
            {
                ProjectStatusDAL projectStatusDAL = new ProjectStatusDAL();

                List<ProjectStatus> projectStatuses = projectStatusDAL.GetAllProjectStatuses(kw, isIncludeInActive: false);
                if (projectStatuses == null)
                    return null;

                return projectStatuses.Select(status => status.ToDto()).ToList();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while retrieving project statuses.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while retrieving project statuses.", ex);
            }
        }

        public List<ProjectStatusDTO> GetAllProjectStatusesInlcudeInActive(string kw)
        {
            try
            {
                ProjectStatusDAL projectStatusDAL = new ProjectStatusDAL();

                List<ProjectStatus> projectStatuses = projectStatusDAL.GetAllProjectStatuses(kw, isIncludeInActive: true);
                if (projectStatuses == null)
                    return null;

                return projectStatuses.Select(status => status.ToDto()).ToList();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while retrieving project statuses.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while retrieving project statuses.", ex);
            }
        }

        public ProjectStatusDTO GetById(int statusId)
        {
            try
            {
                ProjectStatusDAL projectStatusDAL = new ProjectStatusDAL();
                var projectStatus = projectStatusDAL.GetById(statusId, isIncludeInActive: false);

                if (projectStatus == null)
                    throw new Exception("Project status not found.");

                return projectStatus.ToDto();

            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while retrieving project status.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while retrieving project status.", ex);
            }
        }

        public ProjectStatusDTO GetByIdInlcudeInActive(int statusId)
        {
            try
            {
                ProjectStatusDAL projectStatusDAL = new ProjectStatusDAL();
                var projectStatus = projectStatusDAL.GetById(statusId, isIncludeInActive: true);
                if (projectStatus == null)
                    throw new Exception("Project status not found.");

                return projectStatus.ToDto();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while retrieving project status.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while retrieving project status.", ex);
            }
        }

        public List<ProjectStatusDTO> GetProjectStatusByName(string kw)
        {
            try
            {
                ProjectStatusDAL projectStatusDAL = new ProjectStatusDAL();

                var projectStatus = projectStatusDAL.GetProjectStatusByName(kw, isIncludeInActive: false);
                if (projectStatus == null)
                    return null;

                return projectStatus.Select(status => status.ToDto()).ToList();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while retrieving project status.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while retrieving project status.", ex);
            }
        }

        public List<ProjectStatusDTO> GetProjectStatusByNameInlcudeInActive(string kw)
        {
            try
            {
                ProjectStatusDAL projectStatusDAL = new ProjectStatusDAL();

                var projectStatus = projectStatusDAL.GetProjectStatusByName(kw, isIncludeInActive: true);
                if (projectStatus == null)
                    return null;

                return projectStatus.Select(status => status.ToDto()).ToList();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while retrieving project status.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while retrieving project status.", ex);
            }
        }

        public bool UpdateProjectStatus(ProjectStatusDTO item)
        {
            try
            {
                ProjectStatusDAL projectStatusDAL = new ProjectStatusDAL();

                var projectStatus = item.ToProjectStatusEntity();
                projectStatus.UpdatedDate = DateTime.Now;

                var res = projectStatusDAL.UpdateProjectStatus(projectStatus);
                return res;
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while updating project status.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while updating project status.", ex);
            }
        }
    }
}
