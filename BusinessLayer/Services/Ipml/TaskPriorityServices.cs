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
    public class TaskPriorityServices : ITaskPriorityServices
    {
        public bool CreateTaskPriority(TaskPriorityDTO projectPriorityDTO)
        {
            try
            {
                TaskPriorityDAL projectPriorityDAL = new TaskPriorityDAL();

                var projectPriority = projectPriorityDTO.ToTaskPriorityEntity();
                projectPriority.IsDeleted = false;
                projectPriority.IsActive = true;
                projectPriority.CreatedDate = DateTime.Now;
                projectPriority.UpdatedDate = null;

                var res = projectPriorityDAL.CreateTaskPriorities(projectPriority);

                return res;
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while adding task priorities.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while adding task priorities.", ex);
            }
        }

        public List<TaskPriorityDTO> GetAllTaskPriorities(string kw)
        {
            try
            {
                TaskPriorityDAL taskPriorityDAL = new DataLayer.DataAccess.TaskPriorityDAL();

                var taskPriorities = taskPriorityDAL.GetAllTaskPriorities(kw, isIncludeInActive: false);
                if (taskPriorities == null)
                    return null;

                return taskPriorities.Select(t => t.ToDto()).ToList();
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

        public List<TaskPriorityDTO> GetAllTaskPrioritiesInlcudeInActive(string kw)
        {
            try
            {
                TaskPriorityDAL taskPriorityDAL = new DataLayer.DataAccess.TaskPriorityDAL();

                var taskPriorities = taskPriorityDAL.GetAllTaskPriorities(kw, isIncludeInActive: true);
                if (taskPriorities == null)
                    return null;

                return taskPriorities.Select(t => t.ToDto()).ToList();
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

        public TaskPriorityDTO GetTaskPriorityById(int priorityId)
        {
            try
            {
                TaskPriorityDAL taskPriorityDAL = new DataLayer.DataAccess.TaskPriorityDAL();

                var taskPriority = taskPriorityDAL.GetById(priorityId, isIncludeInActive: false);
                if (taskPriority != null)
                    return taskPriority.ToDto();

                throw new Exception("Task Priority not found");
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

        public TaskPriorityDTO GetTaskPriorityByIdInlcudeInActive(int priorityId)
        {
            try
            {
                TaskPriorityDAL taskPriorityDAL = new DataLayer.DataAccess.TaskPriorityDAL();

                var taskPriority = taskPriorityDAL.GetById(priorityId, isIncludeInActive: true);

                if (taskPriority == null)
                    throw new Exception("Task Priority not found");

                return taskPriority.ToDto();

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
        public bool UpdateTaskPriority(TaskPriorityDTO item)
        {
            try
            {
                TaskPriorityDAL projectPriorityDAL = new TaskPriorityDAL();

                var projectPriority = item.ToTaskPriorityEntity();
                projectPriority.UpdatedDate = DateTime.Now;

                var res = projectPriorityDAL.UpdateTasktPriorities(projectPriority);
                return res;
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while updating task priorities.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while updating task priorities.", ex);
            }
        }
    }
}
