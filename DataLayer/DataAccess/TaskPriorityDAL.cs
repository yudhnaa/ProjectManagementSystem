using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataAccess
{
    public class TaskPriorityDAL
    {
        public TaskPriority GetById(int priorityId)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var taskPriority = dbContext.TaskPriorities.FirstOrDefault(t => t.Id == priorityId && t.IsDeleted == false);
                    
                    return taskPriority;
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

        public List<TaskPriority> getTaskPriorities()
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var taskPriorities = dbContext.TaskPriorities.Where(t => t.IsDeleted == false).ToList();

                    return taskPriorities;
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
