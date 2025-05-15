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
    public class TaskHelpRequestServices : ITaskHelpRequestServices
    {
        private readonly ITaskHelpRequestDAL taskHelpRequestDAL = new TaskHelpRequestDAL();

        public bool CreateTaskHelpRequest(TaskHelpRequestDTO request, NotificationDTO notification)
        {
            try
            {
                var taskHelpRequest = request.ToTaskHelpRequestEntity();
                taskHelpRequest.IsResolved = false;
                taskHelpRequest.ResolvedDate = null;
                taskHelpRequest.CreatedDate = DateTime.Now;
                taskHelpRequest.UpdatedDate = null;

                var res = taskHelpRequestDAL.CreateTaskHelpRequest(taskHelpRequest);

                // Create a notification for the user
                INotificationDAL notificationDAL = new NotificationDAL();
                var noti = notification.ToNotificationEntity();
                noti.CreatedDate = DateTime.Now;
                noti.IsRead = false;

                var res1 = notificationDAL.CreateNotification(noti);

                return res > 0 && res1 > 0;
            }
            catch (SqlException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public List<TaskHelpRequestDTO> GetTaskHelpRequestById(int taskId, int userId)
        {
            try
            {
                var requests = taskHelpRequestDAL.GetTaskHelpRequestById(taskId, userId);
                return requests.Select(r => r.ToDto()).ToList();
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

        public List<TaskHelpRequestDTO> GetTaskHelpRequestByUserId(int userId)
        {
            try
            {
                var requests = taskHelpRequestDAL.GetTaskHelpRequestByUserId(userId);
                return requests.Select(r => r.ToDto()).ToList();
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
    }
}
