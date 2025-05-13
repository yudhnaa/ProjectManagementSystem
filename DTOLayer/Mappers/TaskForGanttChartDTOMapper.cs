using DTOLayer.Models;

namespace DTOLayer.Mappers
{
    public static class TaskForGanttChartDTOMapper
    {
        public static TaskForGanttChartDTO ToDto(this DataLayer.Domain.Task task, string assignedUserName)
        {
            return new TaskForGanttChartDTO
            {
                Id = task.Id,
                Code = task.Code,
                Name = task.Name,
                Description = task.Description,
                ProjectId = task.ProjectId,
                AssignedUserId = task.AssignedUserId,
                StatusId = task.StatusId,
                PriorityId = task.PriorityId,
                StartDate = task.StartDate,
                DueDate = task.DueDate,
                EstimatedHours = task.EstimatedHours,
                ActualHours = task.ActualHours,
                PercentComplete = task.PercentComplete,
                ParentTaskId = task.ParentTaskId,
                AssignedUserName = assignedUserName,
                //TaskDependencies = task.Tasks1,
                IsDeleted = task.IsDeleted,
            };
        }

        public static DataLayer.Domain.Task ToTaskEntity(this TaskForGanttChartDTO model)
        {
            return new DataLayer.Domain.Task
            {
                Id = model.Id,
                Code = model.Code,
                Name = model.Name,
                Description = model.Description,
                ProjectId = model.ProjectId,
                AssignedUserId = model.AssignedUserId,
                StatusId = model.StatusId,
                PriorityId = model.PriorityId,
                StartDate = model.StartDate,
                DueDate = model.DueDate,
                EstimatedHours = model.EstimatedHours,
                ActualHours = model.ActualHours,
                PercentComplete = model.PercentComplete,
                ParentTaskId = model.ParentTaskId,
                BlockReason = model.AssignedUserName,
                //Tasks1 = model.TaskDependencies,
                IsDeleted = model.IsDeleted,
            };
        }

    }
}

