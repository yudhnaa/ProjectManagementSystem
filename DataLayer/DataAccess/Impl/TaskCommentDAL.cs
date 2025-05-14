using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataAccess.Impl
{
    public class TaskCommentDAL : ITaskCommentDAL
    {
        public int CreateTaskComment(TaskComment taskComment)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    dbContext.TaskComments.Add(taskComment);

                    var res = dbContext.SaveChanges();

                    return res;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public List<TaskComment> GetAllTaskComments(int taskId, bool isIncludeInActive)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    return dbContext.TaskComments.Where
                        (
                            tc => tc.TaskId == taskId
                        //&& (isIncludeInActive || tc.IsDeleted == false)
                        ).ToList();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public void DeleteTaskComment(int commentId)
        {
            throw new NotImplementedException();
            //using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            //{
            //    try
            //    {
            //        var comment = dbContext.TaskComments.Find(commentId);
            //        if (comment != null)
            //        {
            //            dbContext.TaskComments.Remove(comment);
            //            dbContext.SaveChanges();
            //        }
            //    }
            //    catch (SqlException ex)
            //    {
            //        throw ex;
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
            //}
        }
    }
}
