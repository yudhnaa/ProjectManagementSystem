using DataLayer.DataAccess;
using DataLayer.DataAccess.Impl;
using DataLayer.Domain;
using DTOLayer.Mappers;
using DTOLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Ipml
{
    public class TaskCommentServices : ITaskCommentServices
    {
        private readonly ITaskCommentDAL taskCommentDAL;
        public TaskCommentServices()
        {
            this.taskCommentDAL = new TaskCommentDAL();
        }

        public TaskCommentDTO CreateTaskComment(TaskCommentDTO taskCommentDTO)
        {
            throw new NotImplementedException();
            //try
            //{
            //    TaskComment taskComment = new TaskComment()
            //    {
            //        Comment = taskCommentDTO.Comment,
            //        CreatedDate = DateTime.Now,
            //        IsDeleted = false,
            //        TaskId = taskCommentDTO.TaskId,
            //        UserId = taskCommentDTO.UserId
            //    };
            //    return taskCommentDAL.CreateTaskComment(taskComment);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        public List<TaskCommentDTO> GetAllTaskCommentsById(int taskId)
        {
            if (taskId <= 0)
                throw new ArgumentException("Task ID must be greater than zero.");

            try
            {
                List<TaskComment> taskComments = taskCommentDAL.GetAllTaskComments(taskId, isIncludeInActive: false);

                if (taskComments == null || taskComments.Count == 0)
                    return null;

                return taskComments.Select(tc => tc.ToDto()).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TaskCommentDTO> GetAllTaskCommentsByIdIncludeInActive(int taskId)
        {
            if (taskId <= 0)
                throw new ArgumentException("Task ID must be greater than zero.");

            try
            {
                List<TaskComment> taskComments = taskCommentDAL.GetAllTaskComments(taskId, isIncludeInActive: true);

                if (taskComments == null || taskComments.Count == 0)
                    return null;

                return taskComments.Select(tc => tc.ToDto()).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteTaskComment(int commentId)
        {
            throw new NotImplementedException();
            //    try
            //    {
            //        taskCommentDAL.DeleteTaskComment(commentId);
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
        }
    }
}
