using DTOLayer.Models;

namespace DTOLayer.Mappers
{
    public static class TaskForListDTOMapper
    {
        public static TaskForListDTO ToDto(this DataLayer.Domain.Task task)
        {
            return new TaskForListDTO
            {
                Id = task.Id,
                Code = task.Code,
                Name = task.Name,
                StatusId = task.StatusId,
                StartDate = task.StartDate,
                CreatedBy = task.CreatedBy,
            };
        }

        public static DataLayer.Domain.Task ToTaskEntity(this TaskForListDTO model)
        {
            return new DataLayer.Domain.Task
            {
                Id = model.Id,
                Code = model.Code,
                Name = model.Name,
                StatusId = model.StatusId,
                StartDate = model.StartDate,
                CreatedBy = model.CreatedBy,
            };
        }

    }
}

