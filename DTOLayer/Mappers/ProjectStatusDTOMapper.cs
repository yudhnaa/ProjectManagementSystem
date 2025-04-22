using DTOLayer.Models;

namespace DTOLayer.Mappers
{
    public static class ProjectStatusDTOMapper
    {
        public static taskStatusDTO ToDto(this DataLayer.Domain.ProjectStatus projectstatus)
        {
            return new taskStatusDTO
            {
                Id = projectstatus.Id,
                Name = projectstatus.Name,
                Description = projectstatus.Description,
            };
        }

        public static DataLayer.Domain.ProjectStatus ToProjectStatusEntity(this taskStatusDTO model)
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

