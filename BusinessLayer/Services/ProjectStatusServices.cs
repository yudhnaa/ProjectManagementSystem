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
    public class ProjectStatusServices
    {
        public List<ProjectStatusDTO> GetAllProjectStatuses()
        {
            try
            {
                ProjectStatusDAL projectStatusDAL = new ProjectStatusDAL();


                List<ProjectStatus> projectStatuses = projectStatusDAL.GetAllProjectStatuses();

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
                var projectStatus = projectStatusDAL.GetById(statusId);
                if (projectStatus != null)
                {
                    return projectStatus.ToDto();
                }
                else
                {
                    throw new Exception("Project status not found.");
                }
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
    }
}
