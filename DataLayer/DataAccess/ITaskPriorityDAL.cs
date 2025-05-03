using DataLayer.Domain;
using System.Collections.Generic;

namespace DataLayer.DataAccess
{
    public interface ITaskPriorityDAL
    {
        bool CreateTaskPriorities(TaskPriority taskPriorities);
        TaskPriority GetById(int priorityId, bool isIncludeInActive);
        List<TaskPriority> GetAllTaskPriorities(string kw, bool isIncludeInActive);
        bool UpdateTasktPriorities(TaskPriority taskPriorities);
    }
}