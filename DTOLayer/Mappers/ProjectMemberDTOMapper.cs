using DTOLayer.Models;

namespace DTOLayer.Mappers
{
    public static class ProjectMemberDTOMapper
    {
        public static ProjectMemberDTO ToDto(this DataLayer.Domain.ProjectMember projectmember)
        {
            return new ProjectMemberDTO
            {
                Id = projectmember.Id,
                ProjectId = projectmember.ProjectId,
                UserId = projectmember.UserId,
                JoinDate = projectmember.JoinDate,
                RoleInProject = projectmember.RoleInProject,
                IsConfirmed = projectmember.IsConfirmed,
                ConfirmationDate = projectmember.ConfirmationDate,
                IsActive = projectmember.IsActive,
                IsDeleted = projectmember.IsDeleted,
                CreatedDate = projectmember.CreatedDate,
                UpdatedDate = projectmember.UpdatedDate,
            };
        }

        public static DataLayer.Domain.ProjectMember ToProjectMemberEntity(this ProjectMemberDTO model)
        {
            return new DataLayer.Domain.ProjectMember
            {
                Id = model.Id,
                ProjectId = model.ProjectId,
                UserId = model.UserId,
                JoinDate = model.JoinDate,
                RoleInProject = model.RoleInProject,
                IsConfirmed = model.IsConfirmed,
                ConfirmationDate = model.ConfirmationDate,
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted,
                CreatedDate = model.CreatedDate,
                UpdatedDate = model.UpdatedDate,
            };
        }

    }
}

