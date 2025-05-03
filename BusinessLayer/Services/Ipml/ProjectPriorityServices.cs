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
    public class ProjectPriorityServices : IProjectPriorityServices
    {
        public bool CreateProjectPriority(ProjectPriorityDTO projectPriorityDTO)
        {
            try
            {
                ProjectPriorityDAL projectPriorityDAL = new ProjectPriorityDAL();

                var projectPriority = projectPriorityDTO.ToProjectPriorityEntity();
                projectPriority.IsDeleted = false;
                projectPriority.IsActive = true;
                projectPriority.CreatedDate = DateTime.Now;
                projectPriority.UpdatedDate = null;

                var res = projectPriorityDAL.CreateProjectPriority(projectPriority);

                return res;
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while adding project priorities.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while adding project priorities.", ex);
            }
        }

        public bool UpdateProjectPriority(ProjectPriorityDTO item)
        {
            try
            {
                ProjectPriorityDAL projectPriorityDAL = new ProjectPriorityDAL();

                var projectPriority = item.ToProjectPriorityEntity();
                projectPriority.UpdatedDate = DateTime.Now;

                var res = projectPriorityDAL.UpdateProjectPriority(projectPriority);
                return res;
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while updating project priorities.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while updating project priorities.", ex);
            }
        }
        public List<ProjectPriorityDTO> GetAllProjectPriorities(string kw)
        {
            try
            {
                ProjectPriorityDAL projectPriorityDAL = new ProjectPriorityDAL();
                var projectPriorities = projectPriorityDAL.GetAllProjectPriorities(kw, isIncludeInActive: false);
                if (projectPriorities == null)
                    return null;

                return projectPriorities.Select(p => p.ToDto()).ToList();

            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while retrieving project priorities.", ex);
            }
            catch
            (Exception ex)
            {
                // Handle exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("An error occurred while retrieving project priorities.", ex);
            }

        }

        public List<ProjectPriorityDTO> GetAllProjectPrioritiesInlcudeInActive(string kw)
        {
            try
            {
                ProjectPriorityDAL projectPriorityDAL = new ProjectPriorityDAL();
                var projectPriorities = projectPriorityDAL.GetAllProjectPriorities(kw, isIncludeInActive: true);
                if (projectPriorities == null)
                    return null;

                return projectPriorities.Select(p => p.ToDto()).ToList();

            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while retrieving project priorities.", ex);
            }
            catch
            (Exception ex)
            {
                // Handle exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("An error occurred while retrieving project priorities.", ex);
            }

        }

        public ProjectPriorityDTO GetAllProjectPrioritiesById(int priorityId)
        {
            try
            {
                ProjectPriorityDAL projectPriorityDAL = new ProjectPriorityDAL();

                var projectPriority = projectPriorityDAL.GetById(priorityId, isIncludeInActive: false);
                if (projectPriority == null)
                    throw new Exception("Project priority not found.");

                return projectPriority.ToDto();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while retrieving project priorities.", ex);
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("An error occurred while retrieving project priorities.", ex);
            }
        }

        public ProjectPriorityDTO GetAllProjectPrioritiesByIdInlcudeInActive(int priorityId)
        {
            try
            {
                ProjectPriorityDAL projectPriorityDAL = new ProjectPriorityDAL();

                var projectPriority = projectPriorityDAL.GetById(priorityId, isIncludeInActive: true);
                if (projectPriority == null)
                    throw new Exception("Project priority not found.");

                return projectPriority.ToDto();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while retrieving project priorities.", ex);
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("An error occurred while retrieving project priorities.", ex);
            }
        }
    }
}
