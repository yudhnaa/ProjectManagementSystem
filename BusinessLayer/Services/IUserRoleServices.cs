using DTOLayer.Mappers;
using DTOLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public interface IUserRoleServices
    {
        bool CreateUserRole(UserRoleDTO userRoleDTO);
        List<UserRoleDTO> GetAllUserRoles(string kw);
        List<UserRoleDTO> GetAllUserRolesInlcudeInActive(string kw);
        UserRoleDTO GetUserRoleById(int id);
        UserRoleDTO GetUserRoleByIdInlcudeInActive(int id);
        bool UpdateUserRole(UserRoleDTO item);
    }
}