using DTOLayer.Models;

namespace DTOLayer.Mappers
{
    public static class ProjectPriorityDTOMapper
    {
        public static ProjectPriorityDTO ToDto(this DataLayer.Domain.ProjectPriority projectpriority)
        {
            return new ProjectPriorityDTO
            {
                Id = projectpriority.Id,
                Name = projectpriority.Name,
                Description = projectpriority.Description,
            };
        }

        public static DataLayer.Domain.ProjectPriority ToProjectPriorityEntity(this ProjectPriorityDTO model)
        {
            return new DataLayer.Domain.ProjectPriority
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
            };
        }

    }
}

