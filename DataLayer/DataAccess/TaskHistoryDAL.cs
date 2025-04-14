using DataLayer.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataAccess
{
    public class TaskHistoryDAL
    {
        public int addTaskHistory(TaskHistory taskHistory)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    dbContext.TaskHistories.Add(taskHistory);
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
    }
}
