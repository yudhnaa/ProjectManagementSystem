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
    public class TaskStatusDAL : ITaskStatusDAL
    {
        public bool CreateTaskStatus(TaskStatus taskStatus)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    dbContext.TaskStatuses.Add(taskStatus);
                    return dbContext.SaveChanges() > 0;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Database error occurred while adding task status.", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while adding task status.", ex);
                }
            }
        }

        public bool UpdateTasktStatus(TaskStatus taskStatus)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var existingTaskStatus = dbContext.TaskStatuses.Find(taskStatus.Id);
                    if (existingTaskStatus == null)
                        return false;

                    existingTaskStatus.Name = taskStatus.Name;
                    existingTaskStatus.Description = taskStatus.Description;
                    existingTaskStatus.UpdatedDate = DateTime.Now;
                    return dbContext.SaveChanges() > 0;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Database error occurred while updating task status.", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while updating task status.", ex);
                }
            }
        }

        public TaskStatus GetById(int statusId, bool isIncludeInActive)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    return dbContext.TaskStatuses.FirstOrDefault(t =>
                        t.Id == statusId &&
                        (isIncludeInActive || t.IsDeleted == false && t.IsActive == true));
                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving task status by ID.", ex);
                }
            }
        }

        public List<TaskStatus> GetTaskStatusByName(string kw, bool isIncludeInActive)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var query = dbContext.TaskStatuses.AsQueryable();

                    if (!isIncludeInActive)
                        query = query.Where(t => t.IsDeleted == false && t.IsActive == true);

                    if (!string.IsNullOrEmpty(kw))
                        query = query.Where(t => t.Name.Contains(kw));

                    return query.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving task status by name.", ex);
                }
            }
        }

        public List<TaskStatus> GetAllTaskStatuses(string kw, bool isIncludeInActive)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var query = dbContext.TaskStatuses.AsQueryable();

                    if (!isIncludeInActive)
                        query = query.Where(t => t.IsDeleted == false && t.IsActive == true);

                    if (!string.IsNullOrEmpty(kw))
                        query = query.Where(t => t.Name.Contains(kw));

                    return query.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving all task statuses.", ex);
                }
            }
        }
    }
}
