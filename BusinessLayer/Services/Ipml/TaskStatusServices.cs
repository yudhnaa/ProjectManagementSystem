using DataLayer.DataAccess;
using DTOLayer.Mappers;
using DTOLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class TaskStatusServices : ITaskStatusServices
    {
        public bool CreateTaskStatus(TaskStatusDTO taskStatusDTO)
        {
            try
            {
                TaskStatusDAL taskStatusDAL = new TaskStatusDAL();

                var taskStatus = taskStatusDTO.ToTaskStatusEntity();
                taskStatus.IsDeleted = false;
                taskStatus.IsActive = true;
                taskStatus.CreatedDate = DateTime.Now;
                taskStatus.UpdatedDate = null;

                var res = taskStatusDAL.CreateTaskStatus(taskStatus);

                return res;
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while adding task status.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while adding task status.", ex);
            }
        }

        public List<TaskStatusDTO> GetAllTaskStatuses(string kw)
        {
            try
            {
                TaskStatusDAL taskStatusDAL = new TaskStatusDAL();

                var taskStatuses = taskStatusDAL.GetAllTaskStatuses(kw, isIncludeInActive: false);
                if (taskStatuses == null)
                    return null;

                return taskStatuses.Select(t => t.ToDto()).ToList();
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

        public List<TaskStatusDTO> GetAllTaskStatusesInlcudeInActive(string kw)
        {
            try
            {
                TaskStatusDAL taskStatusDAL = new TaskStatusDAL();

                var taskStatuses = taskStatusDAL.GetAllTaskStatuses(kw, isIncludeInActive: true);
                if (taskStatuses == null)
                    return null;

                return taskStatuses.Select(t => t.ToDto()).ToList();
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

        public TaskStatusDTO GetById(int statusId)
        {
            try
            {
                TaskStatusDAL taskStatusDAL = new TaskStatusDAL();
                var taskStatus = taskStatusDAL.GetById(statusId, isIncludeInActive: false);

                if (taskStatus != null)
                    return taskStatus.ToDto();

                throw new Exception("Task Status not found");
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

        public TaskStatusDTO GetByIdInlcudeInActive(int statusId)
        {
            try
            {
                TaskStatusDAL taskStatusDAL = new TaskStatusDAL();
                var taskStatus = taskStatusDAL.GetById(statusId, isIncludeInActive: true);

                if (taskStatus != null)
                    return taskStatus.ToDto();

                throw new Exception("Task Status not found");
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

        public List<TaskStatusDTO> GetTaskStatusByName(string kw)
        {
            try
            {
                TaskStatusDAL taskStatusDAL = new TaskStatusDAL();
                var taskStatus = taskStatusDAL.GetTaskStatusByName(kw, isIncludeInActive: false);
                if (taskStatus == null)
                    return null;
                
                return taskStatus.Select(t => t.ToDto()).ToList();
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
        public List<TaskStatusDTO> GetTaskStatusByNameInlcudeInActive(string kw)
        {
            try
            {
                TaskStatusDAL taskStatusDAL = new TaskStatusDAL();
                var taskStatuses = taskStatusDAL.GetTaskStatusByName(kw, isIncludeInActive: true);
                if (taskStatuses == null)
                    return null;

                return taskStatuses.Select(t => t.ToDto()).ToList();
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
        public bool UpdateTaskStatus(TaskStatusDTO item)
        {
            try
            {
                TaskStatusDAL taskStatusDAL = new TaskStatusDAL();

                var taskStatus = item.ToTaskStatusEntity();
                taskStatus.UpdatedDate = DateTime.Now;

                var res = taskStatusDAL.UpdateTasktStatus(taskStatus);
                return res;
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while updating task status.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while updating task status.", ex);
            }
        }
    }
}
