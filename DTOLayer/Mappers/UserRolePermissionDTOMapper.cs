using DTOLayer.Models;

namespace DTOLayer.Mappers
{
    public static class UserRolePermissionDTOMapper
    {
        public static UserRolePermissionDTO ToDto(this DataLayer.Domain.UserRolePermission userrolepermission)
        {
            return new UserRolePermissionDTO
            {
                Id = userrolepermission.Id,
                UserRoleId = userrolepermission.UserRoleId,
                PermissionId = userrolepermission.PermissionId,
            };
        }

        public static DataLayer.Domain.UserRolePermission ToUserRolePermissionEntity(this UserRolePermissionDTO model)
        {
            return new DataLayer.Domain.UserRolePermission
            {
                Id = model.Id,
                UserRoleId = model.UserRoleId,
                PermissionId = model.PermissionId,
            };
        }

    }
}

