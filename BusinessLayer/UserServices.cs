using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Domain;
using DTOLayer;

namespace BusinessLayer
{
    public class UserServices
    {
        User user { get; set; }

        public UserDTO checkLoginUser(UserDTO userDTO)
        {

            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                var user = dbContext.Users.Where(u => u.Username == userDTO.Username).Select(u => new
                {
                    u.Id,
                    u.Username,
                    u.Email,
                    u.FirstName,
                    u.LastName,
                    u.UserRole,
                    u.Avatar,
                    u.Password
                }).FirstOrDefault();

                if (user == null)
                    return null;
                else
                {
                    if (user.Password != userDTO.Password)
                        return null;
                    else
                        return new UserDTO
                        {
                            Id = user.Id,
                            Username = user.Username,
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            UserRoles = new UserRoleDTO
                            {
                                Id = user.UserRole.Id,
                                Name = user.UserRole.Name,
                                Description = user.UserRole.Description,
                            },
                            Avatar = user.Avatar,
                        };
                }
            }
        }
    }
}
