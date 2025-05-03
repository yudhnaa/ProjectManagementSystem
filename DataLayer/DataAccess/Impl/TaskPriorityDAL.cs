using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataAccess
{
    public class TaskPriorityDAL : ITaskPriorityDAL
    {
        public bool CreateTaskPriorities(TaskPriority taskPriorities)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    dbContext.TaskPriorities.Add(taskPriorities);
                    return dbContext.SaveChanges() > 0;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Database error occurred while adding task priorities.", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while adding task priorities.", ex);
                }
            }
        }

        public bool UpdateTasktPriorities(TaskPriority taskPriorities)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var existingTaskPriorities = dbContext.TaskPriorities.Find(taskPriorities.Id);
                    if (existingTaskPriorities == null)
                        return false;

                    existingTaskPriorities.Name = taskPriorities.Name;
                    existingTaskPriorities.Description = taskPriorities.Description;
                    existingTaskPriorities.UpdatedDate = DateTime.Now;

                    return dbContext.SaveChanges() > 0;
                }
                catch (SqlException ex)
                {
                    throw new Exception("Database error occurred while updating task priorities.", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while updating task priorities.", ex);
                }
            }
        }

        public TaskPriority GetById(int priorityId, bool isIncludeInActive)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    IQueryable<TaskPriority> query = dbContext.TaskPriorities.Where(t => t.Id == priorityId);

                    if (!isIncludeInActive)
                        query = query.Where(t => t.IsDeleted == false && t.IsActive == true);

                    return query.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving task priority by ID.", ex);
                }
            }
        }

        public List<TaskPriority> GetAllTaskPriorities(string kw, bool isIncludeInActive)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    IQueryable<TaskPriority> query = dbContext.TaskPriorities;

                    if (!isIncludeInActive)
                        query = query.Where(t => t.IsDeleted == false && t.IsActive == true);

                    if (!string.IsNullOrEmpty(kw))
                        query = query.Where(t => t.Name.Contains(kw) || t.Description.Contains(kw));

                    return query.ToList();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving task priorities.", ex);
                }
            }
        }
    }
}
