using DTOLayer.Models;

namespace DTOLayer.Mappers
{
    public static class NotificationDTOMapper
    {
        public static NotificationDTO ToDto(this DataLayer.Domain.Notification notification)
        {
            return new NotificationDTO
            {
                Id = notification.Id,
                UserId = notification.UserId,
                Title = notification.Title,
                Message = notification.Message,
                NotificationTypeId = notification.NotificationTypeId,
                IsRead = notification.IsRead,
                CreatedDate = notification.CreatedDate,
            };
        }

        public static DataLayer.Domain.Notification ToNotificationEntity(this NotificationDTO model)
        {
            return new DataLayer.Domain.Notification
            {
                Id = model.Id,
                UserId = model.UserId,
                Title = model.Title,
                Message = model.Message,
                NotificationTypeId = model.NotificationTypeId,
                IsRead = model.IsRead,
                CreatedDate = model.CreatedDate,
            };
        }

    }
}

