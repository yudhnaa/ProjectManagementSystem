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
    public class UserExtraInfoServices
    {
        public UserExtraInfoDTO Createuser(UserDTO userDTO)
        {
            try
            {
                UserDAL userDAL = new UserDAL();

                var user = userDTO.ToUserEntity();
                user.IsActive = true;
                user.CreatedDate = DateTime.Now;

                var res = userDAL.createUser(user);

                return UserExtraInfoDTOMapper.ToDto(res);
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

        public List<UserExtraInfoDTO> GetAllUser(int page, int pageSize)
        {
            try
            {
                UserDAL userDAL = new UserDAL();
                var users = userDAL.GetAllUsers(page, pageSize);
                if (users == null)
                {
                    throw new Exception("No users found");
                }
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
                UserDAL userDAL = new UserDAL();
                var user = userDAL.GetUserById(id);
                if (user == null)
                {
                    throw new Exception("User not found");
                }

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

        public UserExtraInfoDTO UpdateUser(UserExtraInfoDTO user)
        {
            try
            {
                UserDAL userDAL = new UserDAL();
                var userEntity = user.ToUserEntity();
                userEntity.UpdatedDate = DateTime.Now;

                var res = userDAL.UpdateUser(userEntity);
                if (res == null)
                {
                    throw new Exception("User not found");
                }

                return UserExtraInfoDTOMapper.ToDto(res);
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
