using DataLayer.DataAccess;
using DataLayer.Domain;
using DTOLayer;
using DTOLayer.Mappers;
using DTOLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BusinessLayer.Services
{
    public class TaskServices
    {
        //public List<TaskDTO> GetTaskByProjectId(int projectId)
        //{
        //    using (var dBContext = new ProjectManagementSystemDBContext())
        //    {
        //        List<TaskDTO> tasks = dBContext.Tasks.Where(t => t.ProjectId == projectId).ToList().Select(p => p.ToDto()).ToList();

        //        return tasks;
        //    }
        //}

        public List<TaskDTO> GetTaskByProjectIdAndUserId(int projectId, int userId)
        {
            try
            {
                TaskDAL taskDAL = new TaskDAL();

                List<Task> tasks = taskDAL.GetTaskByProjectIdAndUserId(projectId, userId);

                return tasks.Select(t => t.ToDto()).ToList();
            }
            catch (SqlException sqlEx)
            {
                // Log the SQL exception
                throw new Exception("Database error occurred while fetching tasks.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching tasks.", ex);
            }
        }

        public bool CreateTask(TaskDTO taskDTO, int createdByUserId)
        {
            try
            {
                TaskDAL taskDAL = new TaskDAL();

                var task = taskDTO.ToTaskEntity();

                task.CreatedBy = createdByUserId;
                task.IsDeleted = false;
                task.CreatedDate = DateTime.Now;

                if (task.StatusId == 3)
                    task.PercentComplete = 100;
                else
                    task.PercentComplete = 0;

                var res = taskDAL.CreateTask(task);

                if (res > 0)
                {
                    ////// Add task history record
                    TaskHistoryDTO taskHistoryDTO = new TaskHistoryDTO
                    {
                        TaskId = task.Id,
                        FieldChanged = "Created",
                        OldValue = null,
                        NewValue = task.ToString(),
                        ChangedBy = createdByUserId
                    };

                    TaskHistoryServices taskHistoryServices = new TaskHistoryServices();
                    var res1 = taskHistoryServices.addTaskHistory(taskHistoryDTO);
                    return res1 > 0;
                }
                else
                    return false;
            }
            catch (SqlException sqlEx)
            {
                // Log the SQL exception
                throw new Exception("Database error occurred while creating task.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while creating task.", ex);
            }
        }

        //public List<TaskDTO> GetTaskByUserId(int userId)
        //{
        //    using (var dBContext = new ProjectManagementSystemDBContext())
        //    {
        //        List<TaskDTO> tasks = dBContext.Tasks.Where(t => t.AssignedUserId == userId).ToList().Select(p => p.ToDto()).ToList();
        //        return tasks;
        //    }
        //}

        //
        //public TaskDTO GetTaskById(int taskId)
        //{
        //    using (var dBContext = new ProjectManagementSystemDBContext())
        //    {
        //        var task = dBContext.Tasks.FirstOrDefault(t => t.Id == taskId);
        //        return task?.ToDto();
        //    }
        //}

        //public List<TaskDTO> GetAllTasks()
        //{
        //    using (var dBContext = new ProjectManagementSystemDBContext())
        //    {
        //        List<TaskDTO> tasks = dBContext.Tasks
        //            .Where(t => t.IsDeleted != true)
        //            .ToList()
        //            .Select(p => p.ToDto())
        //            .ToList();
        //        return tasks;
        //    }
        //}



        //public bool UpdateTask(TaskDTO taskDto)
        //{
        //    using (var dBContext = new ProjectManagementSystemDBContext())
        //    {
        //        var task = dBContext.Tasks.FirstOrDefault(t => t.Id == taskDto.Id);
        //        if (task == null)
        //            return false;

        //        task.Name = taskDto.Name;
        //        task.Description = taskDto.Description;
        //        task.AssignedUserId = taskDto.AssignedUserId;
        //        task.StatusId = taskDto.StatusId;
        //        task.PriorityId = taskDto.PriorityId;
        //        task.StartDate = taskDto.StartDate;
        //        task.DueDate = taskDto.DueDate;
        //        task.EstimatedHours = taskDto.EstimatedHours;
        //        task.ActualHours = taskDto.ActualHours;
        //        task.PercentComplete = taskDto.PercentComplete;
        //        task.ParentTaskId = taskDto.ParentTaskId;
        //        task.BlockReason = taskDto.BlockReason;
        //        task.UpdatedDate = DateTime.Now;

        //        // If status changed, update LastStatusChangeDate
        //        if (task.StatusId != taskDto.StatusId)
        //        {
        //            task.LastStatusChangeDate = DateTime.Now;
        //        }

        //        dBContext.SaveChanges();
        //        return true;
        //    }
        //}

        //public bool DeleteTask(int taskId)
        //{
        //    using (var dBContext = new ProjectManagementSystemDBContext())
        //    {
        //        var task = dBContext.Tasks.FirstOrDefault(t => t.Id == taskId);
        //        if (task == null)
        //            return false;

        //        // Soft delete
        //        task.IsDeleted = true;
        //        task.UpdatedDate = DateTime.Now;

        //        dBContext.SaveChanges();
        //        return true;
        //    }
        //}

        //public bool UpdateTaskStatus(int taskId, int statusId, int userId)
        //{
        //    using (var dBContext = new ProjectManagementSystemDBContext())
        //    {
        //        var task = dBContext.Tasks.FirstOrDefault(t => t.Id == taskId);
        //        if (task == null)
        //            return false;

        //        task.StatusId = statusId;
        //        task.LastStatusChangeDate = DateTime.Now;
        //        task.UpdatedDate = DateTime.Now;

        //        // Add task history record
        //        var taskHistory = new TaskHistory
        //        {
        //            TaskId = taskId,
        //            FieldChanged = "Status",
        //            OldValue = task.StatusId.ToString(),
        //            NewValue = statusId.ToString(),
        //            ChangedBy = userId,
        //            ChangedDate = DateTime.Now
        //        };

        //        dBContext.TaskHistories.Add(taskHistory);
        //        dBContext.SaveChanges();

        //        return true;
        //    }
        //}

        //public bool UpdateTaskAssignment(int taskId, int assignedUserId, int updatedByUserId)
        //{
        //    using (var dBContext = new ProjectManagementSystemDBContext())
        //    {
        //        var task = dBContext.Tasks.FirstOrDefault(t => t.Id == taskId);
        //        if (task == null)
        //            return false;

        //        var oldAssignedUserId = task.AssignedUserId;
        //        task.AssignedUserId = assignedUserId;
        //        task.UpdatedDate = DateTime.Now;

        //        // Add task history record
        //        var taskHistory = new TaskHistory
        //        {
        //            TaskId = taskId,
        //            FieldChanged = "AssignedUser",
        //            OldValue = oldAssignedUserId.ToString(),
        //            NewValue = assignedUserId.ToString(),
        //            ChangedBy = updatedByUserId,
        //            ChangedDate = DateTime.Now
        //        };

        //        dBContext.TaskHistories.Add(taskHistory);
        //        dBContext.SaveChanges();

        //        return true;
        //    }
        //}

        //public bool UpdateTaskCompletion(int taskId, decimal percentComplete, int updatedByUserId)
        //{
        //    using (var dBContext = new ProjectManagementSystemDBContext())
        //    {
        //        var task = dBContext.Tasks.FirstOrDefault(t => t.Id == taskId);
        //        if (task == null)
        //            return false;

        //        var oldPercentComplete = task.PercentComplete;
        //        task.PercentComplete = percentComplete;
        //        task.UpdatedDate = DateTime.Now;

        //        // Add task history record
        //        var taskHistory = new TaskHistory
        //        {
        //            TaskId = taskId,
        //            FieldChanged = "PercentComplete",
        //            OldValue = oldPercentComplete.ToString(),
        //            NewValue = percentComplete.ToString(),
        //            ChangedBy = updatedByUserId,
        //            ChangedDate = DateTime.Now
        //        };

        //        dBContext.TaskHistories.Add(taskHistory);
        //        dBContext.SaveChanges();

        //        return true;
        //    }
        //}

        //public List<TaskDTO> GetTasksByStatus(int statusId)
        //{
        //    using (var dBContext = new ProjectManagementSystemDBContext())
        //    {
        //        List<TaskDTO> tasks = dBContext.Tasks
        //            .Where(t => t.StatusId == statusId && t.IsDeleted != true)
        //            .ToList()
        //            .Select(p => p.ToDto())
        //            .ToList();
        //        return tasks;
        //    }
        //}

        //public List<TaskDTO> GetTasksByPriority(int priorityId)
        //{
        //    using (var dBContext = new ProjectManagementSystemDBContext())
        //    {
        //        List<TaskDTO> tasks = dBContext.Tasks
        //            .Where(t => t.PriorityId == priorityId && t.IsDeleted != true)
        //            .ToList()
        //            .Select(p => p.ToDto())
        //            .ToList();
        //        return tasks;
        //    }
        //}

        //public List<TaskDTO> GetChildTasks(int parentTaskId)
        //{
        //    using (var dBContext = new ProjectManagementSystemDBContext())
        //    {
        //        List<TaskDTO> tasks = dBContext.Tasks
        //            .Where(t => t.ParentTaskId == parentTaskId && t.IsDeleted != true)
        //            .ToList()
        //            .Select(p => p.ToDto())
        //            .ToList();
        //        return tasks;
        //    }
        //}

        //public List<TaskDTO> GetTasksDueInDateRange(DateTime startDate, DateTime endDate)
        //{
        //    using (var dBContext = new ProjectManagementSystemDBContext())
        //    {
        //        List<TaskDTO> tasks = dBContext.Tasks
        //            .Where(t => t.DueDate >= startDate && t.DueDate <= endDate && t.IsDeleted != true)
        //            .ToList()
        //            .Select(p => p.ToDto())
        //            .ToList();
        //        return tasks;
        //    }
        //}

        //public List<TaskDTO> GetOverdueTasks()
        //{
        //    var today = DateTime.Today;
        //    using (var dBContext = new ProjectManagementSystemDBContext())
        //    {
        //        List<TaskDTO> tasks = dBContext.Tasks
        //            .Where(t => t.DueDate < today && t.PercentComplete < 100 && t.IsDeleted != true)
        //            .ToList()
        //            .Select(p => p.ToDto())
        //            .ToList();
        //        return tasks;
        //    }
        //}

        //public List<TaskDTO> GetBlockedTasks()
        //{
        //    using (var dBContext = new ProjectManagementSystemDBContext())
        //    {
        //        List<TaskDTO> tasks = dBContext.Tasks
        //            .Where(t => !string.IsNullOrEmpty(t.BlockReason) && t.IsDeleted != true)
        //            .ToList()
        //            .Select(p => p.ToDto())
        //            .ToList();
        //        return tasks;
        //    }
        //}

        //public bool AddTaskComment(int taskId, string comment, int userId)
        //{
        //    using (var dBContext = new ProjectManagementSystemDBContext())
        //    {
        //        var taskComment = new TaskComment
        //        {
        //            TaskId = taskId,
        //            UserId = userId,
        //            CommentText = comment,
        //            CreatedDate = DateTime.Now
        //        };

        //        dBContext.TaskComments.Add(taskComment);
        //        dBContext.SaveChanges();

        //        return true;
        //    }
        //}

        //public bool RecordTimeEntry(int taskId, DateTime entryDate, string description, int userId)
        //{
        //    using (var dBContext = new ProjectManagementSystemDBContext())
        //    {
        //        //var timeEntry = new TimeEntry
        //        //{
        //        //    TaskId = taskId,
        //        //    UserId = userId,
        //        //     = hours,
        //        //    EntryDate = entryDate,
        //        //    Description = description,
        //        //    CreatedDate = DateTime.Now
        //        //};

        //        //dBContext.TimeEntries.Add(timeEntry);

        //        //// Update actual hours on task
        //        //var task = dBContext.Tasks.FirstOrDefault(t => t.Id == taskId);
        //        //if (task != null)
        //        //{
        //        //    task.ActualHours = (task.ActualHours ?? 0) + hours;
        //        //    task.UpdatedDate = DateTime.Now;
        //        //}

        //        //dBContext.SaveChanges();
        //        return true;
        //    }
        //}

    }
}
