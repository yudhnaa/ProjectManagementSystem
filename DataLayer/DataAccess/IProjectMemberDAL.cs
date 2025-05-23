﻿using DataLayer.Domain;
using DataLayer.EnumObjects;
using System.Collections.Generic;

namespace DataLayer.DataAccess
{
    public interface IProjectMemberDAL
    {
        bool CreateProjectMember(ProjectMember projectMember);
        List<ProjectMember> GetAllProjectMembers(string kw, bool isIncludeInActive);
        List<ProjectMember> GetProjectMembersByProjectId(int projectId, bool isIncludeInActive);
        bool RemoveMemberFromProject(int projectId, int userId);
        bool UpdateProjectMember(ProjectMember projectMember);
        ProjectMember GetProjectMemberByNotification(int userId, NotificationTypeEnum type, string kw);
    }
}