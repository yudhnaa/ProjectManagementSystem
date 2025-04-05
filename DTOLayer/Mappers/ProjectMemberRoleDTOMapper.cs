using DTOLayer.Models;

namespace DTOLayer.Mappers
{
    public static class ProjectMemberRoleDTOMapper
    {
        public static ProjectMemberRoleDTO ToDto(this DataLayer.Domain.ProjectMemberRole projectmemberrole)
        {
            return new ProjectMemberRoleDTO
            {
                Id = projectmemberrole.Id,
                Name = projectmemberrole.Name,
                Description = projectmemberrole.Description,
                CreatedDate = projectmemberrole.CreatedDate,
                UpdatedDate = projectmemberrole.UpdatedDate,
            };
        }

        public static DataLayer.Domain.ProjectMemberRole ToProjectMemberRoleEntity(this ProjectMemberRoleDTO model)
        {
            return new DataLayer.Domain.ProjectMemberRole
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                CreatedDate = model.CreatedDate,
                UpdatedDate = model.UpdatedDate,
            };
        }

    }
}

