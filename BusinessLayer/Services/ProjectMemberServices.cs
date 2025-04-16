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
    public class ProjectMemberServices
    {

        //public List<UserDTO> GetProjectMembers(int projectId)
        //{
        //    using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
        //    {
        //        var memberIds = dbContext.ProjectMembers
        //            .Where(pm => pm.ProjectId == projectId)
        //            .Select(pm => pm.UserId)
        //            .ToList();

        //        var users = dbContext.Users
        //            .Where(u => memberIds.Contains(u.Id))
        //            .ToList();

        //        return users.Select(u => u.ToDto()).ToList();
        //    }
        //}

        public bool AddMemberToProject(ProjectMemberDTO projectMemberDTO)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    ProjectMemberDAL projectMemberDAL = new ProjectMemberDAL();
                    ProjectMember projectMember = projectMemberDTO.ToProjectMemberEntity();

                    bool res = projectMemberDAL.AddMemberToProject(projectMember);

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
                    var members = projectMemberDAL.GetProjectMembersById(projectId);

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
                    projectMemberDAL.AddMemberToProject(projectMember);

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

        //public bool RemoveMemberFromProject(int projectId, int userId)
        //{
        //    using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
        //    {
        //        ProjectMember projectMember = dbContext.ProjectMembers.FirstOrDefault(pm => pm.ProjectId == projectId && pm.UserId == userId);
        //        if (projectMember == null)
        //            return false;

        //        dbContext.ProjectMembers.Remove(projectMember);
        //        dbContext.SaveChanges();
        //        return true;
        //    }
        //}
    }
}
