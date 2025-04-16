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
    public class TaskStatusServices
    {
        public TaskStatusDTO GetById(int statusId)
        {
            try
            {
                TaskStatusDAL taskStatusDAL = new TaskStatusDAL();
                var taskStatus = taskStatusDAL.GetById(statusId);

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

        public List<TaskStatusDTO> getTaskStatuses()
        {
            try
            {
                TaskStatusDAL taskStatusDAL = new TaskStatusDAL();

                var taskStatuses = taskStatusDAL.getTaskStatuses();

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
    }
}
