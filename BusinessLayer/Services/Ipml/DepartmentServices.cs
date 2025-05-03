using DataLayer.DataAccess;
using DataLayer.Domain;
using DTOLayer;
using DTOLayer.Mappers;
using DTOLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace BusinessLayer.Services
{
    public class DepartmentServices : IDepartmentServices
    {
        public bool CreateDepartment(DepartmentDTO departmentDTO)
        {
            try
            {
                DepartmentDAL departmentDAL = new DepartmentDAL();

                var department = departmentDTO.ToDepartmentEntity();
                //department.IsDeleted = false;
                //department.IsActive = true;
                department.CreatedDate = DateTime.Now;
                department.UpdatedDate = null;

                var res = departmentDAL.CreateDepartment(department);

                return res;
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while adding department.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while adding department.", ex);
            }
        }

        public bool UpdateDepartment(DepartmentDTO item)
        {
            try
            {
                DepartmentDAL departmentDAL = new DepartmentDAL();

                var department = item.ToDepartmentEntity();
                department.UpdatedDate = DateTime.Now;

                var res = departmentDAL.UpdateDepartment(department);
                return res;
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while updating department.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while updating department.", ex);
            }
        }

        public List<DepartmentDTO> GetAllDepartments(string kw)
        {
            try
            {
                DepartmentDAL departmentDAL = new DepartmentDAL();
                var departments = departmentDAL.GetAllDepartments(kw, isIncludeInActive: false);
                if (departments == null)
                    return null;

                return departments.Select(d => d.ToDto()).ToList();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while retrieving departments.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while retrieving departments.", ex);
            }
        }

        public List<DepartmentDTO> GetAllDepartmentsInlcudeInactive(string kw)
        {
            try
            {
                DepartmentDAL departmentDAL = new DepartmentDAL();
                var departments = departmentDAL.GetAllDepartments(kw, isIncludeInActive: true);
                if (departments == null)
                    return null;

                return departments.Select(d => d.ToDto()).ToList();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while retrieving departments.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while retrieving departments.", ex);
            }
        }

        public DepartmentDTO GetDepartmentById(int departmentId)
        {
            try
            {
                DepartmentDAL departmentDAL = new DepartmentDAL();
                var department = departmentDAL.GetDepartmentById(departmentId, isIncludeInActive: false);

                if (department == null)
                {
                    throw new Exception("Department not found");
                }

                return department.ToDto();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while retrieving the department.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while retrieving the department.", ex);
            }
        }

        public DepartmentDTO GetDepartmentByIdInlcudeInActive(int departmentId)
        {
            try
            {
                DepartmentDAL departmentDAL = new DepartmentDAL();
                var department = departmentDAL.GetDepartmentById(departmentId, isIncludeInActive: true);

                if (department == null)
                {
                    throw new Exception("Department not found");
                }

                return department.ToDto();
            }
            catch (SqlException ex)
            {
                // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
                throw new Exception("Database error occurred while retrieving the department.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception("An error occurred while retrieving the department.", ex);
            }
        }
    }
}
