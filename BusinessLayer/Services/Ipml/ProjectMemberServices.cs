using DataLayer.DataAccess;
using DataLayer.Domain;
using DTOLayer.Mappers;
using DTOLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ProjectMemberServices : IProjectMemberServices
    {
        public bool CreateMemberToProject(ProjectMemberDTO projectMemberDTO)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    ProjectMemberDAL projectMemberDAL = new ProjectMemberDAL();
                    ProjectMember projectMember = projectMemberDTO.ToProjectMemberEntity();

                    bool res = projectMemberDAL.CreateProjectMember(projectMember);

                    return res;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<ProjectMemberDTO> GetProjectMembersById(int projectId)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    ProjectMemberDAL projectMemberDAL = new ProjectMemberDAL();
                    var members = projectMemberDAL.GetProjectMembersById(projectId, isIncludeInActive: false);
                    if (members == null)
                        return null;

                    return members.Select(pm => pm.ToDto()).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        public List<ProjectMemberDTO> GetProjectMembersByIdInlcudeInActive(int projectId)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    ProjectMemberDAL projectMemberDAL = new ProjectMemberDAL();
                    var members = projectMemberDAL.GetProjectMembersById(projectId, isIncludeInActive: true);
                    if (members == null)
                        return null;

                    return members.Select(pm => pm.ToDto()).ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool UpdateProjectMember(ProjectMemberDTO projectMemberDTO, int[] deleteUserId)
        {
            try
            {
                ProjectMemberDAL projectMemberDAL = new ProjectMemberDAL();

                ProjectMember projectMember = projectMemberDTO.ToProjectMemberEntity();

                var res = projectMemberDAL.UpdateProjectMember(projectMember);

                if (res == false)
                    projectMemberDAL.CreateProjectMember(projectMember);

                foreach (var userId in deleteUserId)
                {
                    projectMemberDAL.RemoveMemberFromProject(projectMember.ProjectId, userId);
                }

                return true;
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
