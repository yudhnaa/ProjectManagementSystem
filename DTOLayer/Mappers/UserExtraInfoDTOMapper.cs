using DTOLayer.Models;

namespace DTOLayer.Mappers
{
    public static class UserExtraInfoDTOMapper
    {
        public static UserExtraInfoDTO ToDto(this DataLayer.Domain.User user)
        {
            return new UserExtraInfoDTO
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                Avatar = user.Avatar,
                UserRoleId = user.UserRoleId,
                PositionId = user.PositionId,
                DepartmentId = user.DepartmentId,
                LastLogin = user.LastLogin,
                IsActive = user.IsActive,
            };
        }

        public static DataLayer.Domain.User ToUserEntity(this UserExtraInfoDTO model)
        {
            return new DataLayer.Domain.User
            {
                Id = model.Id,
                Username = model.Username,
                Password = model.Password,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                Avatar = model.Avatar,
                UserRoleId = model.UserRoleId,
                PositionId = model.PositionId,
                DepartmentId = model.DepartmentId,
                LastLogin = model.LastLogin,
                IsActive = model.IsActive,
            };
        }

    }
}

