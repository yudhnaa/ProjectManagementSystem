using DTOLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public interface IProjectServices
    {
        ProjectDTO CreateProject(ProjectDTO projectDTO);
        bool DeleteProject(ProjectDTO project);
        List<ProjectDTO> GetAllProjects(string kw);
        List<ProjectForListDTO> GetAllProjectsForList(string kw);
        List<ProjectForListDTO> GetAllProjectsForListInlcudeInActive(string kw);
        List<ProjectDTO> GetAllProjectsInlcudeInActive(string kw);
        ProjectDTO GetProjectById(int projectId);
        ProjectDTO GetProjectByIdInlcudeInActive(int projectId);
        ProjectForListDTO GetProjectForListById(int projectId);
        ProjectForListDTO GetProjectForListByIdInlcudeInActive(int projectId);
        List<ProjectDTO> GetProjectsByUserId(int userId);
        List<ProjectDTO> GetProjectsByUserIdInlcudeInActive(int userId);
        List<ProjectForListDTO> GetProjectsForListByUserId(int userId);
        List<ProjectForListDTO> GetProjectsForListByUserIdInlcudeInActive(int userId);
        bool HardDeleteProjectById(int id);
        bool UpdateProject(ProjectDTO project);
    }
}