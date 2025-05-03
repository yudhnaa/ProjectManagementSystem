using DTOLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public interface IProjectMemberServices
    {
        bool CreateMemberToProject(ProjectMemberDTO projectMemberDTO);
        List<ProjectMemberDTO> GetProjectMembersById(int projectId);
        List<ProjectMemberDTO> GetProjectMembersByIdInlcudeInActive(int projectId);
        bool UpdateProjectMember(ProjectMemberDTO projectMemberDTO, int[] deleteUserId);
    }
}