using DTOLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services.Ipml
{
    public interface ITaskHelpRequestServices
    {
        bool CreateTaskHelpRequest(TaskHelpRequestDTO request, NotificationDTO notification);
        List<TaskHelpRequestDTO> GetTaskHelpRequestById(int taskId, int userId);
        List<TaskHelpRequestDTO> GetTaskHelpRequestByUserId(int userId);

    }
}