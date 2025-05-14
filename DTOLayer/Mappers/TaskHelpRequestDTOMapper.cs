using DTOLayer.Models;

namespace DTOLayer.Mappers
{
    public static class TaskHelpRequestDTOMapper
    {
        public static TaskHelpRequestDTO ToDto(this DataLayer.Domain.TaskHelpRequest taskhelprequest)
        {
            return new TaskHelpRequestDTO
            {
                Id = taskhelprequest.Id,
                TaskId = taskhelprequest.TaskId,
                RequestedBy = taskhelprequest.RequestedBy,
                RequestedTo = taskhelprequest.RequestedTo,
                RequestMessage = taskhelprequest.RequestMessage,
                IsResolved = taskhelprequest.IsResolved,
                ResolvedDate = taskhelprequest.ResolvedDate,
                CreatedDate = taskhelprequest.CreatedDate,
                UpdatedDate = taskhelprequest.UpdatedDate,
            };
        }

        public static DataLayer.Domain.TaskHelpRequest ToTaskHelpRequestEntity(this TaskHelpRequestDTO model)
        {
            return new DataLayer.Domain.TaskHelpRequest
            {
                Id = model.Id,
                TaskId = model.TaskId,
                RequestedBy = model.RequestedBy,
                RequestedTo = model.RequestedTo,
                RequestMessage = model.RequestMessage,
                IsResolved = model.IsResolved,
                ResolvedDate = model.ResolvedDate,
                CreatedDate = model.CreatedDate,
                UpdatedDate = model.UpdatedDate,
            };
        }

    }
}

