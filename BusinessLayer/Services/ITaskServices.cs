using DataLayer.Domain;
using DataLayer.Domain;
using DataLayer.EnumObjects;
using DTOLayer.Models;
using System;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public interface ITaskServices
    {
        int CountTaskByProjectId(int id);
        Dictionary<string, int> CountTaskByStatusAndUserId(int userId);
        Dictionary<string, int> CountTaskByProjectAndUserId(int userId);

        bool CreateTask(TaskDTO taskDTO, int createdByUserId, NotificationDTO notification);
        bool DeleteTask(TaskDTO taskDTO);
        List<TaskDTO> GetAllTask(string kw);
        List<TaskForListDTO> GetAllTaskForList(string kw);
        List<TaskForListDTO> GetAllTaskForListInlcudeInActive(string kw);
        List<TaskDTO> GetAllTaskInlcudeInActive(string kw);
        TaskDTO GetTaskById(int id);
        TaskDTO GetTaskByIdInlcudeInActive(int id);
        List<TaskDTO> GetTaskByProjectIdAndUserId(int projectId, int userId);
        List<TaskDTO> GetTaskByProjectIdAndUserIdInlcudeInActive(int projectId, int userId);
        List<TaskForListDTO> GetTaskForlistByProjectId(int projectId);
        List<TaskForListDTO> GetTaskForlistByProjectIdInlcudeInActive(int projectId);
        List<TaskForListDTO> GetTaskForlistByProjectIdAndUserId(int projectId, int userId);
        List<TaskForListDTO> GetTaskForlistByProjectIdAndUserIdInlcudeInActive(int projectId, int userId);
        List<TaskForListDTO> GetTasksForlistByProjectIdAndUserIdAndKw(int projectId, int userId, string kw);
        List<TaskForListDTO> GetTaskAllForlistByProjectIdAndUserIdAndKwInlcudeInActive(int projectId, int userId, string kw);
        List<TaskForListDTO> GetTasksForlistByProjectIdAndKw(int projectId, string kw);
        List<TaskForListDTO> GetTaskAllForlistByProjectIdAndUserIdAndKwInlcudeInActive(int projectId, string kw);
        bool UpdateTask(TaskDTO newTaskDTO);
        bool UpdateTaskStatus(int taskId, TaskStatusEnum? taskStatusEnum);
        bool UpdateTaskFromGanttStatus(TaskForGanttChartDTO ganttTask);
        List<TaskForGanttChartDTO> GetTaskByProjectIdWithDependencies(int projectId, int userId);
        Dictionary<DateTime, int> GetCompletedTaskByDate(int userId);
    }
}