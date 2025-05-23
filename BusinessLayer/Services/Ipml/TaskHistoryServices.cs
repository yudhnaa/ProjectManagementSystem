﻿using DataLayer.DataAccess;
using DataLayer.Domain;
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
    public class TaskHistoryServices : ITaskHistoryServices
    {
        public int CreateTaskHistory(TaskHistoryDTO taskHistoryDTO)
        {
            try
            {
                TaskHistoryDAL taskHistoryDAL = new TaskHistoryDAL();

                var taskHistory = taskHistoryDTO.ToTaskHistoryEntity();
                taskHistory.ChangedDate = DateTime.Now;

                return taskHistoryDAL.CreateTaskHistory(taskHistory);
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
