using DataLayer.Domain;
using System.Collections.Generic;

namespace DataLayer.DataAccess.Impl
{
    public interface INotificationDAL
    {
        void AcceptProjectInvite(int projectId, int userId);
        void AcceptTaskHelpRequest(int taskId, int userId);
        List<Notification> GetNotificationByUserId(int userId);
        int CreateNotification(Notification notification);
    }
}