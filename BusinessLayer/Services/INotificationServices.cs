using DTOLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services.Ipml
{
    public interface INotificationServices
    {
        void AcceptProjectInvite(int projectId, int userId);
        void AcceptTaskHelpRequest(int taskId, int userId);
        List<NotificationDTO> GetNotificationByUserId(int userId);
        bool CreateNotification(NotificationDTO notification);
    }
}