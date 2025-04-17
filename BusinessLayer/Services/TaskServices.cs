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

        public TaskDTO GetTaskById(int id)
        {
            try
            {
                TaskDAL taskDAL = new TaskDAL();
                var task = taskDAL.GetTaskById(id);

                if (task == null)
                    throw new Exception("Task not found.");

                return task.ToDto();
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

        public bool UpdateTask(TaskDTO newTaskDTO)
        {
            try
            {
                TaskDAL taskDAL = new TaskDAL();
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

        public List<TaskDTO> GetTaskByKw(string kw, int v)
        {
            try
            {
                TaskDAL taskDAL = new TaskDAL();
                var tasks = taskDAL.GetTaskByKw(kw, v);

                if (tasks == null)
                    throw new Exception("Tasks not found.");

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

        public int CountTaskByProjectId(int id)
        {
            try
            {
                TaskDAL taskDAL = new TaskDAL();
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
    }
}
