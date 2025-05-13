using DTOLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public interface IProjectMemberServices
    {
        bool CreateMemberToProject(ProjectMemberDTO projectMemberDTO);
        List<ProjectMemberDTO> GetAllProjectMembers(string kw);
        List<ProjectMemberDTO> GetAllProjectMembersIncludeInActive(string kw);
        List<ProjectMemberDTO> GetProjectMembersByProjectId(int projectId);
        List<ProjectMemberDTO> GetProjectMembersByProjectIdInlcudeInActive(int projectId);
        bool UpdateProjectMember(ProjectMemberDTO projectMemberDTO);
        bool UpdateProjectMember(List<ProjectMemberDTO> projectMemberDTO);
        bool RemoveProjectMember(int projectId, int userId);

    }
}