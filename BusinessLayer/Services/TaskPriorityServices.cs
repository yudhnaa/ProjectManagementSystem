using DataLayer.DataAccess;
using DTOLayer.Mappers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class TaskPriorityServices
    {
        public List<DTOLayer.Models.TaskPriorityDTO> getTaskPriorities()
        {
            try
            {
                TaskPriorityDAL taskPriorityDAL = new DataLayer.DataAccess.TaskPriorityDAL();
                
                var taskPriorities = taskPriorityDAL.getTaskPriorities();
                
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
    }
}
