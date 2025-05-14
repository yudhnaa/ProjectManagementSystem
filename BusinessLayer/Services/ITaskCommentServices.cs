using DTOLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public interface ITaskCommentServices
    {
        bool CreateTaskComment(TaskCommentDTO taskCommentDTO, NotificationDTO notification);
        void DeleteTaskComment(int commentId);
        List<TaskCommentDTO> GetAllTaskCommentsById(int taskId);
        List<TaskCommentDTO> GetAllTaskCommentsByIdIncludeInActive(int taskId);
    }
}