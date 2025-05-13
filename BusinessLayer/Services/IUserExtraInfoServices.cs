using DTOLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public interface IUserExtraInfoServices
    {
        UserExtraInfoDTO Createuser(UserExtraInfoDTO userDTO);
        List<UserExtraInfoDTO> GetAllUser(string kw);
        List<UserExtraInfoDTO> GetAllUserInlcudeInActive(string kw);
        UserExtraInfoDTO GetUserById(int id);
        UserExtraInfoDTO GetUserByIdInlcudeInActive(int id);
        bool UpdateUser(UserExtraInfoDTO user);
    }
}