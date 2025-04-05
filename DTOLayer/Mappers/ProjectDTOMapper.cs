using DTOLayer.Mappers;
using DTOLayer.Models;
using System.Linq;

namespace DTOLayer
{
    public static class ProjectDTOMapper
    {
        public static ProjectDTO ToDto(this DataLayer.Domain.Project project)
        {
            return new ProjectDTO
            {
                Id = project.Id,
                Name = project.Name,
                ProjectCode = project.ProjectCode,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                Budget = project.Budget,
                StatusId = project.StatusId,
                ManagerId = project.ManagerId,
                PriorityId = project.PriorityId,
                PercentComplete = project.PercentComplete,
                IsDeleted = project.IsDeleted,
                CreatedBy = project.CreatedBy,
                CreatedDate = project.CreatedDate,
                UpdatedDate = project.UpdatedDate,
                Tasks = project.Tasks?.Select(t => t.ToDto()).ToList(),
            };
        }

        public static DataLayer.Domain.Project ToProjectEntity(this ProjectDTO model)
        {
            return new DataLayer.Domain.Project
            {
                Id = model.Id,
                Name = model.Name,
                ProjectCode = model.ProjectCode,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Budget = model.Budget,
                StatusId = model.StatusId,
                ManagerId = model.ManagerId,
                PriorityId = model.PriorityId,
                PercentComplete = model.PercentComplete,
                IsDeleted = model.IsDeleted,
                CreatedBy = model.CreatedBy,
                CreatedDate = model.CreatedDate,
                UpdatedDate = model.UpdatedDate,
                Tasks = model.Tasks?.Select(t => t.ToTaskEntity()).ToList(),
            };
        }

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
                Task1 = task.Task1?.ToDto()
            };
        }

        public static DataLayer.Domain.Task ToTaskEntity(this TaskDTO taskDto)
        {
            return new DataLayer.Domain.Task
            {
                Id = taskDto.Id,
                Code = taskDto.Code,
                Name = taskDto.Name,
                Description = taskDto.Description,
                ProjectId = taskDto.ProjectId,
                AssignedUserId = taskDto.AssignedUserId,
                StatusId = taskDto.StatusId,
                PriorityId = taskDto.PriorityId,
                StartDate = taskDto.StartDate,
                DueDate = taskDto.DueDate,
                EstimatedHours = taskDto.EstimatedHours,
                ActualHours = taskDto.ActualHours,
                PercentComplete = taskDto.PercentComplete,
                ParentTaskId = taskDto.ParentTaskId,
                LastStatusChangeDate = taskDto.LastStatusChangeDate,
                BlockReason = taskDto.BlockReason,
                IsDeleted = taskDto.IsDeleted,
                CreatedBy = taskDto.CreatedBy,
                CreatedDate = taskDto.CreatedDate,
                UpdatedDate = taskDto.UpdatedDate,
                Tasks1 = taskDto.Tasks1?.Select(t => t.ToTaskEntity()).ToList(),
                Task1 = taskDto.Task1?.ToTaskEntity()
            };
        }
    }
}

