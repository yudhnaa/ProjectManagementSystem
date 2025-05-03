using DTOLayer.Models;

namespace DTOLayer.Mappers
{
    public static class UserRoleDTOMapper
    {
        public static UserRoleDTO ToDto(this DataLayer.Domain.UserRole userrole)
        {
            return new UserRoleDTO
            {
                Id = userrole.Id,
                Name = userrole.Name,
                Description = userrole.Description,
            };
        }

        public static DataLayer.Domain.UserRole ToUserRoleEntity(this UserRoleDTO model)
        {
            return new DataLayer.Domain.UserRole
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
            };
        }

    }
}

