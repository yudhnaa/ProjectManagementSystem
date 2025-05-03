using DTOLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public interface IProjectStatusServices
    {
        bool CreateProjectStatus(ProjectStatusDTO projectStatusDTO);
        List<ProjectStatusDTO> GetAllProjectStatuses(string kw);
        List<ProjectStatusDTO> GetAllProjectStatusesInlcudeInActive(string kw);
        ProjectStatusDTO GetById(int statusId);
        ProjectStatusDTO GetByIdInlcudeInActive(int statusId);
        List<ProjectStatusDTO> GetProjectStatusByName(string kw);
        List<ProjectStatusDTO> GetProjectStatusByNameInlcudeInActive(string kw);
        bool UpdateProjectStatus(ProjectStatusDTO item);
    }
}