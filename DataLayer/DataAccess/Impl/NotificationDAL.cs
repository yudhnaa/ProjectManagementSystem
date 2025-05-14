using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataAccess.Impl
{
    public class NotificationDAL : INotificationDAL
    {
        public List<Notification> GetNotificationByUserId(int userId)
        {
            try
            {
                using (var context = new ProjectManagementSystemDBContext())
                {
                    var query = context.Notifications
                        .Where(n => n.UserId == userId);

                    return query.ToList();
                }
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

        public int CreateNotification(Notification notification)
        {
            try
            {
                using (var context = new ProjectManagementSystemDBContext())
                {
                    context.Notifications.Add(notification);
                    var res = context.SaveChanges();

                    return res;
                }
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
