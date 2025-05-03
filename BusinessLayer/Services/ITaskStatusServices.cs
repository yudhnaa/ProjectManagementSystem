using DTOLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public interface ITaskStatusServices
    {
        bool CreateTaskStatus(TaskStatusDTO taskStatusDTO);
        List<TaskStatusDTO> GetAllTaskStatuses(string kw);
        List<TaskStatusDTO> GetAllTaskStatusesInlcudeInActive(string kw);
        TaskStatusDTO GetById(int statusId);
        TaskStatusDTO GetByIdInlcudeInActive(int statusId);
        List<TaskStatusDTO> GetTaskStatusByName(string kw);
        List<TaskStatusDTO> GetTaskStatusByNameInlcudeInActive(string kw);
        bool UpdateTaskStatus(TaskStatusDTO item);
    }
}