using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataAccess.Impl
{
    public class TaskHelpRequestDAL : ITaskHelpRequestDAL
    {
        public int CreateTaskHelpRequest(TaskHelpRequest request)
        {
            // create a new task help request

            try
            {
                NotificationDAL notificationDAL = new NotificationDAL();

                using (var context = new ProjectManagementSystemDBContext())
                {
                    context.TaskHelpRequests.Add(request);

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

        public List<TaskHelpRequest> GetTaskHelpRequestById(int taskId, int userId)
        {
            // get a task help request by id
            try
            {
                using (var context = new ProjectManagementSystemDBContext())
                {
                    var query = context.TaskHelpRequests
                        .Where(n => n.TaskId == taskId && (n.RequestedBy == userId || n.RequestedTo == userId));

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
    }
}
