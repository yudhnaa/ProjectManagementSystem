using DTOLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public interface IProjectPriorityServices
    {
        bool CreateProjectPriority(ProjectPriorityDTO projectPriorityDTO);
        List<ProjectPriorityDTO> GetAllProjectPriorities(string kw);
        ProjectPriorityDTO GetAllProjectPrioritiesById(int priorityId);
        ProjectPriorityDTO GetAllProjectPrioritiesByIdInlcudeInActive(int priorityId);
        List<ProjectPriorityDTO> GetAllProjectPrioritiesInlcudeInActive(string kw);
        bool UpdateProjectPriority(ProjectPriorityDTO item);
    }
}