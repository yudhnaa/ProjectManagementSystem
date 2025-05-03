using DataLayer.Domain;
using System.Collections.Generic;

namespace DataLayer.DataAccess
{
    public interface ITaskCommentDAL
    {
        TaskComment CreateTaskComment(TaskComment taskComment);
        List<TaskComment> GetAllTaskComments(int taskId, bool isIncludeInActive);
        void DeleteTaskComment(int commentId);
    }
}