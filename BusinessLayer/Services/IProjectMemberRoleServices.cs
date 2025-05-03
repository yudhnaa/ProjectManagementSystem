using DTOLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public interface IProjectMemberRoleServices
    {
        List<ProjectMemberRoleDTO> GetAllProjectMemberRoles(string kw);
        List<ProjectMemberRoleDTO> GetAllProjectMemberRolesInlcudeInActive(string kw);
        ProjectMemberRoleDTO GetRoleById(int id);
        ProjectMemberRoleDTO GetRoleByIdInlcudeInActive(int id);
    }
}