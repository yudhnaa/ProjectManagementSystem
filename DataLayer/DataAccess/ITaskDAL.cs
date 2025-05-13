using DataLayer.Domain;
using System.Collections.Generic;

namespace DataLayer.DataAccess
{
    public interface ITaskDAL
    {
        int CountTaskByProjectId(int id);
        int CreateTask(Task task);
        List<Task> GetAllTasks(string kw, bool isIncludeInActive);
        Task GetTaskById(int taskId, bool isIncludeInActive);
        List<Task> GetTaskByKeyValue(string key, string value, bool isIncludeInActive);
        List<Task> GetTaskByProjectId(int projectId, bool isIncludeInActive);
        List<Task> GetTaskByProjectIdAndKw(int projectId, string kw, bool isIncludeInActive);
        List<Task> GetTaskByProjectIdAndUserId(int projectId, int userId, bool isIncludeInActive);
        List<Task> GetTaskByProjectIdAndUserIdAndKw(int projectId, int userId, string kw, bool isIncludeInActive);
        List<Task> GetTaskByUserId(int userId, bool isIncludeInActive);
        List<Task> GetTasksByKeywordAndStatus(string keyword, int statusId, bool isIncludeInActive);
        bool UpdateTask(int taskId, string key, string value, int updatedByUserId);
        int UpdateTask(Task task);
    }
}