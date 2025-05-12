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
        public UserDTO CreateUser(UserDTO userDTO)
        {
            try
            {
                

                var user = userDTO.ToUserEntity();
                user.LastLogin = null;
                user.IsActive = true;
                user.IsDeleted = false;
                user.UpdatedDate = null;

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

        public bool UpdateUser(UserDTO userDTO)
        {
            try
            {
                
                var user = userDTO.ToUserEntity();

                user.UpdatedDate = DateTime.Now;

                var res = userDAL.UpdateUser(user);

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
