using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public UserDTO CheckLoginUser(UserDTO userDTO)
        {
            try
            {
                UserDAL userDAL = new UserDAL();

                var user = userDAL.CheckLoginUser(userDTO.ToUserEntity());

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

        public List<UserDTO> GetUserByKw(string kw, int pageSize)
        {
            try
            {
                UserDAL userDAL = new UserDAL();
                var users = userDAL.GetUserByKw(kw, pageSize);

                // Explicitly specify the mapper to resolve ambiguity
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

        public UserDTO createUser(UserDTO userDTO)
        {
            try
            {
                UserDAL userDAL = new UserDAL();

                var user = userDTO.ToUserEntity();
                user.LastLogin = null;
                user.IsActive = true;
                user.IsDeleted = false;
                user.UpdatedDate = null;

                var res = userDAL.createUser(user);
                
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

        public bool UpdateUser(UserDTO userDTO)
        {
            try
            {
                UserDAL userDAL = new UserDAL();
                var user = userDTO.ToUserEntity();

                user.UpdatedDate = DateTime.Now;

                userDAL.UpdateUser(user);
                
                return true;
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
                UserDAL userDAL = new UserDAL();
                var user = userDAL.GetUserById(userId);
                if (user != null)
                {
                    return UserDTOMapper.ToDto(user);
                }
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

        public List<UserDTO> GetAllUsers()
        {
            try
            {
                UserDAL userDAL = new UserDAL();
                var users = userDAL.GetAllUsers();
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
    }
}
