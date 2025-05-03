using DTOLayer.Models;

namespace DTOLayer.Mappers
{
    public static class TaskCommentDTOMapper
    {
        public static TaskCommentDTO ToDto(this DataLayer.Domain.TaskComment taskcomment)
        {
            return new TaskCommentDTO
            {
                Id = taskcomment.Id,
                TaskId = taskcomment.TaskId,
                UserId = taskcomment.UserId,
                CommentText = taskcomment.CommentText,
                CreatedDate = taskcomment.CreatedDate,
            };
        }

        public static DataLayer.Domain.TaskComment ToTaskCommentEntity(this TaskCommentDTO model)
        {
            return new DataLayer.Domain.TaskComment
            {
                Id = model.Id,
                TaskId = model.TaskId,
                UserId = model.UserId,
                CommentText = model.CommentText,
                CreatedDate = model.CreatedDate,
            };
        }

    }
}

