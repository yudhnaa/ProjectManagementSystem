using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataAccess.Impl
{
    public class TaskDependencyDAL : ITaskDependencyDAL
    {
        public List<TaskDependency> GetDependentTaskIds(int taskId, bool isIncludeInActive)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    IQueryable<TaskDependency> query = dbContext.TaskDependencies.Where(t => t.TaskId == taskId);
                    //if (!isIncludeInActive)
                    //    query = query.Where(t => t.IsDeleted == false && t.IsActive == true);
                    return query.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving dependent task IDs.", ex);
                }
            }
        }
    }
}
