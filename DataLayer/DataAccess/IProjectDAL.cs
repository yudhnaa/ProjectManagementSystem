using DataLayer.Domain;
using System.Collections.Generic;

namespace DataLayer.DataAccess
{
    public interface IProjectDAL
    {
        Project CreateProject(Project project);
        List<Project> GetAllProjects(string kw, bool isIncludeInActive);
        Project GetProjectById(int projectId, bool isIncludeInActive);
        List<Project> GetProjectsByKeywordAndStatus(string keyword, int statusId, bool isIncludeInActive);
        List<Project> GetProjectsByUserId(int userId, bool isIncludeInActive);
        bool UpdateProject(Project project);
    }
}