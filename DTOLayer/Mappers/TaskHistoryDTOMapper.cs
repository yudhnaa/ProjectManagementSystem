using DTOLayer.Models;

namespace DTOLayer.Mappers
{
    public static class TaskHistoryDTOMapper
    {
        public static TaskHistoryDTO ToDto(this DataLayer.Domain.TaskHistory taskhistory)
        {
            return new TaskHistoryDTO
            {
                Id = taskhistory.Id,
                TaskId = taskhistory.TaskId,
                FieldChanged = taskhistory.FieldChanged,
                OldValue = taskhistory.OldValue,
                NewValue = taskhistory.NewValue,
                ChangedBy = taskhistory.ChangedBy,
            };
        }

        public static DataLayer.Domain.TaskHistory ToTaskHistoryEntity(this TaskHistoryDTO model)
        {
            return new DataLayer.Domain.TaskHistory
            {
                Id = model.Id,
                TaskId = model.TaskId,
                FieldChanged = model.FieldChanged,
                OldValue = model.OldValue,
                NewValue = model.NewValue,
                ChangedBy = model.ChangedBy,
            };
        }

    }
}

