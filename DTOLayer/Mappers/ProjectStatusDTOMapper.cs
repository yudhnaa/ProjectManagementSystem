using DTOLayer.Models;

namespace DTOLayer.Mappers
{
    public static class ProjectStatusDTOMapper
    {
        public static ProjectStatusDTO ToDto(this DataLayer.Domain.ProjectStatus projectstatus)
        {
            return new ProjectStatusDTO
            {
                Id = projectstatus.Id,
                Name = projectstatus.Name,
                Description = projectstatus.Description,
            };
        }

        public static DataLayer.Domain.ProjectStatus ToProjectStatusEntity(this ProjectStatusDTO model)
        {
            return new DataLayer.Domain.ProjectStatus
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
            };
        }

    }
}

