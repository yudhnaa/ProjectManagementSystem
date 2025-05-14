using DTOLayer.Models;

namespace DTOLayer.Mappers
{
    public static class NotificationTypeDTOMapper
    {
        public static NotificationTypeDTO ToDto(this DataLayer.Domain.NotificationType notificationtype)
        {
            return new NotificationTypeDTO
            {
                Id = notificationtype.Id,
                Name = notificationtype.Name,
                Description = notificationtype.Description,
                CreatedDate = notificationtype.CreatedDate,
                UpdatedDate = notificationtype.UpdatedDate,
            };
        }

        public static DataLayer.Domain.NotificationType ToNotificationTypeEntity(this NotificationTypeDTO model)
        {
            return new DataLayer.Domain.NotificationType
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                CreatedDate = model.CreatedDate,
                UpdatedDate = model.UpdatedDate,
            };
        }

    }
}

