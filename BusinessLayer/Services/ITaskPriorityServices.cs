using DTOLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public interface ITaskPriorityServices
    {
        bool CreateTaskPriority(TaskPriorityDTO projectPriorityDTO);
        List<TaskPriorityDTO> GetAllTaskPriorities(string kw);
        List<TaskPriorityDTO> GetAllTaskPrioritiesInlcudeInActive(string kw);
        TaskPriorityDTO GetTaskPriorityById(int priorityId);
        TaskPriorityDTO GetTaskPriorityByIdInlcudeInActive(int priorityId);
        bool UpdateTaskPriority(TaskPriorityDTO item);
    }
}