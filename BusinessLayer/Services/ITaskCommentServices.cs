using DTOLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public interface ITaskCommentServices
    {
        TaskCommentDTO CreateTaskComment(TaskCommentDTO taskCommentDTO);
        void DeleteTaskComment(int commentId);
        List<TaskCommentDTO> GetAllTaskCommentsById(int taskId);
        List<TaskCommentDTO> GetAllTaskCommentsByIdIncludeInActive(int taskId);
    }
}