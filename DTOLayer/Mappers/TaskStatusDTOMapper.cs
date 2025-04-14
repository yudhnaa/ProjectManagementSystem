using DTOLayer.Models;

namespace DTOLayer.Mappers
{
    public static class TaskStatusDTOMapper
    {
        public static TaskStatusDTO ToDto(this DataLayer.Domain.TaskStatus taskstatus)
        {
            return new TaskStatusDTO
            {
                Id = taskstatus.Id,
                Name = taskstatus.Name,
                Description = taskstatus.Description,
            };
        }

        public static DataLayer.Domain.TaskStatus ToTaskStatusEntity(this TaskStatusDTO model)
        {
            return new DataLayer.Domain.TaskStatus
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
            };
        }

    }
}

