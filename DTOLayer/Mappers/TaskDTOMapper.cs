using DTOLayer.Models;
using System.Linq;

namespace DTOLayer.Mappers
{
    public static class TaskDTOMapper
    {
        public static TaskDTO ToDto(this DataLayer.Domain.Task task)
        {
            return new TaskDTO
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
                LastStatusChangeDate = task.LastStatusChangeDate,
                BlockReason = task.BlockReason,
                IsDeleted = task.IsDeleted,
                CreatedBy = task.CreatedBy,
                CreatedDate = task.CreatedDate,
                UpdatedDate = task.UpdatedDate,
                Tasks1 = task.Tasks1?.Select(t => t.ToDto()).ToList(),
                Task1 = task.Task1?.ToDto(),
            };
        }

        public static DataLayer.Domain.Task ToTaskEntity(this TaskDTO model)
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
                LastStatusChangeDate = model.LastStatusChangeDate,
                BlockReason = model.BlockReason,
                IsDeleted = model.IsDeleted,
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                UpdatedDate = model.UpdatedDate,
                Tasks1 = model.Tasks1?.Select(t => t.ToTaskEntity()).ToList(),
                Task1 = model.Task1?.ToTaskEntity(),
            };
        }
    }
}

