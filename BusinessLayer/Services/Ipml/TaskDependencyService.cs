using DataLayer.DataAccess;
using DataLayer.DataAccess.Impl;
using DTOLayer.Mappers;
using DTOLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class TaskDependencyService : ITaskDependencyService
    {
        private readonly ITaskDependencyDAL taskDependencyDAL;

        public TaskDependencyService()
        {
            this.taskDependencyDAL = new TaskDependencyDAL();
        }

        public List<TaskDependencyDTO> GetDependentByTaskIds(int taskId)
        {
            var taskDependencies = taskDependencyDAL.GetDependentTaskIds(taskId, false);
            return taskDependencies.Select(td => td.ToDto()).ToList();
        }

        public List<TaskDependencyDTO> GetDependentByTaskIdsIncludeInActive(int taskId)
        {
            var taskDependencies = taskDependencyDAL.GetDependentTaskIds(taskId, true);
            return taskDependencies.Select(td => td.ToDto()).ToList();
        }
    }
}
