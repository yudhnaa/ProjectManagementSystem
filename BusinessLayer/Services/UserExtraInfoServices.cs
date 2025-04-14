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
    }
}
