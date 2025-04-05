using DTOLayer.Models;

namespace DTOLayer.Mappers
{
    public static class PermissionTypeDTOMapper
    {
        public static PermissionTypeDTO ToDto(this DataLayer.Domain.PermissionType permissiontype)
        {
            return new PermissionTypeDTO
            {
                Id = permissiontype.Id,
                Name = permissiontype.Name,
            };
        }

        public static DataLayer.Domain.PermissionType ToPermissionTypeEntity(this PermissionTypeDTO model)
        {
            return new DataLayer.Domain.PermissionType
            {
                Id = model.Id,
                Name = model.Name,
            };
        }

    }
}

