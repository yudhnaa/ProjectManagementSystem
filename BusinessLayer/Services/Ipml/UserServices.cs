using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.DataAccess;
using DataLayer.Domain;
using DTOLayer.Mappers;
using DTOLayer.Models;

namespace BusinessLayer.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUserDAL userDAL = new UserDAL();

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public UserDTO CreateUser(UserDTO userDTO)
        {
            try
            {
                var user = userDTO.ToUserEntity();
                user.LastLogin = null;
                user.IsActive = true;
                user.IsDeleted = false;
                user.CreatedDate = DateTime.Now;
                user.UpdatedDate = null;
                user.Password = HashPassword(user.Password);

                var res = userDAL.CreateUser(user);

                if (res != null)
                    return UserDTOMapper.ToDto(res);

                return null;
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred: " + ex.Message);
            }
        }

        public UserDTO CheckLoginUser(UserDTO userDTO)
        {
            try
            {
                var user = userDAL.CheckLoginUser(userDTO.ToUserEntity());

                if (user != null && BCrypt.Net.BCrypt.Verify(userDTO.Password, user.Password))
                {
                    user.LastLogin = DateTime.Now;
                    userDAL.UpdateUser(user);
                }
                else if (user != null && !BCrypt.Net.BCrypt.Verify(userDTO.Password, user.Password))
                {
                    return null;
                }

                if (user == null)
                    return null;

                return UserDTOMapper.ToDto(user);
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred: " + ex.Message);
            }
        }

        public bool DeleteUserById(int userId)
        {
            try
            {
                
                var user = userDAL.GetUserById(userId, isIncludeInActive: false);

                if (user != null && user.IsDeleted == false && user.IsActive == true)
                {
                    user.IsDeleted = true;
                    user.IsActive = false;
                    var res = userDAL.UpdateUser(user);
                    return res;
                }

                return false;
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred: " + ex.Message);
            }
        }

        public List<UserDTO> GetAllUsers(string kw)
        {
            try
            {
                
                var users = userDAL.GetAllUsers(kw, isIncludeInActive: false);
                if (users == null)
                    return null;

                return users.Select(u => UserDTOMapper.ToDto(u)).ToList();
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred: " + ex.Message);
            }
        }

        public List<UserDTO> GetAllUsersInlcudeInActive(string kw)
        {
            try
            {
                
                var users = userDAL.GetAllUsers(kw, isIncludeInActive: true);
                if (users == null)
                    return null;

                return users.Select(u => UserDTOMapper.ToDto(u)).ToList();
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred: " + ex.Message);
            }
        }

        public UserDTO GetUserById(int userId)
        {
            try
            {
                
                var user = userDAL.GetUserById(userId, isIncludeInActive: false);

                if (user == null)
                    throw new Exception("User not found");

                return UserDTOMapper.ToDto(user);
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred: " + ex.Message);
            }
        }

        public UserDTO GetUserByIdInlcudeInActive(int userId)
        {
            try
            {
                
                var user = userDAL.GetUserById(userId, isIncludeInActive: true);

                if (user == null)
                    throw new Exception("User not found");

                return UserDTOMapper.ToDto(user);
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred: " + ex.Message);
            }
        }

        public bool HardDeleteUserById(int id)
        {
            // check relations before delete
            throw new NotImplementedException();
        }

        private User GetUserForUpdate(UserDTO updateUser, User curUser)
        {
            curUser.Username = string.IsNullOrEmpty(updateUser.Username) || updateUser.Username == curUser.Username ? curUser.Username : updateUser.Username;
            curUser.Password = string.IsNullOrEmpty(updateUser.Password) || updateUser.Password == curUser.Password ? curUser.Password : HashPassword(updateUser.Password);
            curUser.Email = string.IsNullOrEmpty(updateUser.Email) || updateUser.Email == curUser.Email ? curUser.Email : updateUser.Email;
            curUser.FirstName = string.IsNullOrEmpty(updateUser.FirstName) || updateUser.FirstName == curUser.FirstName ? curUser.FirstName : updateUser.FirstName;
            curUser.LastName = string.IsNullOrEmpty(updateUser.LastName) || updateUser.LastName == curUser.LastName ? curUser.LastName : updateUser.LastName;
            curUser.PhoneNumber = string.IsNullOrEmpty(updateUser.PhoneNumber) || updateUser.PhoneNumber == curUser.PhoneNumber ? curUser.PhoneNumber : updateUser.PhoneNumber;
            curUser.Address = string.IsNullOrEmpty(updateUser.Address) || updateUser.Address == curUser.Address ? curUser.Address : updateUser.Address;
            curUser.Avatar = string.IsNullOrEmpty(updateUser.Avatar) || updateUser.Avatar == curUser.Avatar ? curUser.Avatar : updateUser.Avatar;
            curUser.UserRoleId = updateUser.UserRoleId == 0 || updateUser.UserRoleId == curUser.UserRoleId ? curUser.UserRoleId : updateUser.UserRoleId;
            curUser.UpdatedDate = DateTime.Now;
            return curUser;
        }

        public bool UpdateUser(UserDTO userDTO)
        {
            try
            {

                var curUser = userDAL.GetUserById(userDTO.Id, true);
                if (!curUser.Password.Equals(userDTO.Password))
                {
                    userDTO.Password = HashPassword(userDTO.Password);
                }

                // this api is used to update user info by user so not accept to update user role by replace original user role
                userDTO.UserRoleId = curUser.UserRoleId;

                var userUpdate = GetUserForUpdate(userDTO, curUser);
                var res = userDAL.UpdateUser(userUpdate);

                return res;
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred: " + ex.Message);
            }
        }

        public List<UserDTO> GetUsersInProject(string kw, int ProjectId)
        {
            try
            {
                IProjectMemberDAL projectMemberDAL = new ProjectMemberDAL();
                var projectMembers = projectMemberDAL.GetProjectMembersByProjectId(ProjectId, isIncludeInActive: false);

                if (projectMembers == null)
                    return null;

                List<UserDTO> users = new List<UserDTO>();
                foreach (var projectMember in projectMembers)
                {
                    var user = userDAL.GetUserById(projectMember.UserId, isIncludeInActive: false);
                    if (user != null)
                    {
                        if (user.Username.Contains(kw) || user.FirstName.Contains(kw) || user.LastName.Contains(kw))
                        {
                            users.Add(UserDTOMapper.ToDto(user));
                        }
                    }
                }

                if (users == null)
                    return null;
                return users;
            }
            catch (SqlException ex)
            {
                throw new Exception("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred: " + ex.Message);
            }
        }
    }
}
