using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ProjectStatusDAL
    {
        public List<ProjectStatus> GetAllProjectStatuses()
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    List<ProjectStatus> projectStatuses = dbContext.ProjectStatuses.Where(p => p.IsDeleted == false && p.IsActive == true).ToList();

                    return projectStatuses;
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
        }

        public ProjectStatus GetById(int statusId)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var projectStatus = dbContext.ProjectStatuses
                                                 .FirstOrDefault(p => p.Id == statusId && p.IsDeleted == false && p.IsActive == true);
                    return projectStatus;
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

        public ProjectStatus GetProjectStatusByName(string v)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var projectStatus = dbContext.ProjectStatuses
                                                 .FirstOrDefault(p => p.Name == v && p.IsDeleted == false && p.IsActive == true);

                    return projectStatus;
                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                    throw new Exception("Database error occurred while retrieving project status by name.", ex);
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    throw new Exception("An error occurred while retrieving project status by name.", ex);
                }
            }
        }
    }
}
