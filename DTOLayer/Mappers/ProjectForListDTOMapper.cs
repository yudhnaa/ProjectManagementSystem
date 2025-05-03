using DTOLayer.Models;

namespace DTOLayer.Mappers
{
    public static class ProjectForListDTOMapper
    {
        public static ProjectForListDTO ToDto(this DataLayer.Domain.Project project)
        {
            return new ProjectForListDTO
            {
                Id = project.Id,
                Name = project.Name,
                ProjectCode = project.ProjectCode,
                StartDate = project.StartDate,
                StatusId = project.StatusId,
                PriorityId = project.PriorityId,
                PercentComplete = project.PercentComplete,
                IsDeleted = project.IsDeleted,
            };
        }

        public static DataLayer.Domain.Project ToProjectEntity(this ProjectForListDTO model)
        {
            return new DataLayer.Domain.Project
            {
                Id = model.Id,
                Name = model.Name,
                ProjectCode = model.ProjectCode,
                StartDate = model.StartDate,
                StatusId = model.StatusId,
                PriorityId = model.PriorityId,
                PercentComplete = model.PercentComplete,
                IsDeleted = model.IsDeleted,
            };
        }

    }
}

