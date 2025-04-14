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
