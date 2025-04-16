using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskStatus = DataLayer.Domain.TaskStatus;

namespace DataLayer.DataAccess
{
    public class TaskStatusDAL
    {
        public TaskStatus GetById(int statusId)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var taskStatus = dbContext.TaskStatuses.FirstOrDefault(t => t.Id == statusId && t.IsDeleted == false);
                    return taskStatus;
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

        public List<TaskStatus> getTaskStatuses()
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var taskStatuses = dbContext.TaskStatuses.ToList();

                    return taskStatuses;
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
