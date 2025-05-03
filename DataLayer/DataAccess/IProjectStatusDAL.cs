using DataLayer.Domain;
using System.Collections.Generic;

namespace DataLayer.DataAccess
{
    public interface IProjectStatusDAL
    {
        bool CreateProjectStatus(ProjectStatus projectStatus);
        List<ProjectStatus> GetAllProjectStatuses(string kw, bool isIncludeInActive);
        ProjectStatus GetById(int statusId, bool isIncludeInActive);
        List<ProjectStatus> GetProjectStatusByName(string kw, bool isIncludeInActive);
        List<ProjectStatus> GetProjectStatusByStatusId(int statusId, bool isIncludeInActive);
        bool UpdateProjectStatus(ProjectStatus projectStatus);
    }
}