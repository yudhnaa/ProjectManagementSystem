using DTOLayer.Models;

namespace DTOLayer.Mappers
{
    public static class PermissionDTOMapper
    {
        public static PermissionDTO ToDto(this DataLayer.Domain.Permission permission)
        {
            return new PermissionDTO
            {
                Id = permission.Id,
                Name = permission.Name,
                PermissionTypeId = permission.PermissionTypeId,
            };
        }

        public static DataLayer.Domain.Permission ToPermissionEntity(this PermissionDTO model)
        {
            return new DataLayer.Domain.Permission
            {
                Id = model.Id,
                Name = model.Name,
                PermissionTypeId = model.PermissionTypeId,
            };
        }

    }
}

