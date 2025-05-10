using DataLayer.DataAccess;
using DataLayer.Domain;
using DataLayer.EnumObjects;
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
    public class TaskServices : ITaskServices
    {
        ITaskDAL taskDAL;
        public TaskServices()
        {
            taskDAL = new TaskDAL();
        }

        public int CountTaskByProjectId(int id)
        {
            try
            {
                
                var count = taskDAL.CountTaskByProjectId(id);

                if (count == 0)
                    throw new Exception("Tasks not found.");

                return count;
            }
            catch (SqlException sqlEx)
            {
                // Log the SQL exception
                throw new Exception("Database error occurred while counting tasks.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while counting tasks.", ex);
            }
        }

        public bool CreateTask(TaskDTO taskDTO, int createdByUserId)
        {
            try
            {
                

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
                    var res1 = taskHistoryServices.CreateTaskHistory(taskHistoryDTO);
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

        public bool DeleteTask(TaskDTO taskDTO)
        {
            try
            {
                TaskStatusServices taskStatusServices = new TaskStatusServices();
                var cancelledTaskStatusId = taskStatusServices.GetTaskStatusByName("Cancelled").First().Id;

                
                TaskDTO existingTask = this.GetTaskById(taskDTO.Id);

                if (existingTask == null)
                    throw new Exception("Task not found.");

                existingTask.StatusId = cancelledTaskStatusId;
                existingTask.IsDeleted = true;
                existingTask.UpdatedDate = DateTime.Now;

                var res = taskDAL.UpdateTask(existingTask.ToTaskEntity());

                return res > 0;
            }
            catch (SqlException sqlEx)
            {
                // Log the SQL exception
                throw new Exception("Database error occurred while deleting task.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting task.", ex);
            }
        }

        public List<TaskDTO> GetAllTask(string kw)
        {
            try
            {
                
                var tasks = taskDAL.GetAllTasks(kw, isIncludeInActive: false);
                if (tasks == null)
                    return null;

                return tasks.Select(t => TaskDTOMapper.ToDto(t)).ToList();
            }
            catch (SqlException sqlEx)
            {
                // Log the SQL exception
                throw new Exception("Database error occurred while fetching all tasks.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching all tasks.", ex);
            }
        }

        public List<TaskForListDTO> GetAllTaskForList(string kw)
        {
            try
            {
                
                var tasks = taskDAL.GetAllTasks(kw, isIncludeInActive: false);
                if (tasks == null)
                    return null;

                return tasks.Select(t => TaskForListDTOMapper.ToDto(t)).ToList();
            }
            catch (SqlException sqlEx)
            {
                // Log the SQL exception
                throw new Exception("Database error occurred while fetching all tasks.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching all tasks.", ex);
            }
        }

        public List<TaskForListDTO> GetAllTaskForListInlcudeInActive(string kw)
        {
            try
            {
                
                var tasks = taskDAL.GetAllTasks(kw, isIncludeInActive: true);
                if (tasks == null)
                    return null;

                return tasks.Select(t => TaskForListDTOMapper.ToDto(t)).ToList();
            }
            catch (SqlException sqlEx)
            {
                // Log the SQL exception
                throw new Exception("Database error occurred while fetching all tasks.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching all tasks.", ex);
            }
        }

        public List<TaskDTO> GetAllTaskInlcudeInActive(string kw)
        {
            try
            {
                
                var tasks = taskDAL.GetAllTasks(kw, isIncludeInActive: true);
                if (tasks == null)
                    return null;

                return tasks.Select(t => TaskDTOMapper.ToDto(t)).ToList();
            }
            catch (SqlException sqlEx)
            {
                // Log the SQL exception
                throw new Exception("Database error occurred while fetching all tasks.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching all tasks.", ex);
            }
        }

        public List<TaskForListDTO> GetTaskAllForlistByProjectIdAndUserIdAndKwInlcudeInActive(int projectId, int userId, string kw)
        {
            try
            {


                List<Task> tasks = taskDAL.GetTaskByProjectIdAndUserIdAndKw(projectId, userId, kw, isIncludeInActive: true);
                if (tasks == null)
                    return null;

                return tasks.Select(t => TaskForListDTOMapper.ToDto(t)).ToList();
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

        public TaskDTO GetTaskById(int id)
        {
            try
            {

                var task = taskDAL.GetTaskById(id, isIncludeInActive: false);
                if (task == null)
                    throw new Exception("Task not found.");

                return TaskDTOMapper.ToDto(task);
            }
            catch (SqlException sqlEx)
            {
                // Log the SQL exception
                throw new Exception("Database error occurred while fetching task.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching task.", ex);
            }
        }

        public TaskDTO GetTaskByIdInlcudeInActive(int id)
        {
            try
            {
                var task = taskDAL.GetTaskById(id, isIncludeInActive: true);
                if (task == null)
                    throw new Exception("Task not found.");

                return TaskDTOMapper.ToDto(task);
            }
            catch (SqlException sqlEx)
            {
                // Log the SQL exception
                throw new Exception("Database error occurred while fetching task.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching task.", ex);
            }
        }

        public List<TaskDTO> GetTaskByProjectIdAndUserId(int projectId, int userId)
        {
            try
            {
                List<Task> tasks = taskDAL.GetTaskByProjectIdAndUserId(projectId, userId, isIncludeInActive: false);
                if (tasks == null)
                    return null;

                return tasks.Select(t => TaskDTOMapper.ToDto(t)).ToList();
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

        public List<TaskDTO> GetTaskByProjectIdAndUserIdInlcudeInActive(int projectId, int userId)
        {
            try
            {


                List<Task> tasks = taskDAL.GetTaskByProjectIdAndUserId(projectId, userId, isIncludeInActive: true);
                if (tasks == null)
                    return null;

                return tasks.Select(t => TaskDTOMapper.ToDto(t)).ToList();
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

        public List<TaskForListDTO> GetTaskForlistByProjectId(int projectId)
        {
            try
            {


                List<Task> tasks = taskDAL.GetTaskByProjectId(projectId, isIncludeInActive: false);
                if (tasks == null)
                    return null;

                return tasks.Select(t => TaskForListDTOMapper.ToDto(t)).ToList();
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

        public List<TaskForListDTO> GetTaskForlistByProjectIdAndUserId(int projectId, int userId)
        {
            try
            {
                List<Task> tasks = taskDAL.GetTaskByProjectIdAndUserId(projectId, userId, isIncludeInActive: false);
                if (tasks == null)
                    return null;

                return tasks.Select(t => TaskForListDTOMapper.ToDto(t)).ToList();
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

        public List<TaskForListDTO> GetTaskForlistByProjectIdAndUserIdInlcudeInActive(int projectId, int userId)
        {
            try
            {


                List<Task> tasks = taskDAL.GetTaskByProjectIdAndUserId(projectId, userId, isIncludeInActive: true);
                if (tasks == null)
                    return null;

                return tasks.Select(t => TaskForListDTOMapper.ToDto(t)).ToList();
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

        public List<TaskForListDTO> GetTaskForlistByProjectIdInlcudeInActive(int projectId)
        {
            try
            {
                List<Task> tasks = taskDAL.GetTaskByProjectId(projectId, isIncludeInActive: true);
                if (tasks == null)
                    return null;

                return tasks.Select(t => TaskForListDTOMapper.ToDto(t)).ToList();
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

        public List<TaskForListDTO> GetTasksForlistByProjectIdAndUserIdAndKw(int projectId, int userId, string kw)
        {
            try
            {
                List<Task> tasks = taskDAL.GetTaskByProjectIdAndUserIdAndKw(projectId, userId, kw, isIncludeInActive: false);
                if (tasks == null)
                    return null;

                return tasks.Select(t => TaskForListDTOMapper.ToDto(t)).ToList();
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
        public bool UpdateTask(TaskDTO newTaskDTO)
        {
            try
            {
                
                var task = newTaskDTO.ToTaskEntity();
                task.UpdatedDate = DateTime.Now;

                var res = taskDAL.UpdateTask(task);

                if (res > 0)
                    return true;
                else
                    throw new Exception("Task not found or update failed.");
            }
            catch (SqlException sqlEx)
            {
                // Log the SQL exception
                throw new Exception("Database error occurred while updating task.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating task.", ex);
            }

        }

        public bool UpdateTaskStatus(int taskId, TaskStatusEnum? taskStatusEnum)
        {
            try
            {
                if (taskStatusEnum == null)
                    throw new ArgumentNullException(nameof(taskStatusEnum), "Task status cannot be null.");

                

                var task = taskDAL.GetTaskById(taskId, isIncludeInActive: false);
                task.StatusId = TaskStatusEnumExtensions.ToId(taskStatusEnum);
                task.UpdatedDate = DateTime.Now;

                var res = taskDAL.UpdateTask(task);

                if (res > 0)
                    return true;
                else
                    throw new Exception("Task not found or update failed.");
            }
            catch (SqlException sqlEx)
            {
                // Log the SQL exception
                throw new Exception("Database error occurred while updating task.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating task.", ex);
            }
        }

        public Dictionary<DateTime, int> GetCompletedTaskByDate(int userId)
        {
            ITaskDAL taskDAL= new TaskDAL();
            List<Task> tasks = taskDAL.GetAllTasks("", false);

            var taskUser = taskDAL.GetTaskByUserId(userId, false).Select(t=>TaskDTOMapper.ToDto(t)).ToList();

            var grouped = taskUser
                .Where(t=>t.UpdatedDate.HasValue)
                .GroupBy(t=>t.UpdatedDate.Value.Date)
                .OrderBy(g=>g.Key)
                .ToDictionary(g=>g.Key, g=>g.Count());

            return grouped;

        }
    }
}
