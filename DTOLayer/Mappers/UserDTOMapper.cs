using DTOLayer.Models;
using System.Linq;

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
                UserRole = user.UserRole.ToDto(),
                LastLogin = user.LastLogin,
                CreatedDate = user.CreatedDate,
                Tasks = user.Tasks.Select(t => t.ToDto()).ToList(),
                Tasks1 = user.Tasks1.Select(t => t.ToDto()).ToList(),
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
                UserRole = model.UserRole.ToUserRoleEntity(),
                LastLogin = model.LastLogin,
                CreatedDate = model.CreatedDate,
                Tasks = model.Tasks.Select(t => t.ToTaskEntity()).ToList(),
                Tasks1 = model.Tasks1.Select(t => t.ToTaskEntity()).ToList(),
            };
        }
    }
}

