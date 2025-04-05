using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Domain;
using DTOLayer.Mappers;
using DTOLayer.Models;

namespace BusinessLayer
{
    public class UserServices
    {
        private User user { get; set; }

        public UserDTO CheckLoginUser(UserDTO userDTO)
        {

            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                var user = dbContext.Users.Where(u => u.Username == userDTO.Username).FirstOrDefault();

                //var user = dbContext.Users.Where(u => u.Username == userDTO.Username).Select(u => new
                //{
                //    u.Id,
                //    u.Username,
                //    u.Email,
                //    u.FirstName,
                //    u.LastName,
                //    u.UserRole,
                //    u.Avatar,
                //    u.Password
                //}).FirstOrDefault();

                if (user == null)
                    return null;
                else
                {
                    if (user.Password != userDTO.Password)
                        return null;
                    else
                        return UserDTOMapper.ToDto(user);

                        //    new UserDTO
                        //{
                        //    Id = user.Id,
                        //    Username = user.Username,
                        //    Email = user.Email,
                        //    FirstName = user.FirstName,
                        //    LastName = user.LastName,
                        //    UserRole = user.UserRole.Id,
                        //    Avatar = user.Avatar,
                        //};
                }
            }
        }

        public List<UserDTO> GetAllUsers()
        {
            return null;
        }

        public UserDTO GetUserById(int Id)
        {
            return null;
        }

        public UserDTO AddOrUpdateUser()
        {

            return null;
        }

        public void DeleteUserById()
        {
            return;
        }
    }
}
