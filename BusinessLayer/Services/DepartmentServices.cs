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
    public class DepartmentServices
    {
        public List<DepartmentDTO> GetAllDepartments()
        {
            try
            {
                DepartmentDAL departmentDAL = new DepartmentDAL();
                var departments = departmentDAL.GetAllDepartments();

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
                var department = departmentDAL.GetDepartmentById(departmentId);

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

        //public List<DepartmentDTO> getDepartmentsByKw(string kw)
        //{
        //    using (var dbContext = new ProjectManagementSystemDBContext())
        //    {
        //        try
        //        {
        //            var departments = dbContext.Departments
        //                .Where(d => d.Name.Contains(kw))
        //                .ToList();
        //            return departments.Select(d => d.ToDto()).ToList();
        //        }
        //        catch (SqlException ex)
        //        {
        //            // Handle SQL exceptions (e.g., log the error, rethrow, etc.)
        //            throw new Exception("Database error occurred while retrieving departments.", ex);
        //        }
        //        catch (Exception ex)
        //        {
        //            // Handle other exceptions
        //            throw new Exception("An error occurred while retrieving departments.", ex);
        //        }
        //    }
        //}

        //// Example method to add a new department
        //public void AddDepartment(DepartmentDTO department)
        //{
        //    // Logic to add a new department to the data layer
        //}
        //// Example method to update an existing department
        //public void UpdateDepartment(DepartmentDTO department)
        //{
        //    // Logic to update an existing department in the data layer
        //}
        //// Example method to delete a department
        //public void DeleteDepartment(int departmentId)
        //{
        //    // Logic to delete a department from the data layer
        //}
    }
}
