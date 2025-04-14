using DTOLayer.Models;

namespace DTOLayer.Mappers
{
    public static class TaskPriorityDTOMapper
    {
        public static TaskPriorityDTO ToDto(this DataLayer.Domain.TaskPriority taskpriority)
        {
            return new TaskPriorityDTO
            {
                Id = taskpriority.Id,
                Name = taskpriority.Name,
                Description = taskpriority.Description,
            };
        }

        public static DataLayer.Domain.TaskPriority ToTaskPriorityEntity(this TaskPriorityDTO model)
        {
            return new DataLayer.Domain.TaskPriority
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
            };
        }

    }
}

