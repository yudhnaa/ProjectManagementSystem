using DataLayer.Domain;
using System.Collections.Generic;

namespace DataLayer.DataAccess
{
    public interface ITaskCommentDAL
    {
        int CreateTaskComment(TaskComment taskComment);
        List<TaskComment> GetAllTaskComments(int taskId, bool isIncludeInActive);
        void DeleteTaskComment(int commentId);
    }
}