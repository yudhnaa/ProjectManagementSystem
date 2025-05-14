using DataLayer.Domain;
using System.Collections.Generic;

namespace DataLayer.DataAccess.Impl
{
    public interface ITaskHelpRequestDAL
    {
        int CreateTaskHelpRequest(TaskHelpRequest request);
        List<TaskHelpRequest> GetTaskHelpRequestById(int taskId, int userId);
        List<TaskHelpRequest> GetTaskHelpRequestByUserId(int userId);
    }
}