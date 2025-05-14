using DataLayer.DataAccess.Impl;
using DataLayer.Domain;
using DTOLayer.Mappers;
using DTOLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Ipml
{
    public class NotificationServices : INotificationServices
    {

        private readonly NotificationDAL notificationDAL;

        public NotificationServices()
        {
            notificationDAL = new NotificationDAL();
        }

        public List<NotificationDTO> GetNotificationByUserId(int userId)
        {
            try
            {
                var res = notificationDAL.GetNotificationByUserId(userId);

                if (res == null || res.Count == 0)
                    return null;

                return res.Select(n => n.ToDto()).ToList();
            }
            catch (SqlException sqlEx)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool CreateNotification(NotificationDTO notification)
        {
            try
            {
                var noti = notification.ToNotificationEntity();
                noti.CreatedDate = DateTime.Now;
                noti.IsRead = false;

                var res1 = notificationDAL.CreateNotification(noti);

                return res1 > 0;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AcceptTaskHelpRequest(int taskId, int userId)
        {
            // Accept the task help request
            // This is a placeholder for the actual implementation
        }

        public void AcceptProjectInvite(int projectId, int userId)
        {
            // Accept the project invitation
            // This is a placeholder for the actual implementation
        }
    }
}
