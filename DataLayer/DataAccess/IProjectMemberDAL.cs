using DataLayer.Domain;
using System.Collections.Generic;

namespace DataLayer.DataAccess
{
    public interface IProjectMemberDAL
    {
        bool CreateProjectMember(ProjectMember projectMember);
        List<ProjectMember> GetProjectMembersById(int projectId, bool isIncludeInActive);
        bool RemoveMemberFromProject(int projectId, int userId);
        bool UpdateProjectMember(ProjectMember projectMember);
    }
}