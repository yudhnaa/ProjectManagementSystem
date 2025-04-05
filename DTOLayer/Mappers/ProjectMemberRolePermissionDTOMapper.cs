using DTOLayer.Models;

namespace DTOLayer.Mappers
{
    public static class ProjectMemberRolePermissionDTOMapper
    {
        public static ProjectMemberRolePermissionDTO ToDto(this DataLayer.Domain.ProjectMemberRolePermission projectmemberrolepermission)
        {
            return new ProjectMemberRolePermissionDTO
            {
                Id = projectmemberrolepermission.Id,
                ProjectMemberRoleId = projectmemberrolepermission.ProjectMemberRoleId,
                PermissionId = projectmemberrolepermission.PermissionId,
            };
        }

        public static DataLayer.Domain.ProjectMemberRolePermission ToProjectMemberRolePermissionEntity(this ProjectMemberRolePermissionDTO model)
        {
            return new DataLayer.Domain.ProjectMemberRolePermission
            {
                Id = model.Id,
                ProjectMemberRoleId = model.ProjectMemberRoleId,
                PermissionId = model.PermissionId,
            };
        }

    }
}

