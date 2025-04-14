using DTOLayer.Models;

namespace DTOLayer.Mappers
{
    public static class DepartmentDTOMapper
    {
        public static DepartmentDTO ToDto(this DataLayer.Domain.Department department)
        {
            return new DepartmentDTO
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description,
                ManagerId = department.ManagerId,
                IsActive = department.IsActive,
            };
        }

        public static DataLayer.Domain.Department ToDepartmentEntity(this DepartmentDTO model)
        {
            return new DataLayer.Domain.Department
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                ManagerId = model.ManagerId,
                IsActive = model.IsActive,
            };
        }

    }
}

