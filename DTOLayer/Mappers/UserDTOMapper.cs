using DTOLayer.Models;

namespace DTOLayer.Mappers
{
    public static class UserDTOMapper
    {
        public static UserDTO ToDto(this DataLayer.Domain.User user)
        {
            return new UserDTO
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
                LastLogin = user.LastLogin,
                CreatedDate = user.CreatedDate,
            };
        }

        public static DataLayer.Domain.User ToUserEntity(this UserDTO model)
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
                LastLogin = model.LastLogin,
                CreatedDate = model.CreatedDate,
            };
        }

    }
}

