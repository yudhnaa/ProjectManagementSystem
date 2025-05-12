using DataLayer.DataAccess;
using DataLayer.Domain;
using DTOLayer.Mappers;
using DTOLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UserExtraInfoServices : IUserExtraInfoServices
    {
        private IUserDAL userDAL = new UserDAL();

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public UserExtraInfoDTO Createuser(UserExtraInfoDTO userDTO)
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
                    return UserExtraInfoDTOMapper.ToDto(res);

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

        public List<UserExtraInfoDTO> GetAllUser(string kw)
        {
            try
            {
                var users = userDAL.GetAllUsers(kw, isIncludeInActive: false);
                if (users == null)
                    return null;

                return users.Select(UserExtraInfoDTOMapper.ToDto).ToList();
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

        public List<UserExtraInfoDTO> GetAllUserInlcudeInActive(string kw)
        {
            try
            {
                
                var users = userDAL.GetAllUsers(kw, isIncludeInActive: true);
                if (users == null)
                    return null;

                return users.Select(UserExtraInfoDTOMapper.ToDto).ToList();
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

        public UserExtraInfoDTO GetUserById(int id)
        {
            try
            {   
                var user = userDAL.GetUserById(id, isIncludeInActive: false);
                if (user == null)
                    throw new Exception("User not found");

                return UserExtraInfoDTOMapper.ToDto(user);
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

        public UserExtraInfoDTO GetUserByIdInlcudeInActive(int id)
        {
            try
            {
                
                var user = userDAL.GetUserById(id, isIncludeInActive: true);
                if (user == null)
                    throw new Exception("User not found");

                return UserExtraInfoDTOMapper.ToDto(user);
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

        public bool UpdateUser(UserExtraInfoDTO user)
        {
            try
            {
                var userEntity = user.ToUserEntity();

                var curUser = userDAL.GetUserById(user.Id, true);
                if (!BCrypt.Net.BCrypt.Verify(userEntity.Password, curUser.Password))
                {
                    userEntity.Password = HashPassword(user.Password);
                }
                else
                {
                    userEntity.Password = curUser.Password;
                }

                userEntity.UpdatedDate = DateTime.Now;

                var res = userDAL.UpdateUser(userEntity);

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
    }
}
