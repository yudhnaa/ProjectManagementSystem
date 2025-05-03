using DataLayer.Domain;
using System.Collections.Generic;

namespace DataLayer.DataAccess
{
    public interface IDepartmentDAL
    {
        bool CreateDepartment(Department department);
        List<Department> GetAllDepartments(string kw, bool isIncludeInActive);
        Department GetDepartmentById(int departmentId, bool isIncludeInActive);
        List<Department> GetDepartmentsByKw(string kw, bool isIncludeInActive);
        bool UpdateDepartment(Department department);
    }
}