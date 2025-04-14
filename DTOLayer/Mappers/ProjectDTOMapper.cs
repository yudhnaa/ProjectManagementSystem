using DTOLayer.Models;

namespace DTOLayer.Mappers
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
            };
        }

    }
}

