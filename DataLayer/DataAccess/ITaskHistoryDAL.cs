using DataLayer.Domain;

namespace DataLayer.DataAccess
{
    public interface ITaskHistoryDAL
    {
        int CreateTaskHistory(TaskHistory taskHistory);
    }
}