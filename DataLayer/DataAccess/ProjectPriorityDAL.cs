using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ProjectPriorityDAL
    {
        public List<ProjectPriority> GetAllProjectPriorities()
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var projectPriorities = dbContext.ProjectPriorities.ToList();
                    return projectPriorities;
                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                    throw ex;
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., log the error, rethrow, etc.)
                    throw ex;
                }
            }
        }

        public ProjectPriority GetById(int priorityId)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var projectPriority = dbContext.ProjectPriorities.Find(priorityId);

                    return projectPriority;
                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                    throw ex;
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., log the error, rethrow, etc.)
                    throw ex;
                }
            }
        }
    }
}
