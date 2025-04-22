using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
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

        public int UpdateTask(Task task)
        {
            try
            {
                using (var dBContext = new ProjectManagementSystemDBContext())
                {
                    var existingTask = dBContext.Tasks.FirstOrDefault(t => t.Id == task.Id);

                    if (existingTask == null)
                        return 0;

                    existingTask.Name = task.Name;
                    existingTask.Code = task.Code;
                    existingTask.StartDate = task.StartDate;
                    existingTask.DueDate = task.DueDate;
                    existingTask.AssignedUserId = task.AssignedUserId;
                    existingTask.StatusId = task.StatusId;
                    existingTask.PriorityId = task.PriorityId;
                    existingTask.ParentTaskId = task.ParentTaskId;
                    existingTask.EstimatedHours = task.EstimatedHours;
                    existingTask.Description = task.Description;

                    dBContext.SaveChanges();


                    return 1;
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

        public List<Task> GetTaskByKw(string kw, int v)
        {
            try
            {
                using (var dBContext = new ProjectManagementSystemDBContext())
                {
                    var tasks = dBContext.Tasks
                        .Where(t => t.Name.Contains(kw) || t.Code.Contains(kw) || t.Id.Equals(kw))
                        .Take(v)
                        .ToList();

                    if (tasks == null || tasks.Count == 0)
                        return null;
                    
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

        public int CountTaskByProjectId(int id)
        {
            try
            {
                using (var dBContext = new ProjectManagementSystemDBContext())
                {
                    var count = dBContext.Tasks.Count(t => t.ProjectId == id);
                    
                    if (count == 0)
                        return 0;

                    return count;
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
        public List<Task> GetTaskByKwAndStatus(string kw,int statusId, int v , int userId, int projectId)
        {
            try
            {
                //sử dụng bDContext 
                using (var dBContext = new ProjectManagementSystemDBContext())
                {
                    //truy vấn dữ liệu where statusId = statusId AND Id like kw AND Name like kw AND code like kw
                    var tasks = dBContext.Tasks.Where(t => t.TaskStatus.Id == statusId && (t.Code.Contains(kw) || t.Name.Contains(kw)) &&  t.AssignedUserId == userId && t.ProjectId == projectId)
                                .Take(v)
                                .ToList();
                    //Take v là lấy số lượng v task hiển thị danh sách
                    
                    //kiểm tra không tìm thấy task thì trả về null
                    if (tasks == null || tasks.Count == 0)
                        return null;
                    
                    //trả về danh sách tasks
                    return tasks;
                }
            }
            //catch bắt các ngoại lệ
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
