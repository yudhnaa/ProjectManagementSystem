using DataLayer.Domain;
using System.Collections.Generic;

namespace DataLayer.DataAccess
{
    public interface IProjectMemberRoleDAL
    {
        bool CreateProjectMemberRole(ProjectMemberRole projectMemberRole);
        List<ProjectMemberRole> GetAllProjectMemberRoles(string kw, bool isIncludeInActive);
        ProjectMemberRole GetRoleById(int id, bool isIncludeInActive);
        bool UpdateProjectMemberRole(ProjectMemberRole projectMemberRole);

    }
}