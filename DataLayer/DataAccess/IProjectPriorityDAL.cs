using DataLayer.Domain;
using System.Collections.Generic;

namespace DataLayer.DataAccess
{
    public interface IProjectPriorityDAL
    {
        bool CreateProjectPriority(ProjectPriority projectPriority);
        List<ProjectPriority> GetAllProjectPriorities(string kw, bool isIncludeInActive);
        ProjectPriority GetById(int priorityId, bool isIncludeInActive);
        bool UpdateProjectPriority(ProjectPriority projectPriority);
    }
}