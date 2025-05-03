using DTOLayer.Models;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public interface IDepartmentServices
    {
        bool CreateDepartment(DepartmentDTO departmentDTO);
        List<DepartmentDTO> GetAllDepartments(string kw);
        List<DepartmentDTO> GetAllDepartmentsInlcudeInactive(string kw);
        DepartmentDTO GetDepartmentById(int departmentId);
        DepartmentDTO GetDepartmentByIdInlcudeInActive(int departmentId);
        bool UpdateDepartment(DepartmentDTO item);
    }
}