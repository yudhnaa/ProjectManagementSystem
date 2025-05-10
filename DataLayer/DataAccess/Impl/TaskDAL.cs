using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DataLayer.DataAccess
{
    public class TaskDAL : ITaskDAL
    {
        public List<Task> GetAllTasks(string kw, bool isIncludeInActive)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    IQueryable<Task> query = dbContext.Tasks;

                    if (!isIncludeInActive)
                        query = query.Where(t => t.IsDeleted == false);

                    if (!string.IsNullOrWhiteSpace(kw))
                        query = query.Where(t => t.Name.Contains(kw) || t.Code.Contains(kw));

                    return query.ToList();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        
        public Task GetTaskById(int taskId, bool isIncludeInActive)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    IQueryable<Task> query = dbContext.Tasks.Where(t => t.Id == taskId);

                    if (!isIncludeInActive)
                        query = query.Where(t => t.IsDeleted == false);

                    return query.FirstOrDefault();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public List<Task> GetTaskByKeyValue(string key, string value, bool isIncludeInActive)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var property = typeof(Task).GetProperty(key);
                    if (property == null)
                        return new List<Task>();

                    var parameter = System.Linq.Expressions.Expression.Parameter(typeof(Task), "t");
                    var propertyAccess = System.Linq.Expressions.Expression.Property(parameter, property);
                    var toStringCall = System.Linq.Expressions.Expression.Call(propertyAccess,
                        typeof(object).GetMethod("ToString"));
                    var equalsMethod = typeof(string).GetMethod("Equals", new[] { typeof(string) });
                    var valueConstant = System.Linq.Expressions.Expression.Constant(value, typeof(string));
                    var equalExpression = System.Linq.Expressions.Expression.Call(toStringCall, equalsMethod, valueConstant);

                    var lambda = System.Linq.Expressions.Expression.Lambda<Func<Task, bool>>(
                        equalExpression, parameter);

                    IQueryable<Task> query = dbContext.Tasks.Where(lambda);

                    if (!isIncludeInActive)
                        query = query.Where(t => t.IsDeleted == false);

                    return query.ToList();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public List<Task> GetTaskByProjectId(int projectId, bool isIncludeInActive)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    IQueryable<Task> query = dbContext.Tasks.Where(t => t.ProjectId == projectId);

                    if (!isIncludeInActive)
                        query = query.Where(t => t.IsDeleted == false);

                    return query.ToList();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public List<Task> GetTaskByUserId(int userId, bool isIncludeInActive)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    IQueryable<Task> query = dbContext.Tasks.Where(t => t.AssignedUserId == userId);

                    if (!isIncludeInActive)
                        query = query.Where(t => t.IsDeleted == false);

                    return query.ToList();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public List<Task> GetTaskByProjectIdAndUserId(int projectId, int userId, bool isIncludeInActive)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    IQueryable<Task> query = dbContext.Tasks
                        .Where(t => t.ProjectId == projectId && t.AssignedUserId == userId);

                    if (!isIncludeInActive)
                        query = query.Where(t => t.IsDeleted == false);

                    return query.ToList();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public List<Task> GetTasksByKeywordAndStatus(string keyword, int statusId, bool isIncludeInActive)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    IQueryable<Task> query = dbContext.Tasks
                        .Where(t => t.Name.Contains(keyword) && t.StatusId == statusId);

                    if (!isIncludeInActive)
                        query = query.Where(t => t.IsDeleted == false);

                    return query.ToList();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public int CreateTask(Task task)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    dbContext.Tasks.Add(task);
                    dbContext.SaveChanges();
                    return task.Id;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public bool UpdateTask(int taskId, string key, string value, int updatedByUserId)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var task = dbContext.Tasks.FirstOrDefault(t => t.Id == taskId);
                    if (task == null)
                        return false;

                    // Use reflection to update the property dynamically
                    var property = typeof(Task).GetProperty(key);
                    if (property == null || !property.CanWrite)
                        return false;

                    string oldValue = property.GetValue(task)?.ToString() ?? string.Empty;
                    var convertedValue = Convert.ChangeType(value, property.PropertyType);
                    property.SetValue(task, convertedValue);

                    task.UpdatedDate = DateTime.Now;

                    // Add task history record
                    dbContext.TaskHistories.Add(new TaskHistory
                    {
                        TaskId = taskId,
                        FieldChanged = key,
                        OldValue = oldValue,
                        NewValue = value,
                        ChangedBy = updatedByUserId,
                        ChangedDate = DateTime.Now
                    });

                    dbContext.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public int UpdateTask(Task task)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var existingTask = dbContext.Tasks.FirstOrDefault(t => t.Id == task.Id);
                    if (existingTask == null)
                        return 0;

                    existingTask.Name = task.Name;
                    existingTask.Code = task.Code;
                    existingTask.StartDate = task.StartDate;
                    existingTask.DueDate = task.DueDate;
                    existingTask.AssignedUserId = task.AssignedUserId;
                    existingTask.StatusId = task.StatusId;
                    existingTask.PriorityId = task.PriorityId;
                    if (task.ParentTaskId > 0)
                        existingTask.ParentTaskId = task.ParentTaskId;
                    existingTask.EstimatedHours = task.EstimatedHours;
                    existingTask.Description = task.Description;
                    existingTask.UpdatedDate = DateTime.Now;

                    dbContext.SaveChanges();
                    return 1;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public int CountTaskByProjectId(int id)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    return dbContext.Tasks.Count(t => t.ProjectId == id);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        //public List<Task> GetTasksByUserWithStatus(int userId)
        //{
        //    using (var dbContext = new ProjectManagementSystemDBContext())
        //    {
        //        try
        //        {
        //            return dbContext.Tasks
        //            .Where(t => t.AssignedUserId == userId && (t.IsDeleted == false || t.IsDeleted == null))
        //            .ToList();
        //        }
        //        catch (Exception ex)
        //        {
        //            throw;
        //        }
        //    }
        //}

        public List<Task> GetTaskByProjectIdAndUserIdAndKw(int projectId, int userId, string kw, bool isIncludeInActive)
        {
            using (var dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    IQueryable<Task> query = dbContext.Tasks
                        .Where(t => t.ProjectId == projectId && t.AssignedUserId == userId && (t.Name.Contains(kw) || t.Code.Contains(kw)));

                    if (!isIncludeInActive)
                        query = query.Where(t => t.IsDeleted == false);

                    return query.ToList();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
       


    }
}
