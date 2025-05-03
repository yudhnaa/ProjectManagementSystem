using DataLayer.Domain;
using System.Collections.Generic;

namespace DataLayer.DataAccess
{
    public interface ITaskStatusDAL
    {
        bool CreateTaskStatus(TaskStatus taskStatus);
        TaskStatus GetById(int statusId, bool isIncludeInActive);
        List<TaskStatus> GetTaskStatusByName(string kw, bool isIncludeInActive);
        List<TaskStatus> GetAllTaskStatuses(string kw, bool isIncludeInActive);
        bool UpdateTasktStatus(TaskStatus taskStatus);
    }
}