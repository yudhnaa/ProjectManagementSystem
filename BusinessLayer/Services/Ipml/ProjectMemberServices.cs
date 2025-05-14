using BusinessLayer.Services.Ipml;
using DataLayer.DataAccess;
using DataLayer.Domain;
using DataLayer.EnumObjects;
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
        private readonly IProjectMemberDAL projectMemberDAL = new ProjectMemberDAL();

        public bool CreateMemberToProject(ProjectMemberDTO projectMemberDTO, NotificationDTO notification)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    
                    ProjectMember projectMember = projectMemberDTO.ToProjectMemberEntity();
                    projectMember.IsConfirmed = false;
                    projectMember.IsActive = true;
                    projectMember.IsDeleted = false;
                    projectMember.CreatedDate = DateTime.Now;
                    projectMember.UpdatedDate = DateTime.Now;

                    bool res = projectMemberDAL.CreateProjectMember(projectMember);

                    INotificationServices notificationServices = new NotificationServices();
                    bool res1 = notificationServices.CreateNotification(notification);

                    return res && res1;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public List<ProjectMemberDTO> GetAllProjectMembers(string kw)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    var members = projectMemberDAL.GetAllProjectMembers(kw, isIncludeInActive: false);
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

        public List<ProjectMemberDTO> GetAllProjectMembersIncludeInActive(string kw)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {

                    var members = projectMemberDAL.GetAllProjectMembers(kw, isIncludeInActive: true);
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

        public List<ProjectMemberDTO> GetProjectMembersByProjectId(int projectId)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    
                    var members = projectMemberDAL.GetProjectMembersByProjectId(projectId, isIncludeInActive: false);
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
        public List<ProjectMemberDTO> GetProjectMembersByProjectIdInlcudeInActive(int projectId)
        {
            using (ProjectManagementSystemDBContext dbContext = new ProjectManagementSystemDBContext())
            {
                try
                {
                    
                    var members = projectMemberDAL.GetProjectMembersByProjectId(projectId, isIncludeInActive: true);
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

        public bool RemoveProjectMember(int projectId, int userId)
        {
            try
            {

                var res = projectMemberDAL.RemoveMemberFromProject(projectId, userId);


                return res;
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

        public bool UpdateProjectMember(ProjectMemberDTO projectMemberDTO, NotificationDTO notification)
        {
            try
            {
                var curProjectMember = projectMemberDAL
                    .GetProjectMembersByProjectId(projectMemberDTO.ProjectId, true)
                    .FirstOrDefault(t => t.UserId == projectMemberDTO.UserId);

                var res = false;
                if (curProjectMember != null)
                {
                    curProjectMember.RoleInProject = projectMemberDTO.RoleInProject;
                    curProjectMember.IsConfirmed = projectMemberDTO.IsConfirmed;
                    curProjectMember.UpdatedDate = DateTime.Now;
                    res = projectMemberDAL.UpdateProjectMember(curProjectMember);
                }
                else
                {
                    var newProjectMember = projectMemberDTO.ToProjectMemberEntity();
                    newProjectMember.IsConfirmed = false;
                    newProjectMember.IsActive = true;
                    newProjectMember.IsDeleted = false;
                    newProjectMember.CreatedDate = DateTime.Now;
                    newProjectMember.UpdatedDate = DateTime.Now;
                    projectMemberDAL.CreateProjectMember(newProjectMember);

                    INotificationServices notificationServices = new NotificationServices();
                    res = notificationServices.CreateNotification(notification);
                }

                return res;
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

        public bool UpdateProjectMember(List<ProjectMemberDTO> projectMemberDTO)
        {
            try
            {
                foreach (var pm in projectMemberDTO)
                {
                    var curProjectMember = projectMemberDAL
                        .GetProjectMembersByProjectId(pm.ProjectId, true)
                        .FirstOrDefault(t => t.UserId == pm.UserId);

                    if (curProjectMember != null)
                    {
                        curProjectMember.RoleInProject = pm.RoleInProject;
                        curProjectMember.IsConfirmed = pm.IsConfirmed;
                        curProjectMember.UpdatedDate = DateTime.Now;
                        var res = projectMemberDAL.UpdateProjectMember(curProjectMember);
                    }
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

        //public bool UpdateProjectMember(ProjectMemberDTO projectMemberDTO, int[] deleteUserId)
        //{
        //    throw new NotImplementedException();
        //}

        public bool ConfirmProjectMemberByNotification(int userId, NotificationTypeEnum type, string kw)
        {
            try
            {
                var res = projectMemberDAL.GetProjectMemberByNotification(userId, type, kw);

                if (res == null)
                    return false;

                res.IsConfirmed = true;
                res.ConfirmationDate = DateTime.Now;
                res.UpdatedDate = DateTime.Now;
                res.JoinDate = DateTime.Now;

                var res1 = projectMemberDAL.UpdateProjectMember(res);

                return res1;
            }
            catch (Exception ex) when (ex is SqlException || ex is Exception)
            {
                throw new Exception("An error occurred while retrieving project member by ID.", ex);
            }
        }
    }
}
