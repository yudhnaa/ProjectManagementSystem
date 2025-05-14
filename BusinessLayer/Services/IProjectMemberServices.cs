using DataLayer.EnumObjects;
using DTOLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public interface IProjectMemberServices
    {
        bool CreateMemberToProject(ProjectMemberDTO projectMemberDTO, NotificationDTO notification);
        List<ProjectMemberDTO> GetAllProjectMembers(string kw);
        List<ProjectMemberDTO> GetAllProjectMembersIncludeInActive(string kw);
        List<ProjectMemberDTO> GetProjectMembersByProjectId(int projectId);
        List<ProjectMemberDTO> GetProjectMembersByProjectIdInlcudeInActive(int projectId);
        bool UpdateProjectMember(ProjectMemberDTO projectMemberDTO, NotificationDTO notification);
        bool UpdateProjectMember(List<ProjectMemberDTO> projectMemberDTO);
        bool RemoveProjectMember(int projectId, int userId);
        bool ConfirmProjectMemberByNotification(int userId, NotificationTypeEnum type, string kw);

    }
}