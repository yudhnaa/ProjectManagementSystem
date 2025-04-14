using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BusinessLayer.Services
{
    public class TaskDAL
    {
        public List<Task> GetAllTasks()
        {
            try
            {
                using (var dBContext = new ProjectManagementSystemDBContext())
                {
                    List<Task> tasks = dBContext.Tasks
                        .Where(t => t.IsDeleted != true)
                        .ToList();

                    return tasks;
                }
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw ex;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw ex;
            }
        }

        public Task GetTaskById(int taskId)
        {
            try
            {
                using (var dBContext = new ProjectManagementSystemDBContext())
                {
                    var task = dBContext.Tasks.FirstOrDefault(t => t.Id == taskId);
                    return task;
                }
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

        public List<Task> GetTaskByProjectId(int projectId)
        {
            try
            {
                using (var dBContext = new ProjectManagementSystemDBContext())
                {
                    List<Task> tasks = dBContext.Tasks.Where(t => t.ProjectId == projectId).ToList();
                    return tasks;
                }
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

        public List<Task> GetTaskByUserId(int userId)
        {
            try
            {
                using (var dBContext = new ProjectManagementSystemDBContext())
                {
                    List<Task> tasks = dBContext.Tasks.Where(t => t.AssignedUserId == userId).ToList();
                    return tasks;
                }
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

        public List<Task> GetTaskByProjectIdAndUserId(int projectId, int userId)
        {
            try
            {
                using (var dBContext = new ProjectManagementSystemDBContext())
                {
                    List<Task> tasks = dBContext.Tasks.Where(t => t.ProjectId == projectId && t.AssignedUserId == userId).ToList();
                    return tasks;
                }
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

        public List<Task> GetTaskByKeyValue(string key, string value)
        {
            try
            {
                using (var dBContext = new ProjectManagementSystemDBContext())
                {
                    List<Task> tasks = dBContext.Tasks.Where(t => t.GetType().GetProperty(key).GetValue(t, null).ToString() == value).ToList();
                    return tasks;
                }
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

        public int CreateTask(Task task)
        {
            try
            {
                using (var dBContext = new ProjectManagementSystemDBContext())
                {
                    dBContext.Tasks.Add(task);
                    dBContext.SaveChanges();
                    return task.Id;
                }
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

        public bool UpdateTask(int taskId, string key, string value, int updatedByUserId)
        {
            using (var dBContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var task = dBContext.Tasks.FirstOrDefault(t => t.Id == taskId);
                    if (task == null)
                        return false;

                    var oldValue = string.Empty;

                    // Use reflection to update the property dynamically
                    var property = typeof(Task).GetProperty(key);
                    if (property == null || !property.CanWrite)
                        return false;

                    oldValue = property.GetValue(task)?.ToString();
                    var convertedValue = Convert.ChangeType(value, property.PropertyType);
                    property.SetValue(task, convertedValue);

                    task.UpdatedDate = DateTime.Now;

                    // Add task history record
                    var taskHistory = new TaskHistory
                    {
                        TaskId = taskId,
                        FieldChanged = key,
                        OldValue = oldValue,
                        NewValue = value,
                        ChangedBy = updatedByUserId,
                        ChangedDate = DateTime.Now
                    };

                    dBContext.TaskHistories.Add(taskHistory);
                    dBContext.SaveChanges();

                    return true;
                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                    throw ex;
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    throw ex;
                }
            }
        }

        public bool RecordTimeEntry(int taskId, DateTime entryDate, string description, int userId)
        {
            using (var dBContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    //var timeEntry = new TimeEntry
                    //{
                    //    TaskId = taskId,
                    //    UserId = userId,
                    //     = hours,
                    //    EntryDate = entryDate,
                    //    Description = description,
                    //    CreatedDate = DateTime.Now
                    //};

                    //dBContext.TimeEntries.Add(timeEntry);

                    //// Update actual hours on task
                    //var task = dBContext.Tasks.FirstOrDefault(t => t.Id == taskId);
                    //if (task != null)
                    //{
                    //    task.ActualHours = (task.ActualHours ?? 0) + hours;
                    //    task.UpdatedDate = DateTime.Now;
                    //}

                    //dBContext.SaveChanges();
                    return true;
                }
                catch (SqlException ex)
                {
                    // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                    throw ex;
                }
                catch (Exception ex)
                {
                    // Handle other exceptions
                    throw ex;
                }
            }
        }
    }
}
