using DTOLayer.Models;

namespace BusinessLayer.Services
{
    public interface ITaskHistoryServices
    {
        int CreateTaskHistory(TaskHistoryDTO taskHistoryDTO);
    }
}