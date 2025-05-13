using DTOLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public interface IUserServices
    {
        UserDTO CheckLoginUser(UserDTO userDTO);
        UserDTO CreateUser(UserDTO userDTO);
        bool DeleteUserById(int userId);
        List<UserDTO> GetAllUsers(string kw);
        List<UserDTO> GetAllUsersInlcudeInActive(string kw);
        UserDTO GetUserById(int userId);
        UserDTO GetUserByIdInlcudeInActive(int userId);
        bool HardDeleteUserById(int id);
        bool UpdateUser(UserDTO userDTO);
        List<UserDTO> GetUsersInProject(string kw, int ProjectId);
    }
}